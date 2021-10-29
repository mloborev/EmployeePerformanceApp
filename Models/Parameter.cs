using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class Parameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Coefficient { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public List<Selection> Selections { get; set; }
        public List<Mark> Marks { get; set; }
        public Parameter()
        {
            Selections = new List<Selection>();
            Marks = new List<Mark>();
        }
    }
}
