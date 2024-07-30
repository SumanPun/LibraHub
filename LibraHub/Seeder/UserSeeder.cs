using LibraHub.Constants;
using LibraHub.Data;
using LibraHub.Models;
using LibraHub.Seeder.Interface;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace LibraHub.Seeder
{
    public class UserSeeder : IUserSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAdminUserAsync()
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            string[] roles = { UserRoles.Admin, UserRoles.User };
            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var admin = await _userManager.GetUsersInRoleAsync(UserRoles.Admin);
            if (!admin.Any())
            {
                var adminUser = new ApplicationUser()
                {
                    FirstName = "Super",
                    LastName = "Admin",
                    UserName = "superadmin",
                    Email = "superadmin@admin.com",
                    EmailConfirmed = true,
                    Status = true
                };
                var defaultPassword = "Admin@123";
                var result = await _userManager.CreateAsync(adminUser, defaultPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                }
            }
            tx.Complete();
        }
    }
}
