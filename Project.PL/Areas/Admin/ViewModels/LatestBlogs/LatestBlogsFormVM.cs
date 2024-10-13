using System.ComponentModel.DataAnnotations;

namespace Project.PL.Areas.Admin.ViewModels.LatestBlogs
{
    public class LatestBlogsFormVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int CommentsCount { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }
    }
}
