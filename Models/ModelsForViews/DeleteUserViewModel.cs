using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class DeleteUserViewModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DepartmentName { get; set; }
        public string StatusName { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
