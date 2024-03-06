using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SE_DevOps_DataLayer.Services;
using System.Security.Claims;

namespace SE_DevOps_Assignment_Project.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = await _taskService.GetAllTasksAsync(userId);
            return View(tasks);
        }

        public async Task<IActionResult> Details(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _taskService.GetTaskByIdAsync(id, userId);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SE_DevOps_DataLayer.Entities.Task task)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _taskService.AddTaskAsync(task, userId);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _taskService.GetTaskByIdAsync(id, userId);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SE_DevOps_DataLayer.Entities.Task task)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _taskService.UpdateTaskAsync(task, userId);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _taskService.GetTaskByIdAsync(id, userId);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _taskService.DeleteTaskAsync(id, userId);
            return RedirectToAction("Index");
        }
    }
}
