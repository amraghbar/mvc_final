using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_.DAL.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        [ForeignKey("ProductBase")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Cart Cart { get; set; }
        public ProductBase Product
        {
            get; set;





        }
    }
}
