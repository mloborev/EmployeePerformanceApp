using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext db;
        public UserRepository(ApplicationContext context)
        {
            db = context;
        }

        public Task AddUser()
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser()
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllData()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            User user = await db.Users.Include(u => u.Role).Include(u => u.Status).Include(u => u.Department).Where(x => x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByLoginPassword(string userLogin, string userPassword)
        {
            User user = await db.Users.Include(u => u.Role).Include(u => u.Status).Include(u => u.Department).Where(u => u.Login == userLogin && u.Password == userPassword).FirstOrDefaultAsync();

            

            if (user != null && String.Equals(user.Login, userLogin) && String.Equals(user.Password, userPassword))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
