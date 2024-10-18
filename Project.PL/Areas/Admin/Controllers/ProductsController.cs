using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.Product;
using Project.PL.Helpers;
using Project_.DAL.Models;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProductsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = context.Products.ToList(); 
            var productVMs = mapper.Map<IEnumerable<ProductsVM>>(products);
            return View(productVMs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductsFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var product = mapper.Map<Product>(vm);


            try
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                // Log the inner exception message for further analysis
                Console.WriteLine(innerException?.Message);
                throw; // Optionally rethrow the exception after logging
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            // جلب المنتج بناءً على المعرف
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            // تحويل المنتج إلى ProductsDetailsVM
            return View(mapper.Map<ProductsDetailsVM>(product));
        }


        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var product = context.Products.Find(id);
            if (product is null)
            {
                return NotFound();
            }
            context.Products.Remove(product);
            context.SaveChanges();

            return Ok(new { message = "Product deleted" });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id); 

            if (product == null)
            {
                return NotFound();
            }

            var model = mapper.Map<ProductsEditVM>(product);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Edit(ProductsEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList() });
            }

            // جلب المنتج بواسطة معرفه
            var product = await context.Products.FindAsync(model.Id);
            if (product == null)
            {
                return NotFound();
            }

            // استخدام المابير لتحديث خصائص المنتج
            mapper.Map(model, product);

            // حفظ التغييرات
            await context.SaveChangesAsync();

            return Json(new { success = true });
        }

    }
}
