using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Security.Claims;

[Authorize(Roles = "Admin")]
public class ClaimController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public ClaimController(UserManager<IdentityUser> userManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> AddClaim()
    {
        List<ClaimList> claimList = await _dbContext.ClaimLists.ToListAsync();
        ViewBag.ClaimList = new SelectList(claimList, "Id", "ClaimValue");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddClaim(string email, string claimType, string ClaimValue)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && !string.IsNullOrWhiteSpace(claimType) && !string.IsNullOrWhiteSpace(ClaimValue))
        {
            var selectedClaim = await _dbContext.ClaimLists.FindAsync(Convert.ToInt32(ClaimValue));
            var claim = new Claim(claimType, selectedClaim.ClaimValue);
            var result = await _userManager.AddClaimAsync(user, claim);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        else
        {
            ModelState.AddModelError("", "Invalid input.");
        }
        return View();
    }

    // List claims for a user
    public async Task<IActionResult> ViewClaims(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return View(claims);
        }
        ModelState.AddModelError("", "User not found.");
        return View();
    }

    //// Add a claim to a user
    //[HttpPost]
    //public async Task<IActionResult> AddClaim(string email, string claimType, string claimValue)
    //{
    //    var user = await _userManager.FindByEmailAsync(email);
    //    if (user != null && !string.IsNullOrWhiteSpace(claimType) && !string.IsNullOrWhiteSpace(claimValue))
    //    {
    //        // Add the claim
    //        var claim = new Claim(claimType, claimValue);
            
    //        var result = await _userManager.AddClaimAsync(user, claim);

    //        if (result.Succeeded)
    //        {
    //            // Update ClaimCategoryId in the database
    //            if (claimCategoryId.HasValue)
    //            {
    //                var userClaim = _dbContext.UserClaims
    //                    .FirstOrDefault(c => c.UserId == user.Id && c.ClaimType == claimType && c.ClaimValue == claimValue);

    //                if (userClaim != null)
    //                {
    //                    userClaim.ClaimCategoryId = claimCategoryId.Value;
    //                    _dbContext.Update(userClaim);
    //                    await _dbContext.SaveChangesAsync();
    //                }
    //            }

    //            return RedirectToAction("Index", "Home");
    //        }

    //        foreach (var error in result.Errors)
    //        {
    //            ModelState.AddModelError("", error.Description);
    //        }
    //    }
    //    else
    //    {
    //        ModelState.AddModelError("", "Invalid input.");
    //    }

    //    return View();
    //}



    //// List claims for a user
    //public async Task<IActionResult> ViewClaims(string email)
    //{
    //    var user = await _userManager.FindByEmailAsync(email);
    //    if (user != null)
    //    {
    //        var claims = await _userManager.GetClaimsAsync(user);
    //        return View(claims);
    //    }
    //    ModelState.AddModelError("", "User not found.");
    //    return View();
    //}
}
