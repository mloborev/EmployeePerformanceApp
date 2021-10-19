using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public interface IStatusRepository
    {
        Task<Status> GetStatusById(int id);
        Task<List<Status>> GetAllData();
    }
}
