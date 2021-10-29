using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models.ModelsForViews
{
    public class AddParameterViewModel
    {
        public IEnumerable<Parameter> Parameters { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
