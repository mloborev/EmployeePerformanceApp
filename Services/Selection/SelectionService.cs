using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public class SelectionService : ISelectionService
    {
        private readonly ISelectionRepository _selectionRepository;
        public SelectionService(ISelectionRepository selectionRepository)
        {
            _selectionRepository = selectionRepository;
        }

        public async Task AddSelection(int departmentId, string selectionName)
        {
            Selection selection = new Selection { DepartmentId = departmentId, Name = selectionName };
            await _selectionRepository.AddSelection(selection);
        }
    }
}
