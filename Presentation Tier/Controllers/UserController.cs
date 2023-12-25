using Data_Access_Tier.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation_Tier.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ApplicationUser> _logger;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<ApplicationUser> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string SearchValue = "")
        {
            List<ApplicationUser> users;

            if (string.IsNullOrEmpty(SearchValue))
                users = await _userManager.Users.ToListAsync();

            else
                users = await _userManager.Users.Where(user => user.Email.Trim().ToLower().Contains(SearchValue.Trim().ToLower())).ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> Details(string Id, string viewName = "Details")
        {
            if (Id is null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(Id);

            if (user is null)
                return NotFound();

            return View(viewName, user);
        }

        public async Task<IActionResult> Update(string Id)
        {
            return await Details(Id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string Id, ApplicationUser applicationUser)
        {
            if (Id != applicationUser.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(Id);

                    user.UserName = applicationUser.UserName;
                    user.NormalizedUserName = applicationUser.UserName.ToUpper();

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return View(applicationUser);
        }

        public async Task<IActionResult> Delete(string Id, ApplicationUser applicationUser)
        {
            if (Id != applicationUser.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(Id);

                    var result = await _userManager.DeleteAsync(user);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
