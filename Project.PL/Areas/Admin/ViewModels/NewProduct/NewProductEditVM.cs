namespace Project.PL.Areas.Admin.ViewModels.NewProduct
{
    public class NewProductEditVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Af_Price { get; set; }
        public string? Be_Price { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}
