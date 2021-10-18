using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models.ModelsForViews
{
    public class AddUserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
