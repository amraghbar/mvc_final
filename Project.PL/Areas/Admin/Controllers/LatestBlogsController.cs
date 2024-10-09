using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;

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
            return View();
        }
    }
}
