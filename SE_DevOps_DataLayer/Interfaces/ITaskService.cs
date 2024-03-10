using Microsoft.AspNetCore.Identity;

namespace SE_DevOps_DataLayer.Interfaces
{
    public interface ITaskService
    {
        Task<Entities.Task> GetTaskByIdAsync(int taskId, IdentityUser user);
        Task<List<Entities.Task>> GetAllTasksAsync(IdentityUser user);
        Task<Entities.Task> AddTaskAsync(Entities.Task task, IdentityUser user);
        Task<Entities.Task> UpdateTaskAsync(Entities.Task task, IdentityUser user);
        Task DeleteTaskAsync(int taskId, IdentityUser user);
    }
}
