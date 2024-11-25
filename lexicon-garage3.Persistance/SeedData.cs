using lexicon_garage3.Core.Entities;
using lexicon_garage3.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace lexicon_garage3.Persistance
{
    public class SeedData
    {
        private static ApplicationDbContext context = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<Member> userManager = default!;

        public static async Task Init(ApplicationDbContext _context, IServiceProvider services)
        {
            context = _context;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            userManager = services.GetRequiredService<UserManager<Member>>();

            var roleNames = new[] { "User", "Admin" };
            var adminEmail = "admin@admin.com";
            var userEmail = "user@user.com";

            await AddRolesAsync(roleNames);

            var admin = await AddAccountAsync("Admin", "Adminsson", "198703012345", adminEmail, "PWadmin-123");
            var user = await AddAccountAsync("User", "Usersson", "198612015645", userEmail, "PWuser-123");

            // Manually insert the role assignments into AspNetUserRoles
            await AddUserToRole(admin, "Admin");
            await AddUserToRole(user, "User");
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
          
                if (await roleManager.RoleExistsAsync(roleName)) continue;

                // Create role
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded) throw new Exception($"Error creating role '{roleName}': {string.Join("\n", result.Errors)}");
            }
        }

        private static async Task<Member> AddAccountAsync(string fName, string lName, string personNumber, string accountEmail, string pw)
        {
            var found = await userManager.FindByEmailAsync(accountEmail);
            if (found != null) return found;

            var user = new Member
            {
                FirstName = fName,
                LastName = lName,
                PersonNumber = personNumber,
                //Email = accountEmail,
                UserName = accountEmail,
                //EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, pw);
            if (!result.Succeeded)
            {
                Console.WriteLine($"Error during seeding: {string.Join("\n", result.Errors)}");
                throw new Exception(string.Join("\n", result.Errors));
            }

            return user;
        }

 
        private static async Task AddUserToRole(Member user, string roleName)
        {
            // Fetch the RoleId
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception($"Role '{roleName}' not found.");
            }

            // Fetch the UserId
            var userId = user.Id;

            //var sql = $"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{userId}', '{role.Id}')";
            //await context.Database.ExecuteSqlRawAsync(sql);

            // Check if the User-Role mapping already exists
            var existsQuery = $@"
            SELECT COUNT(*)
            FROM AspNetUserRoles
            WHERE UserId = '{userId}' AND RoleId = '{role.Id}'";

            var exists = await context.Database.ExecuteSqlRawAsync(existsQuery);

            if (exists == 0) // If no such record exists, insert the new one
            {
                var insertQuery = $@"
            INSERT INTO AspNetUserRoles (UserId, RoleId)
            VALUES ('{userId}', '{role.Id}')";
                await context.Database.ExecuteSqlRawAsync(insertQuery);
            }
            else
            {
                Console.WriteLine($"User '{userId}' is already assigned to role '{roleName}'.");
            }
        }
    }
}
