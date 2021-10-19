using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleById(int id);
        Task<List<Role>> GetAllData();
    }
}
