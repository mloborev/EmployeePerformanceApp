using Microsoft.AspNetCore.Mvc;
using EmployeePerformanceApp.Controllers;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using EmployeePerformanceApp.Repositories;
using EmployeePerformanceApp.Models;
using System.Threading.Tasks;
using EmployeePerformanceApp.Services;

namespace EmployeePerformanceApp.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _systemUnderTest;
        private readonly Mock<IUserRepository> _repository = new Mock<IUserRepository>();

        public UserServiceTests()
        {
            _systemUnderTest = new UserService(_repository.Object);
        }

        [Fact]
        public async Task GetAllDataAllUsersReturned()
        {
            // Arrange
            _repository.Setup(x => x.GetAllData()).ReturnsAsync(new List<User>
            {
                new User {Id = 1},
                new User {Id = 2},
                new User {Id = 3},
                new User {Id = 4},
                new User {Id = 5},
                new User {Id = 6},
                new User {Id = 7},
                new User {Id = 8},
                new User {Id = 9},
                new User {Id = 10},
                new User {Id = 11},
                new User {Id = 12},
                new User {Id = 13},
                new User {Id = 14},
                new User {Id = 15},
                new User {Id = 16},
            });

            // Act
            var actual = await _systemUnderTest.GetAllData();

            // Assert
            Assert.Equal(_repository.Object.GetAllData().Result.Count, actual.Count);
        }

        [Fact]
        public async Task GetUserByLoginIsNotNull()
        {
            // Arrange
            _repository.Setup(x => x.CheckIsUserExistByLogin("progLead")).ReturnsAsync(true);

            // Act
            var actual = await _systemUnderTest.CheckIsUserExistByLogin("progLead");

            // Assert
            Assert.Equal(await _repository.Object.CheckIsUserExistByLogin("progLead"), actual);
        }
    }
}
