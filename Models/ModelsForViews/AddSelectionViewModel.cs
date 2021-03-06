using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class AddSelectionViewModel
    {
        public IEnumerable<Selection> Selections { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
        public int DepartmentId { get; set; }
    }
}
