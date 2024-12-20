﻿namespace Project.PL.Areas.Admin.ViewModels.Item
{
    public class ItemDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Af_Price { get; set; }
        public string Be_Price { get; set; }

        public string? ImageName { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }

    }
}
