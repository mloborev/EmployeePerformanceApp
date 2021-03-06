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
        Task<User> GetUserByName(string name);
        Task<User> GetUserByLoginPassword(string userLogin, string userPassword);
        Task<List<User>> GetUsersByDepartmentId(int id);
        Task<List<User>> GetUsersByDepartmentIdNotChief(int id);
        Task<List<User>> GetAllData();
        Task<List<User>> GetAllDataForDepartmentForLead(int id);
        Task<List<User>> GetAllDataForDepartmentForChief(int id);
        Task AddUser(User user);
        Task DeleteUser(User user);
        Task SetWorkingFlag(User user);
        Task<bool> CheckIsUserExistByLogin(string login);
    }
}
