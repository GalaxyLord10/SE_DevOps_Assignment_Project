using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_DevOps_DataLayer.Entities
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; }
        public int? CategoryId { get; set; } // Optional, foreign key to Category
        public Category Category { get; set; } // Optional
    }
}
