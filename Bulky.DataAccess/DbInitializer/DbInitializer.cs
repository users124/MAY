using MAY.DataAccess.Data;
using MAY.Models;
using MAY.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAY.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        { // Apply pending migrations if exist, else create database
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                    Console.WriteLine("it is empty");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying migrations: {ex.Message}");
            }
            // Create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

                // If roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@may.com",
                    Email = "admin@may.com",
                    Name = "Mohammed MAYAdmin",
                    PhoneNumber = "1234567890",
                    StreetAddress = "123 Main St",
                    City = "Anytown",
                    State = "Anystate",
                    PostalCode = "12345"
                }, "Admin123*").GetAwaiter().GetResult();

                // add admin user to admin role
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@may.com")!;
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }
            // 3. Categorieën Seeden
            if (!_db.Categories.Any())
            {
                _db.Categories.AddRange(new List<Category> {
            new Category { Name = "Zorg & Hygiëne", DisplayOrder = 1 },
            new Category { Name = "Huisdieren", DisplayOrder = 2 },
            new Category { Name = "Huishoudelijk", DisplayOrder = 3 },
            new Category { Name = "Sport & Spel", DisplayOrder = 4 }
        });
                _db.SaveChanges();
            }

            // 4. Companies (Suppliers/Klanten) Seeden
            if (!_db.Companies.Any())
            {
                _db.Companies.AddRange(new List<Company> {
            new Company { Name = "Alibaba Supplier A", StreetAddress = "Shenzhen Road 1", City = "Shenzhen", State = "GD", PostalCode = "518000", PhoneNumber = "86123456789" },
            new Company { Name = "Local Logistics Hub", StreetAddress = "Havenlaan 10", City = "Antwerpen", State = "AN", PostalCode = "2000", PhoneNumber = "031234567" }
        });
                _db.SaveChanges();
            }

            // 5. Producten Seeden
            if (!_db.Products.Any())
            {
                int catZorg = _db.Categories.FirstOrDefault(static u => u.Name == "Zorg & Hygiëne")!.Id;
                int catPets = _db.Categories.FirstOrDefault(static u => u.Name == "Huisdieren")!.Id;
                int catHome = _db.Categories.FirstOrDefault(static u => u.Name == "Huishoudelijk")!.Id;
                int catSport = _db.Categories.FirstOrDefault(static u => u.Name == "Sport & Spel")! .Id;

                _db.Products.AddRange(new List<Product> {
                new Product {
                        ProductName = "25x Incontinentie Wegwerp Onderleggers",
                        SKU = "MAY-INC-WEG-25",
                        EAN = "8720512345001",
                        ListPrice = 25, Price = 22, Price50 = 20, Price100 = 18,
                        CategoryId = catZorg,
                        Description = "Hoogwaardige wegwerp onderleggers voor eenmalig gebruik.",
                        ImageUrl=@"\images\product\wegwerp_onderleggers.jpg"
                    },
                    new Product {
                        ProductName = "Wasbare Incontinentie Onderlegger",
                        SKU = "MAY-INC-WAS-01",
                        EAN = "8720512345018",
                        ListPrice = 35, Price = 30, Price50 = 28, Price100 = 25,
                        CategoryId = catZorg,
                        Description = "Duurzame en comfortabele herbuikbare onderleggers.",
                        ImageUrl=@"\images\product\wasbare_onderlegger.jpg"
                    },
                    new Product {
                        ProductName = "Drinkfontein Kat",
                        SKU = "MAY-PET-FONT-01",
                        EAN = "8720512345025",
                        ListPrice = 45, Price = 40, Price50 = 38, Price100 = 35,
                        CategoryId = catPets,
                        Description = "Ultra-stille drinkfontein met koolstoffilter voor honden en katten.",
                        ImageUrl=@"\images\product\drinkfontein_kat.jpg"
                    },
                    new Product {
                        ProductName = "Stapelkit Wasmachine",
                        SKU = "MAY-HOME-STACK",
                        EAN = "8720512345032",
                        ListPrice = 55, Price = 50, Price50 = 45, Price100 = 40,
                        CategoryId = catHome,
                        Description = "Universele tussenstuk stapelkit voor wasmachine en droger.",
                        ImageUrl=@"\images\product\stapelkit.jpg"
                    },
                    new Product {
                        ProductName = "Dartbord Verlichting",
                        SKU = "MAY-SPORT-DART",
                        EAN = "8720512345049",
                        ListPrice = 85, Price = 75, Price50 = 70, Price100 = 65,
                        CategoryId = catSport,
                        Description = "Professionele LED Surround verlichting zonder schaduwvorming.",
                        ImageUrl=@"\images\product\dartbord.jpg"
                    }
                });
                    _db.SaveChanges();
             }
         return;
        }
     }
}
