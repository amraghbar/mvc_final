using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.Service;
using Project.PL.Helpers;
using Project_.DAL.Models;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ServiceController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var services = context.Services.ToList();
            var serviceVMs = mapper.Map<IEnumerable<ServiceVM>>(services);
            return View(serviceVMs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceFormVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = new Service
            {
                Name = model.Name,
                Description = model.Description,
                // Handle file upload if needed
            };

            if (model.Image != null)
            {
                // Logic to handle file upload and set the image path/name
                string newFileName = FilesSettings.UploadFile(model.Image, "images");
                service.ImageName = newFileName;
            }

            context.Services.Add(service);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var service = context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(mapper.Map<ServiceDetailsVM>(service));
        }

        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var service = context.Services.Find(id);
            if (service is null)
            {
                return RedirectToAction(nameof(Index));
            }
            FilesSettings.DeleteFile(service.ImageName, "images");
            context.Services.Remove(service);
            context.SaveChanges();
            return Ok(new { message = "service deleted" });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var service = context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(mapper.Map<ServiceEditVM>(service));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceEditVM model)
        {
            // تحقق من القيم المستلمة
            Console.WriteLine($"Id: {model.Id}, Name: {model.Name}, Description: {model.Description}, IsDeleted: {model.IsDeleted}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState errors: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))));
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }


            var service = await context.Services.FindAsync(model.Id);
            if (service == null)
            {
                return Json(new { success = false, errors = new[] { "Service not found." } });
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrWhiteSpace(service.ImageName))
                {
                    FilesSettings.DeleteFile(service.ImageName, "images");
                }

                string newFileName = FilesSettings.UploadFile(model.Image, "images");
                service.ImageName = newFileName;
            }

            mapper.Map(model, service);
            await context.SaveChangesAsync();
            return Json(new { success = true });

            
        }

    }
}
