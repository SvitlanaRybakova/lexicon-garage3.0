using lexicon_garage3.Core.Entities;
using lexicon_garage3.Persistance.Data;
using Microsoft.AspNetCore.Identity;
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

            if (context.Roles.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            userManager = services.GetRequiredService<UserManager<Member>>();


            var roleNames = new[] { "User", "Admin" };
            var adminEmail = "admin@admin.com";
            var userEmail = "user@user.com";

            await AddRolesAsync(roleNames);


            var admin = await AddAccountAsync("Admin", "Adminsson", "198703012345", adminEmail, "PWadmin-123");
            var user = await AddAccountAsync("User", "Usersson", "198612015645", userEmail, "PWuser-123");
            await AddUserToRoleAsync(admin, "Admin");
            await AddUserToRoleAsync(user, "User");
        }

      

        private static async Task AddUserToRoleAsync(Member user, string roleName)
        {
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                var result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task<Member> AddAccountAsync(string fName, string lName, string personNumber, string accountEmail, string pw)
        {
            var found = await userManager.FindByEmailAsync(accountEmail);
            if (found != null) return null!;

            var user = new Member
            {
                FirstName = fName,
                LastName = lName,
                PersonNumber = personNumber,
                Email = accountEmail,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, pw);
            if (!result.Succeeded)
            {
                Console.WriteLine($"Error during seeding: {string.Join("\n", result.Errors)}");
                throw new Exception(string.Join("\n", result.Errors));
            }

            return user;
        }
    }


}
