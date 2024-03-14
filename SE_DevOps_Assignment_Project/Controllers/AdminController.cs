using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SE_DevOps_Assignment_Project.Models;
using SE_DevOps_DataLayer.Services;
using System.Security.Claims;
using SE_DevOps_DataLayer.Common;
using SE_DevOps_DataLayer.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SE_DevOps_Assignment_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ITaskService taskService, UserManager<IdentityUser> userManager, ILogger<AdminController> logger)
        {
            _taskService = taskService;
            _userManager = userManager;
            _logger = logger;
        }

        [Route("/admin/manage-users")]
        public async Task<IActionResult> ManageUsers()
        {
            try
            {
                var users = _userManager.Users.ToList();

                return View(users);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting users list.");
                throw;
            }

        }

        public async Task<IActionResult> AdminDelete(string id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user: {id}");
                throw;
            }

        }

    }
}
