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
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(ITaskService taskService, UserManager<IdentityUser> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUser();
            var tasks = await _taskService.GetAllTasksAsync(user);
            TempData["UserName"] = user.UserName;
            return View(tasks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await GetCurrentUser();
            var task = await _taskService.GetTaskByIdAsync(id, user);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        private async Task<IdentityUser> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(User);
            return user;
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new TaskViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskViewModel task)
        {            
            if (ModelState.IsValid)
            {
                var taskEntity = new SE_DevOps_DataLayer.Entities.Task
                {
                    TaskId = task.TaskId,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    IsCompleted = task.IsCompleted,
                    Category = task.Category
                };

                var user = await GetCurrentUser();
                await _taskService.AddTaskAsync(taskEntity, user);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await GetCurrentUser();
            var task = await _taskService.GetTaskByIdAsync(id, user);

            if (task == null)
            {
                TempData["Message"] = "Task not found!";
                TempData["Alert"] = AlertMessages.Error.ToString();
                return NotFound();
            }

            var taskViewModel = new TaskViewModel()
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                Category = task.Category
            };

            return View(taskViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                var taskEntity = new SE_DevOps_DataLayer.Entities.Task
                {
                    TaskId = task.TaskId,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    IsCompleted = task.IsCompleted,
                    Category = task.Category
                };

                var user = await GetCurrentUser();
                await _taskService.UpdateTaskAsync(taskEntity, user);
                TempData["Message"] = $"Task {task.Title} updated successfully!";
                TempData["Alert"] = AlertMessages.Success.ToString();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await GetCurrentUser();
            var task = await _taskService.GetTaskByIdAsync(id, user);
            if (task == null)
            {
                TempData["Message"] = "Task could not be deleted";
                TempData["Alert"] = AlertMessages.Error.ToString();
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await GetCurrentUser();
            await _taskService.DeleteTaskAsync(id, user);
            TempData["Message"] = "Task deleted!";
            TempData["Alert"] = AlertMessages.Success.ToString();
            return RedirectToAction("Index");
        }
    }
}
