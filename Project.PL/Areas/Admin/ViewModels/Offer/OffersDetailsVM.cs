namespace Project.PL.Areas.Admin.ViewModels.Offers
{
    public class OffersDetailsVM
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
      

        public string? ImageName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
