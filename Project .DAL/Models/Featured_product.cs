using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_.DAL.Models
{
    internal class Featured_product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Af_Price { get; set; }
        public string Be_Price { get; set; }


        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
