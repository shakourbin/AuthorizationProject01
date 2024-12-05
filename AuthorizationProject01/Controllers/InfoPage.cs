using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize(Roles = "User")]
public class InfoPageController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly UserClaimsService _userClaimsService;
   // private readonly IHttpContextAccessor _httpContextAccessor;

    public InfoPageController(UserManager<IdentityUser> userManager, UserClaimsService userClaimsService /*IHttpContextAccessor httpContextAccessor*/)
    {
        _userManager = userManager;
        // _httpContextAccessor = httpContextAccessor;
        _userClaimsService = userClaimsService;
    }
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var userClaims = await _userClaimsService.GetUserClaimsAsync(user.Id);

        // Determine field permissions
        var viewName = userClaims.Any(c => c.Value == "ViewName");
        var editName = userClaims.Any(c => c.Value == "EditName");

        var viewSurname = userClaims.Any(c => c.Value == "ViewSurname");
        var editSurname = userClaims.Any(c => c.Value == "EditSurname");

        var viewBirthday = userClaims.Any(c => c.Value == "ViewBirthday");
        var editBirthday = userClaims.Any(c => c.Value == "EditBirthday");

        var viewBankAccount = userClaims.Any(c => c.Value == "ViewBankAccount");
        var editBankAccount = userClaims.Any(c => c.Value == "EditBankAccount");

        // Pass permissions to the view
        var model = new FieldPermissionsViewModel
        {
            CanViewName = viewName,
            CanEditName = editName,
            CanViewSurname = viewSurname,
            CanEditSurname = editSurname,
            CanViewBirthday = viewBirthday,
            CanEditBirthday = editBirthday,
            CanViewBankAccount = viewBankAccount,
            CanEditBankAccount = editBankAccount
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Submit(string name, string surname, DateTime birthday, int bankAccount)
    {
        // Process the submitted data
        // Save changes to the database if allowed
        return RedirectToAction("Index");
    }
}
