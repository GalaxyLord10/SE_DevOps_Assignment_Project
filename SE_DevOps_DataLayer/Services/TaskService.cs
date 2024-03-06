using Microsoft.EntityFrameworkCore;
using SE_DevOps_DataLayer.Entities; // Import the namespace where Task is defined
using System.Linq;

namespace SE_DevOps_DataLayer.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Task>> GetAllTasksAsync(string userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<Entities.Task> GetTaskByIdAsync(int taskId, string userId)
        {
            // Ensuring that the task belongs to the user
            return await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.TaskId == taskId && t.UserId == userId);
        }

        public async Task<Entities.Task> AddTaskAsync(Entities.Task task, string userId)
        {
            task.UserId = userId; // Set the UserId to the current user's ID
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> UpdateTaskAsync(Entities.Task task, string userId)
        {
            var existingTask = await _context.Tasks
                .AsNoTracking() // Use AsNoTracking when you fetch the task to update
                .FirstOrDefaultAsync(t => t.TaskId == task.TaskId && t.UserId == userId);

            if (existingTask == null)
            {
                return null; // Task not found or doesn't belong to the user
            }

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return task;
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int taskId, string userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.TaskId == taskId && t.UserId == userId);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
