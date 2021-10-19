using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private ApplicationContext db;
        public RoleRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<List<Role>> GetAllData()
        {
            return await db.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await db.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
