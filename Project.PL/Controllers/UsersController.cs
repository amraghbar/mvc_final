using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Models;
using Project.PL.ViewModels;
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
            var newProd=context.NewProducts.ToList();
            var Inspireds = context.Inspireds.ToList();
            var latestBlog =context.Latests.ToList();
            var Off=context.Offerss.ToList();
            var combinedViewModel = new CombinedViewModel
            {
                FeaturedProducts = featuredProducts,
                Services = serviceVM,
                NewProducts = newProd,
                Inspireds = Inspireds,
                latestBlo = latestBlog,
                Offerss = Off,

            };

            return View(combinedViewModel);
        }


        public IActionResult Details(int id)
        {
            var product = context.Featureds.FirstOrDefault(p => p.Id == id)
                          ?? context.NewProducts.FirstOrDefault(p => p.Id == id)
                          ?? context.Inspireds.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); // إذا لم يتم العثور على المنتج
            }

            var productViewModel = mapper.Map<ProductDetailsViewModel>(product);

            return View(productViewModel);
        }




    }
}
