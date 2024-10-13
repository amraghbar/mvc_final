﻿namespace Project.PL.Areas.Admin.ViewModels.LatestBlogs
{
    public class LatestBlogsEditVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Author { get; set; }
        public int? CommentsCount { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}

