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

        public async Task AddParameter(string name)
        {
            Parameter parameter = new Parameter {Name = name, IsInUse = false};
            await _parameterRepository.AddParameter(parameter);
        }

        public async Task SetParametersInUse(List<Parameter> parameters)
        {
            await _parameterRepository.SetParametersInUse(parameters);
        }
    }
}
