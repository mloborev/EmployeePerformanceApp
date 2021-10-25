using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public interface IMarkRepository
    {
        Task<Mark> GetMarkById(int id);
        Task<List<Mark>> GetAllData();
        Task AddMark(Mark mark);
    }
}
