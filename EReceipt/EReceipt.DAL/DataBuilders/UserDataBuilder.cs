using System.Collections.Generic;
using System.Linq;
using EReceipt.Common.Authentication;
using EReceipt.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace EReceipt.DAL.DataBuilders
{
    public class UserDataBuilder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManage;

        public UserDataBuilder(UserManager<User> userManager, RoleManager<IdentityRole> roleManage)
        {
            _userManager = userManager;
            _roleManage = roleManage;
        }

        public void SetData()
        {
            if (!EnumerableExtensions.Any(_roleManage.Roles))
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole(Role.Doctor),
                    new IdentityRole(Role.Patient),
                    new IdentityRole(Role.Pharmacy),
                };

                roles.ForEach(r =>
                {
                    _roleManage.CreateAsync(r).GetAwaiter().GetResult();
                });
            }

            var admin = _userManager.FindByNameAsync("ADMIN").Result;

            if (admin == null)
            {
                var user = new User
                {
                    Id = "21878405-d63d-4fc3-ab47-1244124af98d",
                    UserName = "ADMIN",
                    Email = "admin@admin.com"
                };

                _userManager.CreateAsync(user, "19086467Yes").GetAwaiter().GetResult();

                _roleManage.CreateAsync(new IdentityRole(Role.Admin)).GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(user, Role.Admin).GetAwaiter().GetResult();
                //  _userManager.AddToRoleAsync(user, Role.Lead).GetAwaiter().GetResult();
                // _userManager.AddToRoleAsync(user, Role.Developer).GetAwaiter().GetResult();
            }
        }
    }
}
