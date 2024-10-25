using Project_.DAL.Models; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

            // إعداد العلاقة بين Cart و ApplicationUser
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany() // يمكن تخصيص هذه العلاقة حسب الحاجة
                .HasForeignKey(c => c.UserId);

            // إعداد العلاقة بين CartItem و Product
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany() // يمكن تخصيص هذه العلاقة حسب الحاجة
                .HasForeignKey(ci => ci.ProductId);
        }
        public DbSet<Service> Services { get; set; }
        public DbSet<NewProd> NewProducts { get; set; }
        public DbSet<latest> Latests { get; set; }
        public DbSet<Inspired> Inspireds { get; set; }
        public DbSet<Featured> Featureds { get; set; }
        public DbSet<Offer> Offerss { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
         public DbSet<Item> Items { get; set; }
        
        public DbSet<Cart> Carts { get; set; }

    }

}
