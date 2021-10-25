using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int DepartmentId { get; set; }

        public Role Role { get; set; }
        public Status Status { get; set; }
        public Department Department { get; set; }

        public List<Mark> Marks { get; set; }
        public User()
        {
            Marks = new List<Mark>();
        }
    }
}
