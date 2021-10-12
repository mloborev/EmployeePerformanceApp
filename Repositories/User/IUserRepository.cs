using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetCurrentUser();
        Task<User> GetUserByLoginPassword(string loginFromModel, string passwordFromModel);
        Task<List<User>> GetAllData();
        Task AddUser();
        Task DeleteUser();
    }
}
