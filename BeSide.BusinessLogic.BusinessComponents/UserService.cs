using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BeSide.BusinessLogic.Construct;
using BeSide.BusinessLogic.Construct.DTO;
using BeSide.BusinessLogic.Construct.Infrastructure;
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

        public OperationDetails Create(UserDto userDto)
        {
            ApplicationUser applicationUser = uow.UserManager.FindByEmail(userDto.Email);

            if (applicationUser == null)
            {
                applicationUser = new ApplicationUser
                {
                    Email = userDto.Email,
                    UserName = userDto.UserName
                };

                var result = uow.UserManager.Create(applicationUser, userDto.Password);



                if (result.Errors.Any())
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                switch (userDto.Role)
                {
                    case "provider":
                        CreateProviderProfile(userDto, applicationUser);
                        break;

                    case "client":
                        CreateClientProfile(userDto, applicationUser);
                        break;

                    case "admin":
                        uow.UserManager.AddToRole(applicationUser.Id, userDto.Role);
                        break;
                }

                uow.Save();

                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        private void CreateClientProfile(UserDto userDto, ApplicationUser applicationUser)
        {
            ClientProfile profile = new ClientProfile
            {
                Id = applicationUser.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Patronymic = userDto.Patronymic
            };

            // add role
            uow.UserManager.AddToRole(applicationUser.Id, userDto.Role);
            uow.ClientProfiles.Create(profile);
        }

        private void CreateProviderProfile(UserDto userDto, ApplicationUser applicationUser)
        {
            ProviderProfile profile = new ProviderProfile
            {
                Id = applicationUser.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Patronymic = userDto.Patronymic
            };

            // add role
            uow.UserManager.AddToRole(applicationUser.Id, userDto.Role);
            uow.ProviderProfiles.Create(profile);
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

            Create(admin);
        }

        public ApplicationUser GetById(string id)
        {
            ApplicationUser user = uow.UserManager.Users.FirstOrDefault(m => m.Id == id);

            return user;
        }

        public IEnumerable<ProviderProfile> GetAllProviders()
        {
            var providers = uow.ProviderProfiles.GetAll();

            return providers;
        }

        public IEnumerable<ProviderProfile> FindProviders(Func<ProviderProfile, bool> predicate)
        {
            var result = uow.ProviderProfiles.Find(predicate);
            return result;
        }

        public void UpdateProvider(ProviderProfile provider)
        {
            uow.ProviderProfiles.Update(provider);
            uow.Save();
        }

        public void UpdateClient(ClientProfile client)
        {
            uow.ClientProfiles.Update(client);
            uow.Save();
        }

        public void UpdateUser(ApplicationUser user)
        {
            uow.UserManager.Update(user);
            uow.Save();
        }
    }
}
