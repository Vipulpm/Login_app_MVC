using Kharpan.Application.Repositories.Abstract;
using Kharpan.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kharpan.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        public AccountController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Registration(RegistrationModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            model.Role = "user";
            var result = await _userAuthenticationService.RegistrationAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userAuthenticationService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userAuthenticationService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

       /* public async Task<IActionResult> reg()
        {
            var model = new RegistrationModel
            {
                Username = "Admin",
                Name = "Admin Pun",
                Email = "Admin@yopmail.com",
                Password = "Admin@123"
            };
            model.Role = "admin";
            var result = await _userAuthenticationService.RegistrationAsync(model);
            return Ok(result);
        }*/
    }
}
