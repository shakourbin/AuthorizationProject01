using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // List all roles
    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    // Create a new role
    public IActionResult CreateRole() => View();

    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        if (!string.IsNullOrWhiteSpace(roleName))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View();
    }

    // Assign a role to a user
    public IActionResult AssignRole() => View();

    [HttpPost]
    public async Task<IActionResult> AssignRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && !string.IsNullOrWhiteSpace(roleName))
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        else
        {
            ModelState.AddModelError("", "User or Role not found.");
        }
        return View();
    }
}


//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//[Authorize(Roles = "Admin")]
//public class RoleController : Controller
//{
//    private readonly RoleManager<IdentityRole> _roleManager;
//    private readonly UserManager<IdentityUser> _userManager;

//    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
//    {
//        _roleManager = roleManager;
//        _userManager = userManager;
//    }

//    public async Task<IActionResult> CreateRole(string roleName)
//    {
//        if (!await _roleManager.RoleExistsAsync(roleName))
//        {
//            await _roleManager.CreateAsync(new IdentityRole(roleName));
//        }
//        return RedirectToAction("Index", "Home");
//    }

//    public async Task<IActionResult> AssignRole(string email, string roleName)
//    {
//        var user = await _userManager.FindByEmailAsync(email);
//        if (user != null)
//        {
//            await _userManager.AddToRoleAsync(user, roleName);
//        }
//        return RedirectToAction("Index", "Home");
//    }
//}