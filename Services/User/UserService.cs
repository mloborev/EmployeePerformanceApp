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
    }
}
