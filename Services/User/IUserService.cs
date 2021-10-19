using EmployeePerformanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public interface IUserService
    {
        Task AddUser(string surname, string name, string login, string password, int roleId, int statusId, int departmentId);
    }
}
