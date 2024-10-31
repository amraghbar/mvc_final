using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Project.DAl.Data;
using Project_.DAL.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class CartController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ApplicationDbContext context;

    public CartController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
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

        // احصل على السلة الخاصة بالمستخدم واسترجع المنتجات الموجودة فيها
        var cart = await context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product) // تأكد من تضمين المنتج للحصول على بياناته
            .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

        if (cart == null || cart.CartItems.Count == 0)
        {
            return RedirectToAction("index", "Users"); 
                }

        return View(cart); // عرض السلة مع المنتجات
    }


    public async Task<IActionResult> Add(int productId, string productType, int qty)
    {
        // احصل على المستخدم الحالي
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Accounts");
        }

        // تحقق من وجود السلة لهذا المستخدم، وإن لم توجد أنشئ واحدة جديدة
        var cart = await context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

        if (cart == null)
        {
            cart = new Cart
            {
                ApplicationUserId = user.Id
            };
            context.Carts.Add(cart);
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

        // تحقق من وجود العنصر بالفعل في السلة، إن لم يكن موجودًا قم بإضافته
        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                CartId = cart.CartId,
                ProductId = productId,
                Quantity = qty // استخدم الكمية المرسلة
            };
            context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity = qty; // أضف الكمية المرسلة
        }

        await context.SaveChangesAsync(); // احفظ التغييرات
        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Decrease(int cartItemId, int newQuantity)
    {
        var cartItem = await context.CartItems.FindAsync(cartItemId);
        if (cartItem != null)
        {
            if (newQuantity <= 0) // Optionally, remove item if quantity goes to zero
            {
                context.CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = newQuantity;
                context.CartItems.Update(cartItem);
            }
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Increase(int cartItemId, int newQuantity)
    {
        var cartItem = await context.CartItems.FindAsync(cartItemId);
        if (cartItem != null)
        {
            cartItem.Quantity = newQuantity;
            context.CartItems.Update(cartItem);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveItem(int id)
    {
        var cartItem = await context.CartItems.FindAsync(id);
        if (cartItem != null)
        {
            context.CartItems.Remove(cartItem);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }




}
