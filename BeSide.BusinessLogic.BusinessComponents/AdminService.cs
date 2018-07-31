using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;
using Microsoft.AspNet.Identity;

namespace BeSide.BusinessLogic.BusinessComponents
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdminService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            var  allUsers = userManager.Users.ToList();
            return allUsers;
        }
    }
}
