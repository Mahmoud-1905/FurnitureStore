using FurnitureStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using FurnitureStore.Data;


namespace FurnitureStore.Controllers
{

    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = GetProducts();
            return View(products);
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "طاولة كونسول عصرية بتصميم بسيط", Price = 500, ImageUrl = "~/images/1.jpg" },
                new Product { Id = 2, Name = "جزيرة مطبخ فاخرة", Price = 200, ImageUrl = "~/images/2.jpeg" },
                new Product { Id = 3, Name = "كنب زاوية (L-Shape) باللون الكريمي", Price = 150, ImageUrl = "~/images/3.jpg" },
                new Product { Id = 4, Name = "كرسي أرجوحة (بيضة) من الخيزران", Price = 300, ImageUrl = "~/images/4.jpg" },
                new Product { Id = 5, Name = "طقم طعام رخامي دائري", Price = 400, ImageUrl = "~/images/5.jpg" },

                new Product { Id = 6, Name = "خزانة ركن القهوة المدمجة", Price = 120, ImageUrl = "~/images/6.jpg" },
                new Product { Id = 7, Name = "طقم طاولات خشبية متداخلة", Price = 220, ImageUrl = "~/images/7.jpg" },
                new Product { Id = 8, Name = "أريكة مخملية منحنية باللون الأخضر الزمردي", Price = 180, ImageUrl = "~/images/8.jpg" },
                new Product { Id = 9, Name = "كرسي مبتكر على شكل ورقة الشجر", Price = 140, ImageUrl = "~/images/9.jpg" },
                new Product { Id = 10, Name = "تماثيل ومزهريات سيراميك تجريدية", Price = 260, ImageUrl = "~/images/10.jpg" },

                new Product { Id = 11, Name = "طاولة زينة مع مرآة مضيئة", Price = 400, ImageUrl = "~/images/11.jpg" },
                new Product { Id = 12, Name = "خزانة عرض خشبية بسيطة", Price = 90, ImageUrl = "~/images/12.jpg" },
                new Product { Id = 13, Name = "مكتب منزلي متكامل موفر للمساحة", Price = 500, ImageUrl = "~/images/13.jpg" },
                new Product { Id = 14, Name = "طاولة زينة بتصميم منتصف القرن", Price = 210, ImageUrl = "~/images/14.jpg" },
                new Product { Id = 15, Name = "كنب زاوية أبيض مع طاولة خشبية", Price = 130, ImageUrl = "~/images/15.jpg" },

                new Product { Id = 16, Name = "مرآة مضيئة بحواف متموجة", Price = 170, ImageUrl = "~/images/16.jpg" },
                new Product { Id = 17, Name = "كنب مودرن بألوان البيج والبني", Price = 280, ImageUrl = "~/images/17.jpg" },
                new Product { Id = 18, Name = "مقعد مدخل ومرآة بتصميم ريفي حديث", Price = 350, ImageUrl = "~/images/18.jpg" },
                new Product { Id = 19, Name = "طاولة زينة دائرية مخططة", Price = 320, ImageUrl = "~/images/19.jpeg" },
                new Product { Id = 20, Name = "كرسي الفقاعة المريح", Price = 75, ImageUrl = "~/images/20.jpg" },

                new Product { Id = 21, Name = "أريكة مخملية خضراء كلاسيكية", Price = 200, ImageUrl = "~/images/21.jpg" },
                new Product { Id = 22, Name = "كراسي طعام مبطنة عصرية", Price = 60, ImageUrl = "~/images/22.jpeg" },

                new Product { Id = 23, Name = "تجويف جداري مقوس ومضيء", Price = 180, ImageUrl = "~/images/23.jpeg" },
                new Product { Id = 24, Name = "أرفف جدارية بتصميم موجي", Price = 160, ImageUrl = "~/images/24.jpeg" },
                new Product { Id = 25, Name = "ثريات خشبية حلزونية", Price = 190, ImageUrl = "~/images/25.jpeg" },
                new Product { Id = 26, Name = "ساعة حائط سينغولاريتاس", Price = 140, ImageUrl = "~/images/26.jpeg" },
                new Product { Id = 27, Name = "ساعة رخامية بإضاءة خلفية", Price = 220, ImageUrl = "~/images/27.jpeg" },
                new Product { Id = 28, Name = "مزهرية هندسية ملتوية", Price = 90, ImageUrl = "~/images/28.jpeg" },
                new Product { Id = 29, Name = "مصباح أرضي بشكل القمر", Price = 260, ImageUrl = "~/images/29.jpeg" },
                new Product { Id = 30, Name = "نافورة مياه داخلية", Price = 180, ImageUrl = "~/images/30.jpeg" },

                new Product { Id = 31, Name = "بوفيه عصري مع مرآة عضوية", Price = 320, ImageUrl = "~/images/31.jpeg" },
                new Product { Id = 32, Name = "بوفيه خشبي بتصميم كلاسيكي", Price = 300, ImageUrl = "~/images/32.jpeg" },
                new Product { Id = 33, Name = "بوفيه بلمسة لامعة فاخرة", Price = 310, ImageUrl = "~/images/33.jpeg" },
                new Product { Id = 34, Name = "مرآة ذكية بشكل سحابة", Price = 200, ImageUrl = "~/images/34.jpeg" },
                new Product { Id = 35, Name = "مرآة مضيئة بحواف متموجة", Price = 210, ImageUrl = "~/images/35.jpeg" },
                new Product { Id = 36, Name = "مرآة أطفال بشكل أرنب", Price = 170, ImageUrl = "~/images/36.jpeg" },
                new Product { Id = 37, Name = "طاولة سرير بشكل روبوت", Price = 150, ImageUrl = "~/images/37.jpeg" },
                new Product { Id = 38, Name = "مصباح أرضي بشكل غصن شجرة", Price = 260, ImageUrl = "~/images/38.jpeg" },
                new Product { Id = 39, Name = "مكتبة جدارية شجرة الحياة", Price = 350, ImageUrl = "~/images/39.jpeg" },
                new Product { Id = 40, Name = "أرفف جدارية حلزونية", Price = 280, ImageUrl = "~/images/40.jpeg" },

                new Product { Id = 41, Name = "أرفف جدارية بشكل فراشة", Price = 270, ImageUrl = "~/images/41.jpeg" },
                new Product { Id = 42, Name = "أرفف خلية النحل الهندسية", Price = 120, ImageUrl = "~/images/42.jpeg" },
                new Product { Id = 43, Name = "رف عائم حلزوني بسيط", Price = 110, ImageUrl = "~/images/43.jpeg" },
                new Product { Id = 44, Name = "حوامل نباتات جدارية", Price = 95, ImageUrl = "~/images/44.jpeg" },
                new Product { Id = 45, Name = "حامل شموع جداري هندسي", Price = 130, ImageUrl = "~/images/45.jpeg" },
                new Product { Id = 46, Name = "طقم فازات أرضية قطرة الندى", Price = 160, ImageUrl = "~/images/46.jpeg" },
                new Product { Id = 47, Name = "مصباح أرضي بشكل زهرة الكالا", Price = 220, ImageUrl = "~/images/47.jpeg" },
                new Product { Id = 48, Name = "مصباح أرضي مقوس", Price = 240, ImageUrl = "~/images/48.jpeg" },
                new Product { Id = 49, Name = "طقم جلسة بلكونة أنيق", Price = 300, ImageUrl = "~/images/49.jpeg" },
                new Product { Id = 50, Name = "مرآة مستطيلة مع رف مضيء", Price = 260, ImageUrl = "~/images/50.jpeg" }
            };
        }

       

    }
}