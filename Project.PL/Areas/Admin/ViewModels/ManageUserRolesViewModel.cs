using System.ComponentModel.DataAnnotations;

namespace Project.PL.Areas.Admin.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; } // This property should already be here

        public List<RoleSelection> Roles { get; set; } = new List<RoleSelection>(); // Initialize the list

        // Validation for role selection
        [Display(Name = "At least one role must be selected.")]
        public bool IsRoleSelected => Roles != null && Roles.Any(r => r.IsSelected);
    }
}
