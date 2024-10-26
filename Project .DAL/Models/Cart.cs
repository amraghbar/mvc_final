using Project.DAl.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_.DAL.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public IList<CartItem> CartItem { get; set; } = new List<CartItem>();
    }
}

