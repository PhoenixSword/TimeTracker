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
        private ICalendarRepo _calendarRepo;
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
        public string Save([FromBody]CalendarViewModel calendarViewModel)
        {
            if ((calendarViewModel.Hours > 24) || (calendarViewModel.Hours < 0))
            {
                return "Invalid \"Hours\" value";
            }

            if ((calendarViewModel.Date.Month != DateTime.Now.Month) || (calendarViewModel.Date.Year != DateTime.Now.Year))
            {
                return "Invalid \"Date\" value";
            }

            return _calendarRepo.Save(calendarViewModel, UserId);
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