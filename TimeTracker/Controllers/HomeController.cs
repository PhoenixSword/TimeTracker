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
        public async Task<Dictionary<string, int>> GetAll(DateTime date)
        {
            return await _calendarRepo.GetAll(UserId, date);
        }

        [Authorize]
        [Route("/api/getTasks")]
        public async Task<IEnumerable<TaskViewModel>> GetTasks(DateTime date)
        {
            return await _calendarRepo.GetTasks(UserId, date);
        }

        [Authorize]
        [Route("/api/getAllTasks")]
        public async Task<Dictionary<string, string>> GetAllTasks(DateTime date)
        {
            return await _calendarRepo.GetAllTasks(UserId, date);
        }
        
        [Authorize]
        [Route("/api/getInfo")]
        public async Task<Dictionary<string, List<object>>> GetInfo(DateTime date)
        {
            return await _calendarRepo.GetInfo(UserId, date);
        }

        [Authorize]
        [Route("/api")]
        [HttpPost]
        public IActionResult Save(CalendarViewModel calendarViewModel)
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