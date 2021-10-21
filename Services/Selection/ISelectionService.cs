using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public interface ISelectionService
    {
        Task AddSelection(int departmentId, string selectionName);
    }
}
