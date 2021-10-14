using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int? UserId { get; set; }
        public bool ShowPreviousMarks { get; set; }

        public List<Selection> Selections { get; set; }
        public List<User> Users { get; set; }
        public Department()
        {
            Users = new List<User>();
            Selections = new List<Selection>();
        }
    }
}
