using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public interface ISelectionService
    {
        Task AddSelection(int departmentId, string selectionName, int[] arr);
        Task<List<Selection>> GetAllData();
        Task<Selection> GetSelectionById(int id);
    }
}
