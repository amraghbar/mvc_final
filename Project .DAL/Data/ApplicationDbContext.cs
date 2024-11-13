using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project_.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Project.DAl.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteItem>()
                .HasKey(f => f.FavoriteItemId);
        }

        // DbSets
        public DbSet<Service> Services { get; set; }
        public DbSet<NewProd> NewProducts { get; set; }
        public DbSet<latest> Latests { get; set; }
        public DbSet<Inspired> Inspireds { get; set; }
        public DbSet<Featured> Featureds { get; set; }
        public DbSet<Offer> Offerss { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductBase> ProductBases { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }



        // طريقة SeedData
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // تأكد من وجود دور Admin
            string roleName = "Admin";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // إنشاء مستخدم Admin إذا لم يكن موجودًا
            var adminEmail = "amrtaghbar@gmail.com"; // استبدل هذا بعنوان البريد الإلكتروني الخاص بك
            var adminPassword = "Admin@123!123"; // استخدم كلمة مرور قوية

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                await userManager.CreateAsync(adminUser, adminPassword);
                await userManager.AddToRoleAsync(adminUser, roleName);
            }
        }
    }
}
