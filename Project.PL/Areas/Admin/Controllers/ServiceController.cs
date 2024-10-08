using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.Service;
using Project.PL.Helpers;
using Project_.DAL.Models;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ServiceController(ApplicationDbContext context ,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var serr = context.Services.ToList();
            var ServiceVM = mapper.Map<IEnumerable<ServiceVM>>(serr);
            return View(ServiceVM);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceFormVM vm)
        {


            if(!ModelState.IsValid)
            {
                return View(vm);

            }
            vm.ImageName = FilesSettings.UploadFile(vm.Image, "images");

            var ser = mapper.Map<Service>(vm);
            context.Add(ser);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public IActionResult Details(int id) {
            var x = context.Services.Find(id);
            if(x == null)
            {
                return NotFound();
            }
           
            
            return View(mapper.Map<ServiceDetailsVM>(x));

        }
        [HttpGet]
        public IActionResult Delete(int id) {
            var x = context.Services.Find(id);
            if (x == null)
            {
                return NotFound();
            }
            return View(mapper.Map<ServiceVM>(x));
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var x = context.Services.Find(id);
            if (x == null)
            {
                return NotFound();
            }
            return View(mapper.Map<ServiceEditVM>(x));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = await context.Services.FindAsync(model.Id);
            if (service == null)
            {
                return NotFound();
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
            else
            {
                model.ImageName = service.ImageName;
            }

            mapper.Map(model, service);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}