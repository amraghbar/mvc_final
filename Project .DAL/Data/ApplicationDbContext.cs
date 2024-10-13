using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_.DAL.Models;

namespace Project.DAl.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<NewProd> NewProducts { get; set; }
        public DbSet<latest> Latests { get; set; }
        public DbSet<Inspired> Inspireds { get; set; }
        public DbSet<Featured> Featureds { get; set; }

        public DbSet<Offer> Offerss { get; set; }


    }
}
