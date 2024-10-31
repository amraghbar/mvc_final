using Microsoft.EntityFrameworkCore;
using Project_.DAL.Models;

namespace Project.PL.ViewModel
{
    public class CombinedViewModel
    {
        public List<Service> Services { get; set; }
        public List<Featured> FeaturedProducts { get; set; }
        public List<NewProd> NewProducts { get; set; }
        public List<Inspired> Inspireds { get; set; }
        public List<latest> LatestBlogs { get; set; }
        public List<Offer> Offers { get; set; }
      
    }
}
