
using FurnitureStore.Data;
using FurnitureStore.Models.UserRoles.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Models;

namespace FurnitureStore.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                // Ensure the database is ready and migrated
                logger.LogInformation("Migrating the database.");
                await context.Database.MigrateAsync();

                // Add roles
                logger.LogInformation("Seeding roles.");
                await AddRoleAsync(roleManager, "Admin");
                await AddRoleAsync(roleManager, "User");

                // Add admin user
                logger.LogInformation("Seeding admin user.");
                var adminEmail = "admin@codehub.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new Users
                    {
                        FullName = "Code Hub",
                        UserName = adminEmail,
                        NormalizedUserName = adminEmail.ToUpper(),
                        Email = adminEmail,
                        NormalizedEmail = adminEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin@123");
                    if (result.Succeeded)
                    {
                        logger.LogInformation("Assigning Admin role to the admin user.");
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }

                // add products if none exist
                if (!context.Products.Any())
                {
                    logger.LogInformation("Seeding products.");
                    context.Products.AddRange(
                        new Product { Name = "طاولة كونسول عصرية بتصميم بسيط", Price = 500, ImageUrl = "~/images/1.jpg", Description = "طاولة كونسول عصرية", Category = "Tables" },
                        new Product { Name = "جزيرة مطبخ فاخرة", Price = 200, ImageUrl = "~/images/2.jpeg", Description = "جزيرة مطبخ فاخرة", Category = "Kitchen" },
                        new Product { Name = "كنب زاوية (L-Shape) باللون الكريمي", Price = 150, ImageUrl = "~/images/3.jpg", Description = "كنب زاوية مريح", Category = "Sofas" },
                        new Product { Name = "كرسي أرجوحة (بيضة) من الخيزران", Price = 300, ImageUrl = "~/images/4.jpg", Description = "كرسي أرجوحة مريح", Category = "Chairs" },
                        new Product { Name = "طقم طعام رخامي دائري", Price = 400, ImageUrl = "~/images/5.jpg", Description = "طقم طعام فاخر", Category = "Dining" },
                        new Product { Name = "خزانة ركن القهوة المدمجة", Price = 120, ImageUrl = "~/images/6.jpg", Description = "خزانة ركن القهوة", Category = "Cabinets" },
                        new Product { Name = "طقم طاولات خشبية متداخلة", Price = 220, ImageUrl = "~/images/7.jpg", Description = "طقم طاولات خشبية", Category = "Tables" },
                        new Product { Name = "أريكة مخملية منحنية باللون الأخضر الزمردي", Price = 180, ImageUrl = "~/images/8.jpg", Description = "أريكة مخملية فاخرة", Category = "Sofas" },
                        new Product { Name = "كرسي مبتكر على شكل ورقة الشجر", Price = 140, ImageUrl = "~/images/9.jpg", Description = "كرسي بتصميم مبتكر", Category = "Chairs" },
                        new Product { Name = "تماثيل ومزهريات سيراميك تجريدية", Price = 260, ImageUrl = "~/images/10.jpg", Description = "ديكورات سيراميك", Category = "Decor" },
                        new Product { Name = "طاولة زينة مع مرآة مضيئة", Price = 400, ImageUrl = "~/images/11.jpg", Description = "طاولة زينة", Category = "Tables" },
                        new Product { Name = "خزانة عرض خشبية بسيطة", Price = 90, ImageUrl = "~/images/12.jpg", Description = "خزانة عرض", Category = "Cabinets" },
                        new Product { Name = "مكتب منزلي متكامل موفر للمساحة", Price = 500, ImageUrl = "~/images/13.jpg", Description = "مكتب منزلي", Category = "Offices" },
                        new Product { Name = "طاولة زينة بتصميم منتصف القرن", Price = 210, ImageUrl = "~/images/14.jpg", Description = "طاولة زينة كلاسيكية", Category = "Tables" },
                        new Product { Name = "كنب زاوية أبيض مع طاولة خشبية", Price = 130, ImageUrl = "~/images/15.jpg", Description = "كنب زاوية أبيض", Category = "Sofas" },
                        new Product { Name = "مرآة مضيئة بحواف متموجة", Price = 170, ImageUrl = "~/images/16.jpg", Description = "مرآة مضيئة", Category = "Decor" },
                        new Product { Name = "كنب مودرن بألوان البيج والبني", Price = 280, ImageUrl = "~/images/17.jpg", Description = "كنب مودرن", Category = "Sofas" },
                        new Product { Name = "مقعد مدخل ومرآة بتصميم ريفي حديث", Price = 350, ImageUrl = "~/images/18.jpg", Description = "مقعد مدخل", Category = "Furniture" },
                        new Product { Name = "طاولة زينة دائرية مخططة", Price = 320, ImageUrl = "~/images/19.jpeg", Description = "طاولة زينة دائرية", Category = "Tables" },
                        new Product { Name = "كرسي الفقاعة المريح", Price = 75, ImageUrl = "~/images/20.jpg", Description = "كرسي الفقاعة", Category = "Chairs" },
                        new Product { Name = "أريكة مخملية خضراء كلاسيكية", Price = 200, ImageUrl = "~/images/21.jpg", Description = "أريكة كلاسيكية", Category = "Sofas" },
                        new Product { Name = "كراسي طعام مبطنة عصرية", Price = 60, ImageUrl = "~/images/22.jpeg", Description = "كراسي طعام", Category = "Chairs" },
                        new Product { Name = "تجويف جداري مقوس ومضيء", Price = 180, ImageUrl = "~/images/23.jpeg", Description = "ديكور جداري", Category = "Decor" },
                        new Product { Name = "أرفف جدارية بتصميم موجي", Price = 160, ImageUrl = "~/images/24.jpeg", Description = "أرفف جدارية", Category = "Decor" },
                        new Product { Name = "ثريات خشبية حلزونية", Price = 190, ImageUrl = "~/images/25.jpeg", Description = "إضاءة خشبية", Category = "Lighting" },
                        new Product { Name = "ساعة حائط سينغولاريتاس", Price = 140, ImageUrl = "~/images/26.jpeg", Description = "ساعة حائط", Category = "Decor" },
                        new Product { Name = "ساعة رخامية بإضاءة خلفية", Price = 220, ImageUrl = "~/images/27.jpeg", Description = "ساعة رخامية", Category = "Decor" },
                        new Product { Name = "مزهرية هندسية ملتوية", Price = 90, ImageUrl = "~/images/28.jpeg", Description = "مزهرية هندسية", Category = "Decor" },
                        new Product { Name = "مصباح أرضي بشكل القمر", Price = 260, ImageUrl = "~/images/29.jpeg", Description = "مصباح أرضي", Category = "Lighting" },
                        new Product { Name = "نافورة مياه داخلية", Price = 180, ImageUrl = "~/images/30.jpeg", Description = "نافورة مياه", Category = "Decor" },
                        new Product { Name = "بوفيه عصري مع مرآة عضوية", Price = 320, ImageUrl = "~/images/31.jpeg", Description = "بوفيه عصري", Category = "Furniture" },
                        new Product { Name = "بوفيه خشبي بتصميم كلاسيكي", Price = 300, ImageUrl = "~/images/32.jpeg", Description = "بوفيه خشبي", Category = "Furniture" },
                        new Product { Name = "بوفيه بلمسة لامعة فاخرة", Price = 310, ImageUrl = "~/images/33.jpeg", Description = "بوفيه فاخر", Category = "Furniture" },
                        new Product { Name = "مرآة ذكية بشكل سحابة", Price = 200, ImageUrl = "~/images/34.jpeg", Description = "مرآة ذكية", Category = "Decor" },
                        new Product { Name = "مرآة مضيئة بحواف متموجة", Price = 210, ImageUrl = "~/images/35.jpeg", Description = "مرآة مضيئة", Category = "Decor" },
                        new Product { Name = "مرآة أطفال بشكل أرنب", Price = 170, ImageUrl = "~/images/36.jpeg", Description = "مرآة أطفال", Category = "Decor" },
                        new Product { Name = "طاولة سرير بشكل روبوت", Price = 150, ImageUrl = "~/images/37.jpeg", Description = "طاولة سرير", Category = "Tables" },
                        new Product { Name = "مصباح أرضي بشكل غصن شجرة", Price = 260, ImageUrl = "~/images/38.jpeg", Description = "مصباح أرضي", Category = "Lighting" },
                        new Product { Name = "مكتبة جدارية شجرة الحياة", Price = 350, ImageUrl = "~/images/39.jpeg", Description = "مكتبة جدارية", Category = "Decor" },
                        new Product { Name = "أرفف جدارية حلزونية", Price = 280, ImageUrl = "~/images/40.jpeg", Description = "أرفف جدارية", Category = "Decor" },
                        new Product { Name = "أرفف جدارية بشكل فراشة", Price = 270, ImageUrl = "~/images/41.jpeg", Description = "أرفف جدارية", Category = "Decor" },
                        new Product { Name = "أرفف خلية النحل الهندسية", Price = 120, ImageUrl = "~/images/42.jpeg", Description = "أرفف هندسية", Category = "Decor" },
                        new Product { Name = "رف عائم حلزوني بسيط", Price = 110, ImageUrl = "~/images/43.jpeg", Description = "رف عائم", Category = "Decor" },
                        new Product { Name = "حوامل نباتات جدارية", Price = 95, ImageUrl = "~/images/44.jpeg", Description = "حوامل نباتات", Category = "Decor" },
                        new Product { Name = "حامل شموع جداري هندسي", Price = 130, ImageUrl = "~/images/45.jpeg", Description = "حامل شموع", Category = "Decor" },
                        new Product { Name = "طقم فازات أرضية قطرة الندى", Price = 160, ImageUrl = "~/images/46.jpeg", Description = "فازات أرضية", Category = "Decor" },
                        new Product { Name = "مصباح أرضي بشكل زهرة الكالا", Price = 220, ImageUrl = "~/images/47.jpeg", Description = "مصباح أرضي", Category = "Lighting" },
                        new Product { Name = "مصباح أرضي مقوس", Price = 240, ImageUrl = "~/images/48.jpeg", Description = "مصباح أرضي", Category = "Lighting" },
                        new Product { Name = "طقم جلسة بلكونة أنيق", Price = 300, ImageUrl = "~/images/49.jpeg", Description = "جلسة بلكونة", Category = "Furniture" },
                        new Product { Name = "مرآة مستطيلة مع رف مضيء", Price = 260, ImageUrl = "~/images/50.jpeg", Description = "مرآة مستطيلة", Category = "Decor" }
                    );
                    await context.SaveChangesAsync();
                    logger.LogInformation("Products seeded successfully.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}