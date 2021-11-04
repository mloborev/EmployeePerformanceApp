using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class GetAllSelectionsViewModel
    {
        public IEnumerable<User> TopUsers { get; set; }
        public IEnumerable<User> BottomUsers { get; set; }
    }
}
