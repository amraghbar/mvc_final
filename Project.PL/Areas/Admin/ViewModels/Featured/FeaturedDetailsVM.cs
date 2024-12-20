﻿namespace Project.PL.Areas.Admin.ViewModels.Featured
{
    public class FeaturedDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Af_Price { get; set; }
        public string Be_Price { get; set; }

        public string? ImageName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
