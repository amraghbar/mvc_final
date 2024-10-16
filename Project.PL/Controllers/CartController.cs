using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project_.DAL.Models;

public class CartController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CartController(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpPost]
  
    public IActionResult AddToCart(string modelName, int productId, int quantity)
    {
        try
        {
            var userId = GetUserId();
            object? product = null;

            if (modelName == "Inspireds")
            {
                product = context.Inspireds.FirstOrDefault(p => p.Id == productId);
            }
            else if (modelName == "Featureds")
            {
                product = context.Featureds.FirstOrDefault(p => p.Id == productId);
            }
            else if (modelName == "NewProducts")
            {
                product = context.NewProducts.FirstOrDefault(p => p.Id == productId);
            }

            if (product == null)
            {
                return NotFound();
            }

            var cartItem = mapper.Map<CartItem>(product);
            cartItem.Quantity = quantity;
            cartItem.UserId = userId;

            context.CartItems.Add(cartItem);
            context.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); 

            return StatusCode(500, "Internal Server Error");
        }
    }



    private int GetUserId()
    {
        return 1;
    }
}
