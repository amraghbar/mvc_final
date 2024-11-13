using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Project.PL.ViewModel;
using Project_.DAL.Models; // استبدل هذا بمساحة الأسماء الخاصة بك
using System.Linq;
using System.Threading.Tasks;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ApplicationDbContext context;
    
    public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        this.userManager = userManager;
        this.context = context;
    }

    public async Task<IActionResult> Profile()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Accounts");
        }

        // استعلام السلة
       

        // استعلام العناصر المفضلة
        var favoriteItems = await context.Favorites
            .Where(f => f.ApplicationUserId == user.Id)
            .Include(f => f.FavoriteItems) // تأكد من تضمين العناصر
            .ThenInclude(fi => fi.Product) // تضمين تفاصيل المنتج
            .ToListAsync();

        // استعلام الطلبات
        var orders = await context.Orders
            .Where(o => o.ApplicationUserId == user.Id)
            .Include(o => o.OrderItem) // تضمين عناصر الطلب
            .ThenInclude(oi => oi.ProductBase) // تضمين تفاصيل المنتج
            .ToListAsync();

        // بناء النموذج الذي سيتم تمريره إلى العرض
        var viewModel = new UserProfileViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FavoriteItems = favoriteItems.SelectMany(f => f.FavoriteItems).ToList(),
            Orders = orders // إضافة الطلبات إلى النموذج
        };

        return View(viewModel);
    }

}
