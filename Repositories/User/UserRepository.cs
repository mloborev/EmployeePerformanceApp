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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(ApplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddUser(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            db.Remove(user);
            await db.SaveChangesAsync();
        }

        public async Task SetWorkingFlag(User user)
        {
            var user1 = await db.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            if (user1.Status.Name == "Working")
                user1.StatusId = 2;
            else
                user1.StatusId = 1;
            await db.SaveChangesAsync();
        }

        public async Task<bool> CheckIsUserExistByLogin(string login)
        {
            return await db.Users.AnyAsync(x => x.Login == login);
        }

        public async Task<List<User>> GetUsersByDepartmentId(int id)
        {
            return await db.Users.Where(x => x.DepartmentId == id).ToListAsync();
        }

        public async Task<List<User>> GetUsersByDepartmentIdNotChief(int id)
        {
            int currentUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "Id").Value);
            User currentUser = await GetUserById(currentUserId);

            return await db.Users
                .Include(x => x.Marks)
                .Include(u => u.Role)
                .Include(u => u.Status)
                .Include(u => u.Department)
                .Where(x => x.DepartmentId == id && x.RoleId != currentUser.RoleId)
                .ToListAsync();
        }

        public async Task<List<User>> GetAllData()
        {
            return await db.Users.Include(u => u.Role).Include(u => u.Status).Include(u => u.Department).Include(x => x.Marks).ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            User user = await db.Users.Include(x => x.Marks).Include(u => u.Role).Include(u => u.Status).Include(u => u.Department).Where(x => x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByLoginPassword(string userLogin, string userPassword)
        {
            User user = await db.Users
                .Include(u => u.Role)
                .Include(u => u.Status)
                .Include(u => u.Department)
                .Where(u => u.Login == userLogin && u.Password == userPassword)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
