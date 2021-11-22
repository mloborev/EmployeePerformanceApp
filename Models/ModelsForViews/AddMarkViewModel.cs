using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class AddMarkViewModel
    {
        public int LeadDepartmentId { get; set; }
        public int UserId { get; set; }
        public Parameter ParameterId { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
