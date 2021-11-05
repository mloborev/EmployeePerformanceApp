using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public interface IMarkRepository
    {
        Task<List<Mark>> GetAllData();
        Task AddMark(Mark mark);
        Task<List<Mark>> GetMarkByUserAndDepartmentIds(int userId, int parameterId);
    }
}
