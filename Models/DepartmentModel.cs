using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int? UserId { get; set; }
        public bool ShowPreviousMarks { get; set; }

        public List<UserModel> Users { get; set; }
        public DepartmentModel()
        {
            Users = new List<UserModel>();
        }
    }
}
