using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.NewProduct;
using Project.PL.Helpers;
using Project_.DAL.Models;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewProductsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public NewProductsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var NewProduc = context.NewProducts.ToList();
            var NewProduct = mapper.Map<IEnumerable<NewProductVM>>(NewProduc);
            return View(NewProduct);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewProductFormVM vm)
        {
            if (!ModelState.IsValid)
            {

                return View(vm);
            }

            vm.ImageName = FilesSettings.UploadFile(vm.Image, "images");

            var NewProd = mapper.Map<NewProd>(vm);
            context.Add(NewProd);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult Details(int id)
        {
            var NewProducts = context.NewProducts.Find(id);
            if (NewProducts == null)
            {
                return NotFound();
            }

            return View(mapper.Map<NewProductDetailsVM>(NewProducts));
        }

        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var NewProducts = context.NewProducts.Find(id);
            if (NewProducts is null)
            {
                return NotFound();
            }

            FilesSettings.DeleteFile(NewProducts.ImageName, "images");
            context.NewProducts.Remove(NewProducts);
            context.SaveChanges();

            return Ok(new { message = "Service deleted" });
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var NewProducts = context.NewProducts.Find(id);
            if (NewProducts == null)
            {
                return NotFound();
            }
            return View(mapper.Map<NewProductEditVM>(NewProducts));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewProductEditVM model)
        {
            // التحقق من صحة النموذج
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }

            // البحث عن المنتج في قاعدة البيانات
            var newProduct = await context.NewProducts.FindAsync(model.Id);
            if (newProduct == null)
            {
                return NotFound();
            }

            // التحقق من صورة جديدة
            if (model.Image != null)
            {
                // حذف الصورة القديمة إذا كانت موجودة
                if (!string.IsNullOrWhiteSpace(newProduct.ImageName))
                {
                    FilesSettings.DeleteFile(newProduct.ImageName, "images");
                }

                // رفع الصورة الجديدة
                string newFileName = FilesSettings.UploadFile(model.Image, "images");
                newProduct.ImageName = newFileName; // تعيين الاسم الجديد للصورة
            }

            // استخدام AutoMapper لتحديث باقي الخصائص
            mapper.Map(model, newProduct);

            // حفظ التغييرات في قاعدة البيانات
            await context.SaveChangesAsync();

            // إعادة توجيه إلى صفحة الفهرس بعد النجاح
            return Json(new { success = true });
        }

    }
}

