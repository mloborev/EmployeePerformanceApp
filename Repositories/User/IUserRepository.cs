using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByLoginPassword(LoginModel model);
        Task<List<User>> GetAllData();
        Task AddUser();
        Task DeleteUser();
    }
}
