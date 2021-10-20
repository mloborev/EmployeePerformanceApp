using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class Selection
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        public Department Department { get; set; }

        public List<Parameter> Parameters { get; set; }
        public Selection()
        {
            Parameters = new List<Parameter>();
        }
    }
}
