using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.Item;
using Project.PL.Areas.Admin.ViewModels.Product;
using Project.PL.Helpers;
using Project_.DAL.Models;
//InspiredProducts
namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ItemsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

		public IActionResult Index()
		{
			var inspir = context.Items.Include(i => i.Product).ToList();
			var inspiredProd = inspir.Select(item => new ItemVM
			{
				Id = item.Id,
				Name = item.Name,
				IsDeleted = item.IsDeleted,
				ProductId = item.ProductId, // تأكد من أنه int؟ إذا كنت تحتاجه
				ProductName = item.Product.Name_Product // افترض أن لديك علاقة مع كيان Product
			}).ToList();

			return View(inspiredProd);
		}



		[HttpGet]
        public IActionResult Create()
        {
            var VM = new ItemFormVM
            {
                ProductsVM = mapper.Map<IEnumerable<ProductsVM>>(context.Products.ToList())
            };

            if (VM.ProductsVM == null || !VM.ProductsVM.Any())
            {
                ModelState.AddModelError("", "No products found.");
            }

            return View(VM);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.ImageName = FilesSettings.UploadFile(vm.Image, "images");

			var InspiredProduVM = mapper.Map<Item>(vm);
			InspiredProduVM.ProductId = vm.ProductsId; // تأكد من تعيين ProductsId
			
			context.Add(InspiredProduVM);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            var product = context.Products.Find(item.ProductId);
            string productName = product != null ? product.Name_Product : "Unknown Product";

            var itemDetailsVM = mapper.Map<ItemDetailsVM>(item);
            itemDetailsVM.ProductName = productName; // تعيين اسم المنتج

            return View(itemDetailsVM);
        }




        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var Inspired = context.Items.Find(id);
            if (Inspired is null)
            {
                return NotFound();
            }

            FilesSettings.DeleteFile(Inspired.ImageName, "images");
            context.Items.Remove(Inspired);
            context.SaveChanges();

            return Ok(new { message = "Service deleted" });
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Inspired = context.Items.Find(id);
            if (Inspired == null)
            {
                return NotFound();
            }
            return View(mapper.Map<ItemEditVM>(Inspired));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Inspired = await context.Items.FindAsync(model.Id);
            if (Inspired == null)
            {
                return NotFound();
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrWhiteSpace(Inspired.ImageName))
                {
                    FilesSettings.DeleteFile(Inspired.ImageName, "images");
                }

                string newFileName = FilesSettings.UploadFile(model.Image, "images");
                model.ImageName = newFileName;
            }
            else
            {
                model.ImageName = Inspired.ImageName; // Ensure the image name remains the same if no new image is uploaded
            }

            mapper.Map(model, Inspired);
            await context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
