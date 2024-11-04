using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.PL.Areas.Admin.ViewModels;
using Project_.DAL.Models;

namespace Project.PL.Areas.Admin.Controllers
{
   [Area("Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    // عرض قائمة المستخدمين
    public async Task<IActionResult> Index()
    {
        var users = userManager.Users.ToList();
        var userRolesViewModel = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            userRolesViewModel.Add(new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles
            });
        }

        return View(userRolesViewModel);
    }

        // تعديل الأدوار لمستخدم معين
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var model = new ManageUserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = new List<RoleSelection>()
            };

            // Using a foreach loop with await instead of Select with .Result
            foreach (var role in roleManager.Roles)
            {
                var isInRole = await userManager.IsInRoleAsync(user, role.Name);
                model.Roles.Add(new RoleSelection
                {
                    RoleName = role.Name,
                    IsSelected = isInRole
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles(ManageUserRolesViewModel model)
        {
            if (model == null || model.Roles == null)
            {
                return BadRequest("Invalid model or roles data.");
            }

            // Check if at least one role is selected
            if (!model.IsRoleSelected)
            {
                ModelState.AddModelError("Roles", "At least one role must be selected.");
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest($"Model state is invalid: {string.Join(", ", errors)}");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest($"Model state is invalid: {string.Join(", ", errors)}");
            }

            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            // Updating user roles
            foreach (var role in model.Roles)
            {
                if (role.IsSelected && !await userManager.IsInRoleAsync(user, role.RoleName))
                {
                    await userManager.AddToRoleAsync(user, role.RoleName);
                }
                else if (!role.IsSelected && await userManager.IsInRoleAsync(user, role.RoleName))
                {
                    await userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
            }

            return RedirectToAction(nameof(Index));
        }


    }

}
