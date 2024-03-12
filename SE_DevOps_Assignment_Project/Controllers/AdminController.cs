using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SE_DevOps_Assignment_Project.Models;
using SE_DevOps_DataLayer.Services;
using System.Security.Claims;
using SE_DevOps_DataLayer.Common;
using SE_DevOps_DataLayer.Interfaces;

namespace SE_DevOps_Assignment_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ITaskService taskService, UserManager<IdentityUser> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        [Route("/admin/manage-users")]
        public async Task<IActionResult> ManageUsers()
        {
            var users = _userManager.Users.ToList();
            
            return View(users);
        }

        public async Task<IActionResult> AdminDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["Message"] = "User deleted successfully!";
                    TempData["Alert"] = AlertMessages.Success.ToString();
                    return RedirectToAction("ManageUsers");
                }
                else
                {
                    TempData["Message"] = "User could not be deleted!";
                    TempData["Alert"] = AlertMessages.Error.ToString();
                }
            }

            return RedirectToAction("ManageUsers");
        }

    }
}
