using TimeTracker.Models.Repositories.Abstract;
using TimeTracker.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TimeTracker.Models.Repositories.Abstract;
using TimeTracker.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace TimeTracker.Models.Repositories.Concrete
{
    public class CalendarRepo : ICalendarRepo
    {
        FirestoreDb db = FirestoreDb.Create("timetracker-5c762");

        public async Task<Dictionary<int, int>> Get(string UserId)
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

        public string Save(CalendarViewModel calendarViewModel, string UserId)
        {
            DocumentReference docRef = db.Collection("users").Document(UserId.ToString()).Collection("calendars").Document(DateTime.Parse(calendarViewModel.Date.ToString()).ToString("MM.yyyy")).Collection("hours").Document(calendarViewModel.Date.ToString("dd"));
            docRef.SetAsync(new { hours = calendarViewModel.Hours });
            return "";
        }

    }
}

