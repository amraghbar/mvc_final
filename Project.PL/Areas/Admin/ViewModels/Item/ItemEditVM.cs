﻿namespace Project.PL.Areas.Admin.ViewModels.Item
{ 
public class ItemEditVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}

