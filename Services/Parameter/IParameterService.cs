using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public interface IParameterService
    {
        Task AddParameter(int departmentId, string name, double coefficient);
        Task<List<Parameter>> GetAllData();
        Task DeleteParameter(int parameterId);
    }
}
