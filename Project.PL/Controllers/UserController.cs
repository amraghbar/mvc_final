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

        var cartItems = await context.Carts
            .Where(c => c.ApplicationUserId == user.Id)
            .Include(c => c.CartItems) // تأكد من تضمين العناصر
            .ThenInclude(ci => ci.Product) // إذا كنت تريد تضمين تفاصيل المنتج أيضًا
            .ToListAsync();

        var favoriteItems = await context.Favorites
            .Where(f => f.ApplicationUserId == user.Id)
            .Include(f => f.FavoriteItems) // تأكد من تضمين العناصر
            .ThenInclude(fi => fi.Product) // إذا كنت تريد تضمين تفاصيل المنتج أيضًا
            .ToListAsync();

        var viewModel = new UserProfileViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            CartItems = cartItems.SelectMany(c => c.CartItems).ToList(),
            FavoriteItems = favoriteItems.SelectMany(f => f.FavoriteItems).ToList()
        };

        return View(viewModel);
    }
}
