using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private ApplicationContext db;
        public StatusRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<List<Status>> GetAllData()
        {
            return await db.Statuses.ToListAsync();
        }

        public async Task<Status> GetStatusById(int id)
        {
            return await db.Statuses.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
