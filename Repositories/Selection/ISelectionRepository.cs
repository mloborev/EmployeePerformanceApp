using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public interface ISelectionRepository
    {
        Task<List<Selection>> GetAllData();
        Task<Selection> GetSelectionById(int id);
        Task AddSelection(Selection selection);
        Task DeleteSelection(int id);
    }
}
