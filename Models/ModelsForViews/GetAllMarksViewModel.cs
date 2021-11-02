using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class GetAllMarksViewModel
    {
        public int CurrentUserDepartmentId { get; set; }
        public IEnumerable<User> Assessors { get; set; }
        public IEnumerable<Mark> Marks { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
