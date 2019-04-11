using TimeTracker.Models.Repositories.Abstract;
using TimeTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace TimeTracker.Models.Repositories.Concrete
{
    public class CalendarRepo : ICalendarRepo
    {
        FirestoreDb db = FirestoreDb.Create("timetracker-5c762");

        public async Task<Dictionary<int, int>> Get(string userId, DateTime date)
        {
            DocumentReference usersRef = db.Collection("users").Document(userId);
            CollectionReference colRef = usersRef.Collection("calendars").Document(date.ToString("MM.yyyy")).Collection("hours");
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();
            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            foreach (DocumentSnapshot temp in querySnapshot)
            {
                dictionary.Add(int.Parse(temp.Id), int.Parse(temp.ToDictionary()["hours"].ToString()));
            }

            return dictionary;
        }

        public void Save(CalendarViewModel calendarViewModel, string userId)
        {
            DocumentReference docRef = db.Collection("users").Document(userId).Collection("calendars").Document(DateTime.Parse(calendarViewModel.Date.ToString(CultureInfo.CurrentCulture)).ToString("MM.yyyy")).Collection("hours").Document(calendarViewModel.Date.ToString("dd"));
            docRef?.SetAsync(new {hours = calendarViewModel.Hours});
        }

    }
}

