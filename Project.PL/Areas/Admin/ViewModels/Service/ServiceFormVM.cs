namespace Project.PL.Areas.Admin.ViewModels.Service
{
    public class ServiceFormVM
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }
    }
}
