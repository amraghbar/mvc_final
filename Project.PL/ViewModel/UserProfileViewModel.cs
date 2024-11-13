using Project_.DAL.Models;

namespace Project.PL.ViewModel
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<CartItem> CartItems { get; set; } // يمثل العناصر في سلة الشراء
        public List<FavoriteItem> FavoriteItems { get; set; }
        public List<Order> Orders { get; set; }

    }
}
