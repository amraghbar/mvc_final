using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.PL.Models.ViewModel
{
    public class EditUserRoleViewModel
    {
        public string Id { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
        public string? SelectRoles { get; set; }
    }
}
