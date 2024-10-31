using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_.DAL.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime CreatedAt { get; set; }  // تأكد من وجود هذه الخاصية
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<FavoriteItem> FavoriteItems { get; set; }
    }
}
