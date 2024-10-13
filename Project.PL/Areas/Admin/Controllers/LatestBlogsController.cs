using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.LatestBlogs;
using Project.PL.Helpers;
using Project_.DAL.Models;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]

  
    public class LatestBlogsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LatestBlogsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var Latests = context.Latests.ToList();
            var Latestsblog = mapper.Map<IEnumerable<LatestBlogsVM>>(Latests);
            return View(Latestsblog);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LatestBlogsFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.ImageName = FilesSettings.UploadFile(vm.Image, "images");

            var Latestsblog = mapper.Map<latest>(vm);
            context.Add(Latestsblog);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var Latests = context.Latests.Find(id);
            if (Latests == null)
            {
                return NotFound();
            }

            return View(mapper.Map<LatestBlogsDetailsVM>(Latests));
        }

        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var Latests = context.Latests.Find(id);
            if (Latests is null)
            {
                return NotFound();
            }

            FilesSettings.DeleteFile(Latests.ImageName, "images");
            context.Latests.Remove(Latests);
            context.SaveChanges();

            return Ok(new { message = "Service deleted" });
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Latests = context.Latests.Find(id);
            if (Latests == null)
            {
                return NotFound();
            }
            return View(mapper.Map<LatestBlogsEditVM>(Latests));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LatestBlogsEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Latests = await context.Latests.FindAsync(model.Id);
            if (Latests == null)
            {
                return NotFound();
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrWhiteSpace(Latests.ImageName))
                {
                    FilesSettings.DeleteFile(Latests.ImageName, "images");
                }

                string newFileName = FilesSettings.UploadFile(model.Image, "images");
                model.ImageName = newFileName;
            }
            else
            {
                model.ImageName = Latests.ImageName; // Ensure the image name remains the same if no new image is uploaded
            }

            mapper.Map(model, Latests);
            await context.SaveChangesAsync();

            return Json(new { success = true });
        }

    }
}

