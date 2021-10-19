using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartmentById(int id);
        Task<List<Department>> GetAllData();
    }
}
