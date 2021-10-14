using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public async Task<IActionResult> RedirectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value);
            User user = await _userRepository.GetUserById(userId);

            switch(user.RoleId)
            {
                case 1:
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                case 2:
                    {
                        return RedirectToAction("Index", "Chief");
                    }
                case 3:
                    {
                        return RedirectToAction("Index", "Lead");
                    }
                case 4:
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                default:
                    {
                        return RedirectToAction("Login", "Account");
                    }
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string userLogin = model.Login;
                string userPassword = model.Password;
                User user = await _userRepository.GetUserByLoginPassword(userLogin, userPassword);

                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("RedirectUser", "Account");
                }
                else
                    ModelState.AddModelError("", "Wrong login or password");
            }
            else
            {
                ModelState.AddModelError("", "You need to fill both labels");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
