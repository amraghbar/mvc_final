using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Project.PL.ViewModel;
using Project_.DAL.Models;

namespace Project.PL.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var serviceVM = context.Services.ToList();
            var featuredProducts = context.Featureds.ToList();
            var newProd = context.NewProducts.ToList();
            var inspireds = context.Inspireds.ToList();
            var latestBlog = context.Latests.ToList();
            var off = context.Offerss.ToList();

            var combinedViewModel = new CombinedViewModel
            {
                FeaturedProducts = featuredProducts,
                Services = serviceVM,
                NewProducts = newProd,
                Inspireds = inspireds,
                LatestBlogs = latestBlog,
                Offers = off,
            };

            return View(combinedViewModel);
        }

        public IActionResult Details(string modelName, int id)
        {
            ProductBase product = null; // Use ProductBase as the type

            switch (modelName)
            {
                case "Inspireds":
                    product = context.Inspireds.FirstOrDefault(p => p.Id == id);
                    break;
                case "Featureds":
                    product = context.Featureds.FirstOrDefault(p => p.Id == id);
                    break;
                case "NewProducts":
                    product = context.NewProducts.FirstOrDefault(p => p.Id == id);
                    break;
                default:
                    return NotFound();
            }

            if (product == null)
            {
                return NotFound(); // Check if the product exists
            }

            ViewBag.Product = product;
            ViewBag.ModelName = modelName; // Pass the model type for use in the view

            return View();
        }
    }
}
