using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models;
using TimeTracker.ViewModels;
using Calendar = TimeTracker.Models.Calendar;

namespace TimeTracker.Controllers
{
    [Authorize]
    public class TimeTracker : Controller
    {

        public string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;
        private readonly ApplicationDbContext ctx;
        FirestoreDb db = FirestoreDb.Create("timetracker-5c762");

        private IEnumerable<Calendar> Calendars => ctx.Calendars.Where(s => s.UserId == UserId && s.Date.Month == DateTime.Now.Month && s.Date.Year == DateTime.Now.Year).ToList();

        public TimeTracker(ApplicationDbContext _ctx)
        {
            ctx = _ctx;
        }

        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            return View(await getCalendarsAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async System.Threading.Tasks.Task<Dictionary<DateTime, int>> getCalendarsAsync()
        {
            
            DocumentReference usersRef = db.Collection("users").Document(UserId.ToString());
            CollectionReference colRef  = usersRef.Collection("calendars");
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();
            Dictionary<DateTime, int> dictionary = new Dictionary<DateTime, int>();

            foreach (DocumentSnapshot temp in querySnapshot)
            {
                dictionary.Add(DateTime.Parse(temp.Id.ToString()), int.Parse(temp.ToDictionary()["hours"].ToString()));
            }

            return dictionary;

        }
        [HttpPost]
        public string Save(CalendarViewModel calendarViewModel)
        {
            if ((calendarViewModel.Hours > 24) || (calendarViewModel.Hours < 0))
            {
                return "Invalid \"Hours\" value";
            }

            if ((calendarViewModel.Date.Month != DateTime.Now.Month) || (calendarViewModel.Date.Year != DateTime.Now.Year))
            {
                return "Invalid \"Date\" value";
            }
            DocumentReference docRef = db.Collection("users").Document(UserId.ToString()).Collection("calendars").Document(DateTime.Parse(calendarViewModel.Date.ToString()).ToString("dd.MM.yyyy"));
            docRef.SetAsync(new {hours = calendarViewModel.Hours});
            return "";
            //var calendar = new Calendar(calendarViewModel.Date, calendarViewModel.Hours, UserId);
            //var calendarUpdate = ctx.Calendars.FirstOrDefault(d => d.Date == calendar.Date);

            //if (calendarUpdate != null)
            //{
            //    calendarUpdate.Date = calendar.Date;
            //    calendarUpdate.Hours = calendar.Hours;
            //}
            //else
            //{
            //    ctx.Calendars.Add(calendar);
            //}

            //ctx.SaveChanges();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
