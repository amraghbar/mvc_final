using System.ComponentModel.DataAnnotations;

namespace Project.PL.Areas.Admin.ViewModels.Product
{
    public class ProductsFormVM
    {
        public int Id { get; set; }

        public string Name_Product { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
