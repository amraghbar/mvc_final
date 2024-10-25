using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Project_.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.PL.Controllers
{
    public class ShopsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ShopsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
       
        public IActionResult Index(string sortOrder)
        {
            var items = context.Items.AsQueryable();

            // تطبيق ترتيب الفرز حسب الخيار المحدد
            switch (sortOrder)
            {
                case "price_asc":
                    items = items.OrderBy(i => i.Af_Price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(i => i.Af_Price);
                    break;
                case "name_asc":
                    items = items.OrderBy(i => i.Name);
                    break;
                case "name_desc":
                    items = items.OrderByDescending(i => i.Name);
                    break;
                default:
                    break;
            }

            return View( items.ToList()); 
        }
        [HttpGet]
        public IActionResult IndexAjax(string sortOrder)
        {
            var items = context.Items.AsQueryable();

            // تطبيق ترتيب الفرز حسب الخيار المحدد
            switch (sortOrder)
            {
                case "price_asc":
                    items = items.OrderBy(i => i.Af_Price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(i => i.Af_Price);
                    break;
                case "name_asc":
                    items = items.OrderBy(i => i.Name);
                    break;
                case "name_desc":
                    items = items.OrderByDescending(i => i.Name);
                    break;
                default:
                    break;
            }

            return PartialView("shop/_ProPartial", items.ToList());
        }
        public IActionResult Details(int id)
        {
            var item = context.Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            ViewBag.Product = item; // تأكد من تعيين المنتج في ViewBag
            return View(item); // مرر المنتج إلى الـ View
        }





    }
}
