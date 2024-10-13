using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.Offer;
using Project.PL.Areas.Admin.ViewModels.Offers;
using Project.PL.Helpers;
using Project_.DAL.Models;
using System.Linq;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class OffersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public OffersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var Offers = context.Offerss.ToList();
            var Offersblog = mapper.Map<IEnumerable<OffersVM>>(Offers);
            return View(Offersblog);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OffersFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.ImageName = FilesSettings.UploadFile(vm.Image, "images");

            var Offersblog = mapper.Map<Offer>(vm);
            context.Add(Offersblog);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var Offers = context.Offerss.Find(id);
            if (Offers == null)
            {
                return NotFound();
            }

            return View(mapper.Map<OffersDetailsVM>(Offers));
        }



        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var offer = context.Offerss.Find(id);
            if (offer == null)
            {
                return NotFound(); 
            }

            if (!string.IsNullOrEmpty(offer.ImageName))
            {
                FilesSettings.DeleteFile(offer.ImageName, "images");
            }

            context.Offerss.Remove(offer);
            context.SaveChanges();

            return Ok(new { message = "Offer deleted successfully" }); 
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Offers = context.Offerss.Find(id);
            if (Offers == null)
            {
                return NotFound();
            }
            return View(mapper.Map<OffersEditVM>(Offers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OffersEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Offers = await context.Offerss.FindAsync(model.Id);
            if (Offers == null)
            {
                return NotFound();
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrWhiteSpace(Offers.ImageName))
                {
                    FilesSettings.DeleteFile(Offers.ImageName, "images");
                }

                string newFileName = FilesSettings.UploadFile(model.Image, "images");
                model.ImageName = newFileName;
            }
            else
            {
                model.ImageName = Offers.ImageName; // Ensure the image name remains the same if no new image is uploaded
            }

            mapper.Map(model, Offers);
            await context.SaveChangesAsync();

            return Json(new { success = true });
        }

    }
}

