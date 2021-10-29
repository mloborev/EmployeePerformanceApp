using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public class ParameterService : IParameterService
    {
        private readonly IParameterRepository _parameterRepository;
        public ParameterService(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        public async Task AddParameter(int departmentId, string name, double coefficient)
        {
            Parameter parameter = new Parameter {Name = name, Coefficient = coefficient, DepartmentId = departmentId};
            await _parameterRepository.AddParameter(parameter);
        }

        /*public async Task SetParametersInUse(List<Parameter> parameters)
        {
            await _parameterRepository.SetParametersInUse(parameters);
        }*/
    }
}
