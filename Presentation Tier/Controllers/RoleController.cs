using Data_Access_Tier.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation_Tier.Models;

namespace Presentation_Tier.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<ApplicationRole> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<ApplicationRole> roleManager, ILogger<ApplicationRole> logger, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(role);
        }

        public async Task<IActionResult> Details(string Id, string viewName = "Details")
        {
            if (Id is null)
                return NotFound();

            var role = await _roleManager.FindByIdAsync(Id);

            if (role is null)
                return NotFound();

            return View(viewName, role);
        }

        public async Task<IActionResult> Update(string Id)
        {
            return await Details(Id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string Id, ApplicationRole applicationRole)
        {
            if (Id != applicationRole.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(Id);

                    role.Name = applicationRole.Name;
                    role.NormalizedName = applicationRole.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(role);

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

            return View(applicationRole);
        }

        public async Task<IActionResult> Delete(string Id, ApplicationRole applicationRole)
        {
            if (Id != applicationRole.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(Id);

                    var result = await _roleManager.DeleteAsync(role);

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

        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);

            if (role is null)
                return BadRequest();

            var usersInRole = new List<UserInRoleViewModel>();

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsChecked = true;
                else
                    userInRole.IsChecked = false;

                usersInRole.Add(userInRole);
            }

            ViewBag.RoleId = RoleId;
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId, List<UserInRoleViewModel> usersInRole)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);

            if (role is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                foreach (var user in usersInRole)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);

                    if (appUser is not null)
                    {
                        if (user.IsChecked && !await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsChecked && await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }

                return RedirectToAction(nameof(Update), new { id = RoleId });
            }

            return View(usersInRole);
        }
    }
}
