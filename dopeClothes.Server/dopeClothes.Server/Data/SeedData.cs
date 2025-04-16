//using dopeClothes.Server.Models;
//using Microsoft.AspNetCore.Identity;

//namespace dopeClothes.Server.Data
//{
//    public class SeedData
//    {
//        public static async Task InitializeAsync(IServiceProvider serviceProvider)
//        {
//            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//            string adminEmail = "admin@gmail.com";
//            string adminUserName = "admin123";
//            string adminPassword = "Heslo_123";
//            string adminRoleName = "Admin";

//            // Zkontrolujeme, zda role již existuje (kontrola podle názvu)
//            var existingRole = await roleManager.FindByNameAsync(adminRoleName);
//            if (existingRole == null)
//            {
//                var createRoleResult = await roleManager.CreateAsync(new IdentityRole(adminRoleName));
//                if (!createRoleResult.Succeeded)
//                {
//                    // Pokud se chyba týká duplicitního záznamu, můžeme ji ignorovat
//                    if (!createRoleResult.Errors.Any(e => e.Description.Contains("duplicate", StringComparison.OrdinalIgnoreCase)))
//                    {
//                        throw new Exception("Chyba při vytváření role: " +
//                            string.Join(", ", createRoleResult.Errors.Select(e => e.Description)));
//                    }
//                }
//            }

//            // Kontrola, zda admin uživatel již existuje podle e-mailu
//            var adminUser = await userManager.FindByEmailAsync(adminEmail);
//            if (adminUser == null)
//            {
//                adminUser = new ApplicationUser
//                {
//                    UserName = adminUserName,
//                    Email = adminEmail,
//                    EmailConfirmed = true
//                };

//                var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
//                if (!createUserResult.Succeeded)
//                {
//                    throw new Exception("Chyba při vytváření admin uživatele: " +
//                        string.Join(", ", createUserResult.Errors.Select(e => e.Description)));
//                }
//            }

            
//            if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
//            {
//                var addToRoleResult = await userManager.AddToRoleAsync(adminUser, adminRoleName);
//                if (!addToRoleResult.Succeeded)
//                {
//                    throw new Exception("Chyba při přidávání admin role k uživateli: " +
//                        string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
//                }
//            }
//        }
//    }
//}
