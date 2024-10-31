using Project.DAl.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_.DAL.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime CreatedAt { get; set; }  // تأكد من وجود هذه الخاصية
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}

