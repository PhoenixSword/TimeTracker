using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.ViewModels;
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
        public async Task<Dictionary<int, int>> Get(DateTime date)
        {
            return await _calendarRepo.Get(UserId, date);
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