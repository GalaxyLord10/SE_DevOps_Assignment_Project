using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_DevOps_DataLayer.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; } // Optional
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Task> Tasks { get; set; } // User can have multiple tasks
        public ICollection<Category> Categories { get; set; } // Optional, if using categories
    }
}
