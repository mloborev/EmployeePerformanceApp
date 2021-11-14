using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Services
{
    public class MarkService : IMarkService
    {
        private readonly IMarkRepository _markRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MarkService(IMarkRepository markRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _markRepository = markRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddMark(int userId, int parameterId, int markValue, string markDescription)
        {
            int currentUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "Id").Value);
            User currentUser = await _userRepository.GetUserById(currentUserId);
            DateTime assessmentDate = DateTime.Now;


            Mark mark = new Mark 
            { 
                ParameterId = parameterId, 
                MarkValue = markValue, 
                MarkDescription = markDescription, 
                UserId = userId, 
                AssessorId = currentUserId, 
                AssessorSurname = currentUser.Surname, 
                AssessorName = currentUser.Name,
                AssesmentDate = assessmentDate, 
                IsActual = true
            };
            await _markRepository.AddMark(mark);
        }

        public async Task<List<Mark>> GetAllData()
        {
            return await _markRepository.GetAllData();
        }
    }
}
