using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TimeTracker.ViewModels;
using System.Net;
using Firebase.Database;
using System.IO;
using System.Reactive.Linq;
using Firebase.Database.Query;
using Firebase.Auth;
using User = TimeTracker.Models.User;
using Google.Cloud.Firestore;
using System.Threading;

namespace TimeTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private const string ApiKey = "AIzaSyDqloo9E7FZ_je4f4AgcH4MOTaBpPXRDA8";
        
        FirestoreDb db = FirestoreDb.Create("timetracker-5c762");

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                // добавляем пользователя

                //await db.Collection("users").Document(model.Email).SetAsync(new {Email = model.Email, model.Password });


                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "TimeTracker");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        
        //[HttpPost]
        //public void CreateUserTest()
        //{
        //    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        //    var email = $"abcd{new Random().Next()}@test.com";

        //    var auth = authProvider.CreateUserWithEmailAndPasswordAsync(email, "test1234").Result;
        //}

        //[HttpPost]
        //public void SingInTest(string email, string password)
        //{

        //    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        //    var auth = authProvider.SignInWithEmailAndPasswordAsync(email, password).Result;

        //    var result = _userManager.Users;
        //    var jwt = new JwtSecurityToken(auth.FirebaseToken);
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        //}

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "TimeTracker");
                    }
                }

                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "TimeTracker");
        }
    }
}