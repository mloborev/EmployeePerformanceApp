using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int? DepartmentId { get; set; }

        public RoleModel Role { get; set; }
        public StatusModel Status { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
