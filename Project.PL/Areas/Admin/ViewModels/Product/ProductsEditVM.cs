using Microsoft.AspNetCore.Http;

namespace Project.PL.Areas.Admin.ViewModels.Product
{
    public class ProductsEditVM
    {
        public int Id { get; set; }
        public string? Name_Product { get; set; }
       
        public bool? IsDeleted { get; set; }

       
    }
}
