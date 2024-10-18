namespace Project.PL.Areas.Admin.ViewModels.Item
{
    public class ItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
		public int? ProductId{ get; set; }
		public string ProductName { get; set; }

	}
}
