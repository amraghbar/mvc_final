using Project_.DAL.Models;
using Project.DAl.Data;

namespace Project_.BLL
{
    public class CartService
    {
        private readonly ApplicationDbContext context;

        public CartService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddToCart(string userId, int productId, string modelName, int quantity)
        {
            var product = GetProductByIdAndModel(productId, modelName);
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            var cartItem = new CartItem
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = quantity,
                UserId = userId,
                DateAdded = DateTime.Now
            };

            var existingCartItem = context.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId && ci.UserId == userId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                context.CartItems.Add(cartItem);
            }

            context.SaveChanges();
        }

        private dynamic GetProductByIdAndModel(int productId, string modelName)
        {
            switch (modelName)
            {
                case "Inspireds":
                    return context.Inspireds.FirstOrDefault(p => p.Id == productId);
                case "Featureds":
                    return context.Featureds.FirstOrDefault(p => p.Id == productId);
                case "NewProducts":
                    return context.NewProducts.FirstOrDefault(p => p.Id == productId);
                default:
                    return null;
            }
        }
    }
}
