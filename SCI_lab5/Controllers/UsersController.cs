
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCI_lab5.Data;
using SCI_lab5.Models;
using SCI_lab5.ViewModels;

namespace SCI_lab5.Controllers
{
      
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.OrderBy(user => user.Id);

            List<ApplicationUserViewModel> userViewModel = new List<ApplicationUserViewModel>();

            string urole = "";
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Count() > 0)
                {
                    urole = userRoles[0] ?? "";
                }


                userViewModel.Add(
                    new ApplicationUserViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        RoleName = urole
                    });

            }

            return View(userViewModel);
        }


        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            string userRole = "";
            if (userRoles.Count() > 0)
            {
                userRole = userRoles[0] ?? "";
            }

            ApplicationUserViewModel model = new ApplicationUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                RoleName = userRole
            };


            ViewData["RoleName"] = new SelectList(allRoles, "Name", "Name", model.RoleName);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {

                    var oldRoles = await _userManager.GetRolesAsync(user);

                    if (oldRoles.Count() > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(user, oldRoles);

                    }

                    var newRole = model.RoleName;
                    if (newRole.Count() > 0)
                    {
                        await _userManager.AddToRoleAsync(user, newRole);
                    }
                    user.Email = model.Email;
                    user.UserName = model.UserName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Users");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Users");
        }
    }
}
