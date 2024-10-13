using System.ComponentModel.DataAnnotations;

namespace Project.PL.Areas.Admin.ViewModels.Featured
{
    public class FeaturedFormVM
    {

        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 10000, ErrorMessage = "يجب أن يكون السعر بين 0 و 10000.")]
        public decimal Af_Price { get; set; }

        [Range(0, 10000, ErrorMessage = "يجب أن يكون السعر بين 0 و 10000.")]
        public decimal Be_Price { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }
    }
}
