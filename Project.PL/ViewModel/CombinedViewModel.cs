using Microsoft.EntityFrameworkCore;
using Project_.DAL.Models;

namespace Project.PL.ViewModel
{
    public class CombinedViewModel
    {
        public List<Featured> FeaturedProducts { get; set; }
        public List<Service> Services { get; set; }
        public List<NewProd> NewProducts { get; set; }
       
        public List<Inspired> Inspireds { get; set; }
        public List<latest> latestBlo { get; set; }
        public List<Offer> Offerss { get; set; }

      
    }
}
