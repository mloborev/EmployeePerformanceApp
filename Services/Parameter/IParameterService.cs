using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public interface IParameterService
    {
        Task AddParameter(string name);
        Task SetParametersInUse(List<Parameter> parameters);
    }
}
