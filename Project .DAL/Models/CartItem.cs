using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_.DAL.Models
{
    public class CartItem
    {

        public int Id { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string UserId { get; set; }
        public DateTime DateAdded { get; set; }

        // Navigation Property للإشارة إلى المنتج
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
