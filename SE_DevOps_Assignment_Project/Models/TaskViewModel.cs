using SE_DevOps_DataLayer.Common;

namespace SE_DevOps_Assignment_Project.Models
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public Category Category { get; set; }
    }
}
