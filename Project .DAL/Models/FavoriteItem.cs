using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_.DAL.Models
{
    public class FavoriteItem
    {
        public int FavoriteItemId { get; set; }

        [ForeignKey("Favorite")]
        public int FavoriteId { get; set; }

        [ForeignKey("ProductBase")]
        public int ProductId { get; set; }

        public Favorite Favorite { get; set; }
        public ProductBase Product    { get; set; }
    }
}
