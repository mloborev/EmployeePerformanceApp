using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class GetAllSelectionsViewModel
    {
        public IEnumerable<(User user, double mark)> DictTopUsers { get; set; }
        public IEnumerable<(User user, double mark)> DictBottomUsers { get; set; }
    }
}
