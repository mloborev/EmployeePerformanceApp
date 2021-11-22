using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class SelectionWithUsersModel
    {
        public Selection Selection { get; set; }
        public List<(User user, double mark)> SelectionUsers { get; set; }
    }
}
