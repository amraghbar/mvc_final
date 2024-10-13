using System.ComponentModel.DataAnnotations;

namespace Project.PL.Areas.Admin.ViewModels.Offer
{
    public class OffersFormVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }
    }
}
