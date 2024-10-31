using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Project_.DAL.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Microsoft.AspNetCore.Authorization;

public class FavoritesController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ApplicationDbContext context;

    public FavoritesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        this.userManager = userManager;
        this.context = context;
    }

    public async Task<IActionResult> Index()
    {
        // احصل على المستخدم الحالي
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Accounts");
        }

        // احصل على المفضلات الخاصة بالمستخدم واسترجع المنتجات الموجودة فيها
        var favorite = await context.Favorites
            .Include(f => f.FavoriteItems)
            .ThenInclude(fi => fi.Product) // تأكد من تضمين المنتج للحصول على بياناته
            .FirstOrDefaultAsync(f => f.ApplicationUserId == user.Id);

        if (favorite == null || favorite.FavoriteItems.Count == 0)
        {
            return View(); // عرض صفحة مفضلات فارغة
        }

        return View(favorite); // عرض المفضلات مع المنتجات
    }

    public async Task<IActionResult> Add(int productId, string productType)
    {
        // احصل على المستخدم الحالي
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Accounts");
        }

        // تحقق من وجود المفضلة لهذا المستخدم، وإن لم توجد أنشئ واحدة جديدة
        var favorite = await context.Favorites
            .Include(f => f.FavoriteItems)
            .FirstOrDefaultAsync(f => f.ApplicationUserId == user.Id);

        if (favorite == null)
        {
            favorite = new Favorite
            {
                ApplicationUserId = user.Id,
                CreatedAt = DateTime.Now,
                FavoriteItems = new List<FavoriteItem>()
            };
            context.Favorites.Add(favorite);
            await context.SaveChangesAsync();
        }

        // ابحث عن المنتج المطلوب إضافته بناءً على النوع
        ProductBase product = null;

        if (productType == "Featured")
        {
            product = await context.Featureds.FindAsync(productId);
        }
        else if (productType == "Inspired")
        {
            product = await context.Inspireds.FindAsync(productId);
        }
        else if (productType == "NewProd")
        {
            product = await context.NewProducts.FindAsync(productId);
        }
        else if (productType == "Item")
        {
            product = await context.Items.FindAsync(productId);
        }

        if (product == null)
        {
            return NotFound("المنتج غير موجود أو نوعه غير صحيح.");
        }

        // تحقق من وجود العنصر بالفعل في المفضلة، إن لم يكن موجودًا قم بإضافته
        var favoriteItem = favorite.FavoriteItems.FirstOrDefault(fi => fi.ProductId == productId);
        if (favoriteItem == null)
        {
            favoriteItem = new FavoriteItem
            {
                FavoriteId = favorite.Id, // استخدام ID المفضلة
                ProductId = productId
            };
            context.FavoriteItems.Add(favoriteItem);
        }

        await context.SaveChangesAsync(); // احفظ التغييرات
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemoveItem(int id)
    {
        var favoriteItem = await context.FavoriteItems.FindAsync(id);
        if (favoriteItem != null)
        {
            context.FavoriteItems.Remove(favoriteItem);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
