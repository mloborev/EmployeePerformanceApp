using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public interface IParameterRepository
    {
        Task<List<Parameter>> GetAllData();
        Task<Parameter> GetParameterById(int id);
        Task AddParameter(Parameter parameter);
        Task DeleteParameter(int id);
    }
}
