using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.InspiredProducts;
using Project.PL.Helpers;
using Project_.DAL.Models;
//InspiredProducts
namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InspiredProductsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public InspiredProductsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var Inspir = context.Inspireds.ToList();
            var InspiredProd = mapper.Map<IEnumerable<InspiredProductsVM>>(Inspir);
            return View(InspiredProd);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InspiredProductsFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.ImageName = FilesSettings.UploadFile(vm.Image, "images");

            var InspiredProduVM = mapper.Map<Inspired>(vm);
            context.Add(InspiredProduVM);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var Inspired = context.Inspireds.Find(id);
            if (Inspired == null)
            {
                return NotFound();
            }

            return View(mapper.Map<InspiredProductsDetailsVM>(Inspired));
        }

        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var Inspired = context.Inspireds.Find(id);
            if (Inspired is null)
            {
                return NotFound();
            }

            FilesSettings.DeleteFile(Inspired.ImageName, "images");
            context.Inspireds.Remove(Inspired);
            context.SaveChanges();

            return Ok(new { message = "Service deleted" });
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Inspired = context.Inspireds.Find(id);
            if (Inspired == null)
            {
                return NotFound();
            }
            return View(mapper.Map<InspiredProductsEditVM>(Inspired));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InspiredProductsEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Inspired = await context.Inspireds.FindAsync(model.Id);
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
 