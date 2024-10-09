using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]

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
            return View();
        }
    }
}
