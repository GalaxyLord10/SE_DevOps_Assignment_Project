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
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, UserManager<IdentityUser> userManager, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await GetCurrentUser();
                var tasks = await _taskService.GetAllTasksAsync(user);
                TempData["UserName"] = user.UserName;
                return View(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task list.");
                throw;
            }

        }

        private async Task<IdentityUser> GetCurrentUser()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current user.");
                throw;
            }
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
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task.");
                throw;

            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting task {id} for edit.");
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskViewModel task)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating task {task.TaskId}.");
                throw;
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting task {id} for deletion.");
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var user = await GetCurrentUser();
                await _taskService.DeleteTaskAsync(id, user);
                TempData["Message"] = "Task deleted!";
                TempData["Alert"] = AlertMessages.Success.ToString();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting task {id}.");
                throw;
            }
        }
    }
}
