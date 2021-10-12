using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

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

        public Task<List<User>> GetAllData()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetCurrentUser()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            //var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int userId = Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value);
            User user = await db.Users.Where(u => u.Id = userId).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByLoginPassword(string loginFromModel, string passwordFromModel)
        {
            User user = await db.Users.Include(u => u.Role).Where(u => u.Login == loginFromModel && u.Password == passwordFromModel).FirstOrDefaultAsync();
            return user;
        }
    }
}
