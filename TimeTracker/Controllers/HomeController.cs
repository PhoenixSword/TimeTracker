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
using System.Threading;
using TimeTracker.Models.Repositories.Abstract;

namespace TimeTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICalendarRepo _calendarRepo;
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public HomeController(ICalendarRepo calendarRepo)
        {
            _calendarRepo = calendarRepo;
        }

        [Authorize]
        [Route("/api")]
        public async Task<Dictionary<int, int>> Get()
        {
            return await _calendarRepo.Get(UserId);
        }

        [Authorize]
        [Route("/api")]
        [HttpPost]
        public IActionResult Save([FromBody]CalendarViewModel calendarViewModel)
        {
            if (ModelState.IsValid)
            {
                _calendarRepo.Save(calendarViewModel, UserId);
                return Ok();
            }
            
            List<string> allErrors = ModelState.Values.SelectMany(v => v.Errors.Select(d => d.ErrorMessage)).Distinct().ToList();
            return Ok(allErrors);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}