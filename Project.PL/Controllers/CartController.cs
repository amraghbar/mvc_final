using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Project.DAl.Data;
using Project_.BLL;
using Project_.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager; // استخدام UserManager لإدارة المستخدمين

    public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult AddToCart(string userId, int productId, int quantity)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == productId && !p.IsDeleted);
        if (product == null)
        {
            return NotFound("Product not found or unavailable.");
        }

        var cart = _context.Carts.Include(c => c.Items)
                                  .FirstOrDefault(c => c.UserId == userId)
                     ?? new Cart { UserId = userId };

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var cartItem = new CartItem
            {
                ProductId = productId,
                ProductName = product.Name_Product,
                Quantity = quantity,
                Price = (decimal)product.Price,
            };
            cart.Items.Add(cartItem);
        }

        // حفظ السلة في قاعدة البيانات
        if (_context.Carts.Any(c => c.UserId == userId))
        {
            _context.Carts.Update(cart);
        }
        else
        {
            _context.Carts.Add(cart);
        }

        _context.SaveChanges();
        return Ok(); // أو أي نتيجة مناسبة
    }

    public Cart GetCart(string userId)
    {
        return _context.Carts.Include(c => c.Items)
                             .FirstOrDefault(c => c.UserId == userId)
               ?? new Cart { UserId = userId };
    }
}
