using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUser(string surname, string name, string login, string password, int roleId, int statusId, int departmentId)
        {
            User user = new User { Surname = surname, Name = name, Login = login, Password = password, RoleId = roleId, StatusId = statusId, DepartmentId = departmentId };
            await _userRepository.AddUser(user);
        }

        public async Task<List<User>> GetAllData()
        {
            return await _userRepository.GetAllData();
        }

        public async Task<bool> CheckIsUserExistByLogin(string login)
        {
            return await _userRepository.CheckIsUserExistByLogin(login);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<List<User>> GetUsersByDepartmentIdNotChief(int id)
        {
            return await _userRepository.GetUsersByDepartmentIdNotChief(id);
        }

        public async Task DeleteUser(int id)
        {
            User user = await _userRepository.GetUserById(id);
            await _userRepository.DeleteUser(user);
        }

        public async Task SetWorkingFlag(int id)
        {
            User user = await _userRepository.GetUserById(id);
            await _userRepository.SetWorkingFlag(user);
        }
    }
}
