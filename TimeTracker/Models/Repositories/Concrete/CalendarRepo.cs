using TimeTracker.Models.Repositories.Abstract;
using TimeTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;
using Google.Cloud.Firestore;

namespace TimeTracker.Models.Repositories.Concrete
{
    public class CalendarRepo : ICalendarRepo
    {
        FirestoreDb db = FirestoreDb.Create("timetracker-5c762");

        public async Task<Dictionary<int, object>> GetAll(string userId, DateTime date)
        {
            DocumentReference usersRef = db.Collection("users").Document(userId);
            CollectionReference colRef = usersRef.Collection(date.ToString("MM.yyyy"));
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();
            Dictionary<int, object> dictionary = new Dictionary<int, object>();
            Dictionary<string, object> dictionaryObj;
            foreach (var temp in querySnapshot)
            {
                dictionaryObj = new Dictionary<string, object>();
                var tasks = await temp.Reference.Collection("tasks").GetSnapshotAsync();
                foreach (var temp2 in tasks)
                {
                    dictionaryObj.Add(temp2.Id,
                        new
                        {
                            hours = int.Parse(temp2.ToDictionary()["hours"].ToString()),
                            description = temp2.ToDictionary()["description"].ToString()
                        });
                }

                dictionary.Add(int.Parse(temp.Id), dictionaryObj);
            }

            return dictionary;
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasks(string userId, DateTime date)
        {
            CollectionReference colRef = db.Collection("users").Document(userId).Collection(date.ToString("MM.yyyy"))
                .Document(date.ToString("dd")).Collection("tasks");
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();
            List<TaskViewModel> list = new List<TaskViewModel>();
            foreach (var temp in querySnapshot)
            {
                string url;
                if (temp.ToDictionary().ContainsKey("downloadUrl"))
                {
                    url = temp.ToDictionary()["downloadUrl"].ToString();
                }
                else
                {
                    url = "";
                }

                list.Add(new TaskViewModel
                {
                    Name = temp.Id, Hours = int.Parse(temp.ToDictionary()["hours"].ToString()),
                    Description = temp.ToDictionary()["description"].ToString(),
                    DownloadUrl = url
                });
            }

            return list;
        }

        public async Task Save(CalendarViewModel calendarViewModel, string userId)
        {
            var filePath = Path.GetTempFileName();
            // ReSharper disable once RedundantAssignment
            string downloadUrl = "";
            var file = calendarViewModel.Image;
            if (file != null)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var str = File.Open(filePath, FileMode.Open);
                var task = new FirebaseStorage("timetracker-5c762.appspot.com", new FirebaseStorageOptions())
                    .Child(file.FileName)
                    .PutAsync(str);
                downloadUrl = await task;
                str.Dispose();
                File.Delete(filePath);
            }
            else if (calendarViewModel.DownloadUrl != null)
            {
                downloadUrl = calendarViewModel.DownloadUrl;
            }
            DocumentReference dateRef = db.Collection("users").Document(userId)
                .Collection(DateTime.Parse(calendarViewModel.Date.ToString(CultureInfo.CurrentCulture))
                    .ToString("MM.yyyy")).Document(calendarViewModel.Date.ToString("dd"));
            await dateRef.SetAsync(new
            {
                month = calendarViewModel.Date.ToString("MM.yyyy")
            });
            DocumentReference taskRef = dateRef.Collection("tasks").Document(calendarViewModel.Name);
            taskRef?.SetAsync(new {description = calendarViewModel.Description, hours = calendarViewModel.Hours, downloadUrl },
                SetOptions.MergeAll);
        }

    }
}

