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
        Task<List<Parameter>> GetParametersByIds(int[] ids);
        Task<Parameter> GetParameterById(int id);
        Task<Parameter> GetParameterByName(string name);
        Task AddParameter(Parameter parameter);
        Task DeleteParameter(Parameter parameter);
        Task<List<Parameter>> GetAllDataForDepartment(int id);
    }
}
