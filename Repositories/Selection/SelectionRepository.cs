using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public class SelectionRepository : ISelectionRepository
    {
        private ApplicationContext db;
        public SelectionRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task AddSelection(Selection selection)
        {
            db.Selections.Add(selection);
            await db.SaveChangesAsync();
        }

        public Task DeleteSelection(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Selection>> GetAllData()
        {
            return await db.Selections.Include(x => x.Department).Include(x => x.Parameters).ToListAsync();
        }

        public async Task<List<Selection>> GetSelectionsByIds(int[] ids)
        {
            return await db.Selections.Include(x => x.Department).Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<Selection> GetSelectionById(int id)
        {
            return await db.Selections.Include(x => x.Parameters).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
