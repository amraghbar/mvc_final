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
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Accounts");
        }

        var cart = await context.Carts
            .Include(c => c.CartItem)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

        if (cart == null)
        {
            cart = new Cart { ApplicationUserId = user.Id };
            context.Carts.Add(cart);
            await context.SaveChangesAsync();
        }

        return View(cart);
    }

    public async Task<IActionResult> Add(int Id)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Accounts");
        }

        var product = await context.Featureds.FindAsync(Id);
        if (product == null)
        {
            return NotFound("Product not found.");
        }

        var cart = await context.Carts
            .Include(c => c.CartItem)
            .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

        if (cart == null)
        {
            cart = new Cart { ApplicationUserId = user.Id };
            context.Carts.Add(cart);
            await context.SaveChangesAsync();
        }

        var cartItem = cart.CartItem.FirstOrDefault(ci => ci.ProductId == Id);
        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                CartId = cart.CartId,
                ProductId = Id,
                Quantity = 1
            };
            context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity++;
            context.CartItems.Update(cartItem);
        }
        await context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public IActionResult Decrease(int cartItemId)
    {
        var cartItem = context.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);

        if (cartItem == null)
        {
            return NotFound("CartItem not found.");
        }

        if (cartItem.Quantity > 1)
        {
            cartItem.Quantity--;
            context.SaveChanges();
        }
        else
        {
            return BadRequest("Quantity must be greater than zero.");
        }

        return RedirectToAction("Index");
    }

    public IActionResult Increase(int cartItemId)
    {
        var cartItem = context.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);

        if (cartItem == null)
        {
            return NotFound("CartItem not found.");
        }

        cartItem.Quantity++;
        context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult RemoveItem(int Id)
    {
        var cartItem = context.CartItems.FirstOrDefault(ci => ci.CartItemId == Id);

        if (cartItem != null)
        {
            context.CartItems.Remove(cartItem);
            context.SaveChanges();
        }
        else
        {
            return NotFound("CartItem not found.");
        }

        return RedirectToAction(nameof(Index));
    }
}
