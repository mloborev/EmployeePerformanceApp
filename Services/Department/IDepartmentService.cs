using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllData();
    }
}
