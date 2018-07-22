using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BeSide.BusinessLogic.BusinessComponents.Infrastructure;
using BeSide.BusinessLogic.Construct;
using BeSide.BusinessLogic.Construct.DTO;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;
using Microsoft.AspNet.Identity;

namespace BeSide.BusinessLogic.BusinessComponents
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;

        public UserService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<ClaimsIdentity> Authenticate(UserDto userDto)
        {
            ClaimsIdentity claim = null;

            // находим пользователя
            ApplicationUser user = await uow.UserManager.FindAsync(userDto.Email, userDto.Password);

            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
            {
                claim = await uow.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claim;
        }

        public async Task<OperationDetails> Create(UserDto userDto)
        {
            ApplicationUser applicationUser = await uow.UserManager.FindByEmailAsync(userDto.Email);

            if (applicationUser != null)
            {
                applicationUser = new ApplicationUser
                {
                    Email = userDto.Email,
                    UserName = userDto.UserName
                };

                var result = await uow.UserManager.CreateAsync(applicationUser, userDto.Password);

                if (result.Errors.Any())
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                // добавляем роль
                await uow.UserManager.AddToRoleAsync(applicationUser.Id, userDto.Role);

                // создаем профиль клиента
                UserProfile userProfile = new UserProfile()
                {
                    Id = applicationUser.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Patronymic = userDto.Patronymic
                };

                uow.UsersProfiles.Create(userProfile);
                uow.Save();

                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task SetInitialData(UserDto admin, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await uow.RoleManager.FindByNameAsync(roleName);

                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await uow.RoleManager.CreateAsync(role);
                }
            }

            await Create(admin);
        }
    }
}
