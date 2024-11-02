using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.Featured;
using Project.PL.Helpers;
using Project_.DAL.Models;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FeaturedsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public FeaturedsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var featureds = context.Featureds.ToList();
            var featuredVMs = mapper.Map<IEnumerable<FeaturedVM>>(featureds);
            return View(featuredVMs); 
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FeaturedFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.ImageName = FilesSettings.UploadFile(vm.Image, "images");

            var featured = mapper.Map<Featured>(vm);
            context.Add(featured);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var featured = context.Featureds.Find(id);
            if (featured == null)
            {
                return NotFound();
            }

            return View(mapper.Map<FeaturedDetailsVM>(featured));
        }

        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var featured = context.Featureds.Find(id);
            if (featured is null)
            {
                return NotFound(); 
            }

            FilesSettings.DeleteFile(featured.ImageName, "images");
            context.Featureds.Remove(featured);
            context.SaveChanges();

            return Ok(new { message = "Service deleted" });
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var featured = context.Featureds.Find(id);
            if (featured == null)
            {
                return NotFound();
            }
            return View(mapper.Map<FeaturedEditVM>(featured));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FeaturedEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var featured = await context.Featureds.FindAsync(model.Id);
            if (featured == null)
            {
                return NotFound();
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrWhiteSpace(featured.ImageName))
                {
                    FilesSettings.DeleteFile(featured.ImageName, "images");
                }

                string newFileName = FilesSettings.UploadFile(model.Image, "images");
                model.ImageName = newFileName;
            }
            else
            {
                model.ImageName = featured.ImageName; // Ensure the image name remains the same if no new image is uploaded
            }

            mapper.Map(model, featured);
            await context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
