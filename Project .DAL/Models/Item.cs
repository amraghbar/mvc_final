using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_.DAL.Models
{
    public class Item : ProductBase
    {
        
        public string Af_Price { get; set; }
        public string Be_Price { get; set; }

        public string Description { get; set; }

      
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}

