using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Models
{
    public class StatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserModel> Users { get; set; }
        public StatusModel()
        {
            Users = new List<UserModel>();
        }
    }
}
