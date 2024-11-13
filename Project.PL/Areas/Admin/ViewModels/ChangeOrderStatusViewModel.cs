namespace Project.PL.Areas.Admin.ViewModels
{
    public class ChangeOrderStatusViewModel
    {
        public string UserName { get; set; }
        public int OrderId { get; set; }
        public string CurrentStatus { get; set; }
        public string SelectedStatus { get; set; }
        public List<string> AvailableStatuses { get; set; }
    }
}
