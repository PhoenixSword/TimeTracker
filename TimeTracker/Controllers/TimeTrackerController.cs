using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Models;
using TimeTracker.ViewModels;

namespace TimeTracker.Controllers
{
    [Authorize]
    [Route("/api")]
    [ApiController]
    public class TimeTracker : Controller
    {

        public string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;
        FirestoreDb db = FirestoreDb.Create("timetracker-5c762");

        public async System.Threading.Tasks.Task<Dictionary<int, int>> Index()
        {
            
            DocumentReference usersRef = db.Collection("users").Document(UserId.ToString());
            CollectionReference colRef = usersRef.Collection("calendars").Document(DateTime.Now.ToString("MM.yyyy")).Collection("hours");
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();
            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            foreach (DocumentSnapshot temp in querySnapshot)
            {
                dictionary.Add(int.Parse(temp.Id.ToString()), int.Parse(temp.ToDictionary()["hours"].ToString()));
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
            DocumentReference docRef = db.Collection("users").Document(UserId.ToString()).Collection("calendars").Document(DateTime.Parse(calendarViewModel.Date.ToString()).ToString("MM.yyyy")).Collection("hours").Document(calendarViewModel.Date.ToString("dd"));
            docRef.SetAsync(new {hours = calendarViewModel.Hours});
            return "";
        }
    }
}
