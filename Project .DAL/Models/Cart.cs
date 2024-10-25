namespace Project_.DAL.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}

