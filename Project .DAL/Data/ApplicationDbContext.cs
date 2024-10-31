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

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany() 
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteItem>()
             .HasKey(f => f.FavoriteItemId);
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
        public DbSet<ProductBase> ProductBases { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }

        public DbSet<Favorite> Favorites { get; set; }


    }

}
