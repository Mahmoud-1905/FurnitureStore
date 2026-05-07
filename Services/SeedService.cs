
using FurnitureStore.Data;
using FurnitureStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                // Ensure the database is ready and migrated
                logger.LogInformation("Migrating the database.");
                await context.Database.MigrateAsync();

                // ── Seed Roles ──────────────────────────────────────
                logger.LogInformation("Seeding roles.");
                await AddRoleAsync(roleManager, "Customer");
                await AddRoleAsync(roleManager, "StoreManager");
                await AddRoleAsync(roleManager, "Admin");

                // ── Seed Admin Account ──────────────────────────────
                logger.LogInformation("Seeding admin user.");
                var adminEmail = "admin@ruqistore.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new ApplicationUser
                    {
                        FullName = "Ruqi Admin",
                        UserName = adminEmail,
                        NormalizedUserName = adminEmail.ToUpper(),
                        Email = adminEmail,
                        NormalizedEmail = adminEmail.ToUpper(),
                        EmailConfirmed = true,
                        IsActive = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin@123456");
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

                // ── Seed Categories ─────────────────────────────────
                if (!context.Categories.Any())
                {
                    logger.LogInformation("Seeding categories.");
                    context.Categories.AddRange(
                        new Category { Name = "Living Room", Description = "Sofas, armchairs, coffee tables, and more for your living space", SortOrder = 1 },
                        new Category { Name = "Bedroom", Description = "Beds, dressers, nightstands, and bedroom accessories", SortOrder = 2 },
                        new Category { Name = "Dining Room", Description = "Dining tables, chairs, and sideboards", SortOrder = 3 },
                        new Category { Name = "Accent & Decor", Description = "Decorative items, mirrors, lighting, and accent pieces", SortOrder = 4 }
                    );
                    await context.SaveChangesAsync();
                    logger.LogInformation("Categories seeded successfully.");
                }

                // ── Seed Products ───────────────────────────────────
                if (!context.Products.Any())
                {
                    logger.LogInformation("Seeding products.");

                    // Get category IDs
                    var livingRoom = await context.Categories.FirstAsync(c => c.Name == "Living Room");
                    var bedroom = await context.Categories.FirstAsync(c => c.Name == "Bedroom");
                    var diningRoom = await context.Categories.FirstAsync(c => c.Name == "Dining Room");
                    var accentDecor = await context.Categories.FirstAsync(c => c.Name == "Accent & Decor");

                    context.Products.AddRange(
                        new Product { Name = "طاولة كونسول عصرية بتصميم بسيط", Price = 500, ImageUrl = "~/images/1.jpg", Description = "طاولة كونسول عصرية", CategoryId = livingRoom.CategoryId, SKU = "LR-001", StockQuantity = 10 },
                        new Product { Name = "جزيرة مطبخ فاخرة", Price = 200, ImageUrl = "~/images/2.jpeg", Description = "جزيرة مطبخ فاخرة", CategoryId = diningRoom.CategoryId, SKU = "DR-001", StockQuantity = 5 },
                        new Product { Name = "كنب زاوية (L-Shape) باللون الكريمي", Price = 150, ImageUrl = "~/images/3.jpg", Description = "كنب زاوية مريح", CategoryId = livingRoom.CategoryId, SKU = "LR-002", StockQuantity = 8 },
                        new Product { Name = "كرسي أرجوحة (بيضة) من الخيزران", Price = 300, ImageUrl = "~/images/4.jpg", Description = "كرسي أرجوحة مريح", CategoryId = livingRoom.CategoryId, SKU = "LR-003", StockQuantity = 12 },
                        new Product { Name = "طقم طعام رخامي دائري", Price = 400, ImageUrl = "~/images/5.jpg", Description = "طقم طعام فاخر", CategoryId = diningRoom.CategoryId, SKU = "DR-002", StockQuantity = 6 },
                        new Product { Name = "خزانة ركن القهوة المدمجة", Price = 120, ImageUrl = "~/images/6.jpg", Description = "خزانة ركن القهوة", CategoryId = livingRoom.CategoryId, SKU = "LR-004", StockQuantity = 15 },
                        new Product { Name = "طقم طاولات خشبية متداخلة", Price = 220, ImageUrl = "~/images/7.jpg", Description = "طقم طاولات خشبية", CategoryId = livingRoom.CategoryId, SKU = "LR-005", StockQuantity = 10 },
                        new Product { Name = "أريكة مخملية منحنية باللون الأخضر الزمردي", Price = 180, ImageUrl = "~/images/8.jpg", Description = "أريكة مخملية فاخرة", CategoryId = livingRoom.CategoryId, SKU = "LR-006", StockQuantity = 7, Material = "Velvet" },
                        new Product { Name = "كرسي مبتكر على شكل ورقة الشجر", Price = 140, ImageUrl = "~/images/9.jpg", Description = "كرسي بتصميم مبتكر", CategoryId = livingRoom.CategoryId, SKU = "LR-007", StockQuantity = 9 },
                        new Product { Name = "تماثيل ومزهريات سيراميك تجريدية", Price = 260, ImageUrl = "~/images/10.jpg", Description = "ديكورات سيراميك", CategoryId = accentDecor.CategoryId, SKU = "AD-001", StockQuantity = 20 },
                        new Product { Name = "طاولة زينة مع مرآة مضيئة", Price = 400, ImageUrl = "~/images/11.jpg", Description = "طاولة زينة", CategoryId = bedroom.CategoryId, SKU = "BR-001", StockQuantity = 5 },
                        new Product { Name = "خزانة عرض خشبية بسيطة", Price = 90, ImageUrl = "~/images/12.jpg", Description = "خزانة عرض", CategoryId = livingRoom.CategoryId, SKU = "LR-008", StockQuantity = 14 },
                        new Product { Name = "مكتب منزلي متكامل موفر للمساحة", Price = 500, ImageUrl = "~/images/13.jpg", Description = "مكتب منزلي", CategoryId = bedroom.CategoryId, SKU = "BR-002", StockQuantity = 8 },
                        new Product { Name = "طاولة زينة بتصميم منتصف القرن", Price = 210, ImageUrl = "~/images/14.jpg", Description = "طاولة زينة كلاسيكية", CategoryId = bedroom.CategoryId, SKU = "BR-003", StockQuantity = 6 },
                        new Product { Name = "كنب زاوية أبيض مع طاولة خشبية", Price = 130, ImageUrl = "~/images/15.jpg", Description = "كنب زاوية أبيض", CategoryId = livingRoom.CategoryId, SKU = "LR-009", StockQuantity = 10 },
                        new Product { Name = "مرآة مضيئة بحواف متموجة", Price = 170, ImageUrl = "~/images/16.jpg", Description = "مرآة مضيئة", CategoryId = accentDecor.CategoryId, SKU = "AD-002", StockQuantity = 18 },
                        new Product { Name = "كنب مودرن بألوان البيج والبني", Price = 280, ImageUrl = "~/images/17.jpg", Description = "كنب مودرن", CategoryId = livingRoom.CategoryId, SKU = "LR-010", StockQuantity = 7 },
                        new Product { Name = "مقعد مدخل ومرآة بتصميم ريفي حديث", Price = 350, ImageUrl = "~/images/18.jpg", Description = "مقعد مدخل", CategoryId = livingRoom.CategoryId, SKU = "LR-011", StockQuantity = 4 },
                        new Product { Name = "طاولة زينة دائرية مخططة", Price = 320, ImageUrl = "~/images/19.jpeg", Description = "طاولة زينة دائرية", CategoryId = bedroom.CategoryId, SKU = "BR-004", StockQuantity = 5 },
                        new Product { Name = "كرسي الفقاعة المريح", Price = 75, ImageUrl = "~/images/20.jpg", Description = "كرسي الفقاعة", CategoryId = livingRoom.CategoryId, SKU = "LR-012", StockQuantity = 11 },
                        new Product { Name = "أريكة مخملية خضراء كلاسيكية", Price = 200, ImageUrl = "~/images/21.jpg", Description = "أريكة كلاسيكية", CategoryId = livingRoom.CategoryId, SKU = "LR-013", StockQuantity = 6, Material = "Velvet" },
                        new Product { Name = "كراسي طعام مبطنة عصرية", Price = 60, ImageUrl = "~/images/22.jpeg", Description = "كراسي طعام", CategoryId = diningRoom.CategoryId, SKU = "DR-003", StockQuantity = 30 },
                        new Product { Name = "تجويف جداري مقوس ومضيء", Price = 180, ImageUrl = "~/images/23.jpeg", Description = "ديكور جداري", CategoryId = accentDecor.CategoryId, SKU = "AD-003", StockQuantity = 12 },
                        new Product { Name = "أرفف جدارية بتصميم موجي", Price = 160, ImageUrl = "~/images/24.jpeg", Description = "أرفف جدارية", CategoryId = accentDecor.CategoryId, SKU = "AD-004", StockQuantity = 15 },
                        new Product { Name = "ثريات خشبية حلزونية", Price = 190, ImageUrl = "~/images/25.jpeg", Description = "إضاءة خشبية", CategoryId = accentDecor.CategoryId, SKU = "AD-005", StockQuantity = 8, Material = "Solid Oak" },
                        new Product { Name = "ساعة حائط سينغولاريتاس", Price = 140, ImageUrl = "~/images/26.jpeg", Description = "ساعة حائط", CategoryId = accentDecor.CategoryId, SKU = "AD-006", StockQuantity = 20 },
                        new Product { Name = "ساعة رخامية بإضاءة خلفية", Price = 220, ImageUrl = "~/images/27.jpeg", Description = "ساعة رخامية", CategoryId = accentDecor.CategoryId, SKU = "AD-007", StockQuantity = 10 },
                        new Product { Name = "مزهرية هندسية ملتوية", Price = 90, ImageUrl = "~/images/28.jpeg", Description = "مزهرية هندسية", CategoryId = accentDecor.CategoryId, SKU = "AD-008", StockQuantity = 25 },
                        new Product { Name = "مصباح أرضي بشكل القمر", Price = 260, ImageUrl = "~/images/29.jpeg", Description = "مصباح أرضي", CategoryId = accentDecor.CategoryId, SKU = "AD-009", StockQuantity = 7 },
                        new Product { Name = "نافورة مياه داخلية", Price = 180, ImageUrl = "~/images/30.jpeg", Description = "نافورة مياه", CategoryId = accentDecor.CategoryId, SKU = "AD-010", StockQuantity = 5 },
                        new Product { Name = "بوفيه عصري مع مرآة عضوية", Price = 320, ImageUrl = "~/images/31.jpeg", Description = "بوفيه عصري", CategoryId = livingRoom.CategoryId, SKU = "LR-014", StockQuantity = 4 },
                        new Product { Name = "بوفيه خشبي بتصميم كلاسيكي", Price = 300, ImageUrl = "~/images/32.jpeg", Description = "بوفيه خشبي", CategoryId = livingRoom.CategoryId, SKU = "LR-015", StockQuantity = 6, Material = "Solid Oak" },
                        new Product { Name = "بوفيه بلمسة لامعة فاخرة", Price = 310, ImageUrl = "~/images/33.jpeg", Description = "بوفيه فاخر", CategoryId = livingRoom.CategoryId, SKU = "LR-016", StockQuantity = 3 },
                        new Product { Name = "مرآة ذكية بشكل سحابة", Price = 200, ImageUrl = "~/images/34.jpeg", Description = "مرآة ذكية", CategoryId = accentDecor.CategoryId, SKU = "AD-011", StockQuantity = 9 },
                        new Product { Name = "مرآة مضيئة بحواف متموجة 2", Price = 210, ImageUrl = "~/images/35.jpeg", Description = "مرآة مضيئة", CategoryId = accentDecor.CategoryId, SKU = "AD-012", StockQuantity = 11 },
                        new Product { Name = "مرآة أطفال بشكل أرنب", Price = 170, ImageUrl = "~/images/36.jpeg", Description = "مرآة أطفال", CategoryId = accentDecor.CategoryId, SKU = "AD-013", StockQuantity = 14 },
                        new Product { Name = "طاولة سرير بشكل روبوت", Price = 150, ImageUrl = "~/images/37.jpeg", Description = "طاولة سرير", CategoryId = bedroom.CategoryId, SKU = "BR-005", StockQuantity = 10 },
                        new Product { Name = "مصباح أرضي بشكل غصن شجرة", Price = 260, ImageUrl = "~/images/38.jpeg", Description = "مصباح أرضي", CategoryId = accentDecor.CategoryId, SKU = "AD-014", StockQuantity = 6 },
                        new Product { Name = "مكتبة جدارية شجرة الحياة", Price = 350, ImageUrl = "~/images/39.jpeg", Description = "مكتبة جدارية", CategoryId = accentDecor.CategoryId, SKU = "AD-015", StockQuantity = 4 },
                        new Product { Name = "أرفف جدارية حلزونية", Price = 280, ImageUrl = "~/images/40.jpeg", Description = "أرفف جدارية", CategoryId = accentDecor.CategoryId, SKU = "AD-016", StockQuantity = 8 },
                        new Product { Name = "أرفف جدارية بشكل فراشة", Price = 270, ImageUrl = "~/images/41.jpeg", Description = "أرفف جدارية", CategoryId = accentDecor.CategoryId, SKU = "AD-017", StockQuantity = 7 },
                        new Product { Name = "أرفف خلية النحل الهندسية", Price = 120, ImageUrl = "~/images/42.jpeg", Description = "أرفف هندسية", CategoryId = accentDecor.CategoryId, SKU = "AD-018", StockQuantity = 18 },
                        new Product { Name = "رف عائم حلزوني بسيط", Price = 110, ImageUrl = "~/images/43.jpeg", Description = "رف عائم", CategoryId = accentDecor.CategoryId, SKU = "AD-019", StockQuantity = 20 },
                        new Product { Name = "حوامل نباتات جدارية", Price = 95, ImageUrl = "~/images/44.jpeg", Description = "حوامل نباتات", CategoryId = accentDecor.CategoryId, SKU = "AD-020", StockQuantity = 25 },
                        new Product { Name = "حامل شموع جداري هندسي", Price = 130, ImageUrl = "~/images/45.jpeg", Description = "حامل شموع", CategoryId = accentDecor.CategoryId, SKU = "AD-021", StockQuantity = 16 },
                        new Product { Name = "طقم فازات أرضية قطرة الندى", Price = 160, ImageUrl = "~/images/46.jpeg", Description = "فازات أرضية", CategoryId = accentDecor.CategoryId, SKU = "AD-022", StockQuantity = 12 },
                        new Product { Name = "مصباح أرضي بشكل زهرة الكالا", Price = 220, ImageUrl = "~/images/47.jpeg", Description = "مصباح أرضي", CategoryId = accentDecor.CategoryId, SKU = "AD-023", StockQuantity = 5 },
                        new Product { Name = "مصباح أرضي مقوس", Price = 240, ImageUrl = "~/images/48.jpeg", Description = "مصباح أرضي", CategoryId = accentDecor.CategoryId, SKU = "AD-024", StockQuantity = 6 },
                        new Product { Name = "طقم جلسة بلكونة أنيق", Price = 300, ImageUrl = "~/images/49.jpeg", Description = "جلسة بلكونة", CategoryId = livingRoom.CategoryId, SKU = "LR-017", StockQuantity = 4 },
                        new Product { Name = "مرآة مستطيلة مع رف مضيء", Price = 260, ImageUrl = "~/images/50.jpeg", Description = "مرآة مستطيلة", CategoryId = accentDecor.CategoryId, SKU = "AD-025", StockQuantity = 10 }
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