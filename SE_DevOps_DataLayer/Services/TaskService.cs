using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SE_DevOps_DataLayer.Interfaces;

namespace SE_DevOps_DataLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Task>> GetAllTasksAsync(IdentityUser user)
        {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == user.Id)
                .ToListAsync();
            return tasks;
        }

        public async Task<Entities.Task> GetTaskByIdAsync(int taskId, IdentityUser user)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId && t.UserId == user.Id);
        }

        public async Task<Entities.Task> AddTaskAsync(Entities.Task task, IdentityUser user)
        {
            task.UserId = user.Id;
            task.User = user;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> UpdateTaskAsync(Entities.Task task, IdentityUser user)
        {
            task.UserId = user.Id;
            task.User = user;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(int taskId, IdentityUser user)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.TaskId == taskId && t.UserId == user.Id);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
