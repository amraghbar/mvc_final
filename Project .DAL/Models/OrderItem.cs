using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_.DAL.Models
{
     public  class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("ProductBase")]
        public int ProductBaseId { get; set; }
        public Order Order { get; set; }
        public ProductBase ProductBase { get; set; }
    }
}
