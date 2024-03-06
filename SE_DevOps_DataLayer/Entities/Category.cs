using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_DevOps_DataLayer.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
