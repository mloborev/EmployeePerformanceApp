using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private ApplicationContext db;
        public DepartmentRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<List<Department>> GetAllData()
        {
            return await db.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await db.Departments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
