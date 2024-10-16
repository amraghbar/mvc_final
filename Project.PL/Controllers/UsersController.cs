using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Project.PL.Models;
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



        public IActionResult Details(string modelName, int id)
        {
            object? product = null;

            if (modelName == "Inspireds")
            {
                product = context.Inspireds.FirstOrDefault(p => p.Id == id);
            }
            else if (modelName == "Featureds")
            {
                product = context.Featureds.FirstOrDefault(p => p.Id == id);
            }
            else if (modelName == "NewProducts")
            {
                product = context.NewProducts.FirstOrDefault(p => p.Id == id);
            }
            else
            {
                return NotFound();
            }

            if (product == null)
            {
                return NotFound(); // تحقق من أن المنتج موجود
            }

            ViewBag.Product = product;
            ViewBag.ModelName = modelName; // تمرير نوع النموذج أيضا لعرضه في العرض أو إرساله لاحقًا

            return View();
        }









    }
}
