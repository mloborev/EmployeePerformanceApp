using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public class MarkRepository : IMarkRepository
    {
        private ApplicationContext db;
        public MarkRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task AddMark(Mark mark)
        {
            db.Marks.Add(mark);
            await db.SaveChangesAsync();
        }

        public async Task<List<Mark>> GetAllData()
        {
            return await db.Marks.Include(x => x.User).Include(x => x.Parameter).ToListAsync();
        }

        public async Task<List<Mark>> GetMarkByUserAndDepartmentIds(int userId, int parameterId)
        {
            return await db.Marks.Where(x => x.UserId == userId && x.ParameterId == parameterId).ToListAsync();
        }
    }
}
