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


        public Selection Selection { get; set; }
        public List<Mark> Marks { get; set; }
        public Parameter()
        {
            Marks = new List<Mark>();
        }
    }
}
