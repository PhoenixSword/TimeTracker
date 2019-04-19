using TimeTracker.Models.Repositories.Abstract;
using TimeTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Storage;
    using Google.Cloud.Firestore;
    using Google.Type;

namespace TimeTracker.Models.Repositories.Concrete
{
    public class CalendarRepo : ICalendarRepo
    {
        FirestoreDb db = FirestoreDb.Create("timetracker-5c762");

        public async Task<Dictionary<string, int>> GetAll(string userId, DateTime date)
        {
            DocumentReference usersRef = db.Collection("users").Document(userId);
            CollectionReference colRef = usersRef.Collection(date.ToString("MM.yyyy"));
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (var temp in querySnapshot)
            {
                var day = temp.Id;
                var tasks = await temp.Reference.Collection("tasks").GetSnapshotAsync();
                int hours = 0;
                foreach (var temp2 in tasks)
                {
                    hours += int.Parse(temp2.ToDictionary()["hours"].ToString());
                }

                dictionary.Add(day, hours);
            }

            return dictionary;
        }

        public async Task<Dictionary<string, string>> GetAllTasks(string userId, DateTime date)
        {
            CollectionReference colRef = db.Collection("users").Document(userId).Collection(date.ToString("MM.yyyy"))
                .Document("tasks").Collection("tasks");
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var temp in querySnapshot)
            {
                dictionary.Add(temp.Id, temp.ToDictionary()["title"].ToString());
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
                DocumentReference qwe = (DocumentReference)temp.ToDictionary()["reference"];
                DocumentSnapshot qSnapshot = await qwe.GetSnapshotAsync();
                var dictionary = qSnapshot.ToDictionary();
                list.Add(new TaskViewModel{
                    Id = temp.Id,
                    Name = dictionary["title"].ToString(),
                    Description = dictionary["description"].ToString(), 
                    Hours = int.Parse(temp.ToDictionary()["hours"].ToString()),
                    DownloadUrl = dictionary["downloadUrl"].ToString() });
              
            }
            return list;
        }

        public async Task<Dictionary<string, List<object>>> GetInfo(string userId, DateTime date)
        {
            CollectionReference colRef = db.Collection("users").Document(userId).Collection(date.ToString("MM.yyyy"));
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();
            Dictionary<string, List<TaskViewModel>> list = new Dictionary<string, List<TaskViewModel>>();

            foreach (var temp in querySnapshot)
            {
                var tasksRef = colRef.Document(temp.Id).Collection("tasks");
                var tasksSnapshot = await tasksRef.GetSnapshotAsync();
                var tempList = new List<TaskViewModel>();
                foreach (var temp2 in tasksSnapshot)
                {
                    DocumentReference taskReference = (DocumentReference)temp2.ToDictionary()["reference"];
                    DocumentSnapshot qSnapshot = await taskReference.GetSnapshotAsync();
                    var dictionary = qSnapshot.ToDictionary();
                    tempList.Add(new TaskViewModel
                    {
                        Id = temp2.Id,
                        Name = dictionary["title"].ToString(),
                        Description = dictionary["description"].ToString(),
                        Hours = int.Parse(temp2.ToDictionary()["hours"].ToString()),
                    });
                   
                }
                list.Add(temp.Id, tempList);

            }


            Dictionary<string, List<object>> list2 = new Dictionary<string, List<object>>();
            foreach (var task in list)
            {
                foreach (var task2 in task.Value)
                {
                    
                    if (list2.ContainsKey(task2.Name + "(" + task2.Description.Substring(0, task2.Description.Length >= 5 ? 5 : task2.Description.Length) + ")"))
                    {
                        if (task2.Id.Equals(list2[task2.Name + "(" + task2.Description.Substring(0, task2.Description.Length >= 5 ? 5 : task2.Description.Length) + ")"].ElementAt(0).GetType().GetProperty("Id").GetValue(list2[task2.Name + "(" + task2.Description.Substring(0, task2.Description.Length >= 5 ? 5 : task2.Description.Length) + ")"].ElementAt(0), null).ToString()))
                        {
                            list2[task2.Name + "(" + task2.Description.Substring(0, task2.Description.Length >= 5 ? 5 : task2.Description.Length) + ")"].Add(new { task2.Id, date = task.Key, hours = task2.Hours });
                        }
                        else
                        {
                            var tempList = new List<object>
                            {
                                new { task2.Id, date = task.Key, hours = task2.Hours }
                            };
                            list2.Add(task2.Name + "(" + task2.Description.Substring(0, task2.Description.Length >= 5 ? 5 : task2.Description.Length) + ")", tempList);
                        }
                    }
                    else if (list2.ContainsKey(task2.Name))
                    {
                        if (task2.Id.Equals(list2[task2.Name].ElementAt(0).GetType().GetProperty("Id").GetValue(list2[task2.Name].ElementAt(0), null).ToString()))
                        {
                            list2[task2.Name].Add(new { task2.Id, date = task.Key, hours = task2.Hours });
                        }
                        else
                        {
                            var tempList = new List<object>
                            {
                                new { task2.Id, date = task.Key, hours = task2.Hours }
                            };
                            list2.Add(task2.Name + "(" + task2.Description.Substring(0, task2.Description.Length >= 5 ? 5 : task2.Description.Length) + ")", tempList);
                        }
                    }
                    else
                    {
                        var tempList = new List<object>
                        {
                            new { task2.Id, date = task.Key, hours = task2.Hours }
                        };
                        list2.Add(task2.Name, tempList);
                    }   
                    
                }
            }
            return list2;
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

            DocumentReference tasksRef;

            if (calendarViewModel.Id != null)
            {
                tasksRef = db.Collection("users").Document(userId).Collection(DateTime.Parse(calendarViewModel.Date.ToString(CultureInfo.CurrentCulture))
                    .ToString("MM.yyyy")).Document("tasks").Collection("tasks").Document(calendarViewModel.Id);
            }
            else
            {
                tasksRef = db.Collection("users").Document(userId).Collection(DateTime.Parse(calendarViewModel.Date.ToString(CultureInfo.CurrentCulture))
                    .ToString("MM.yyyy")).Document("tasks").Collection("tasks").Document();
            }

            tasksRef?.SetAsync(new { title = calendarViewModel.Name, description = calendarViewModel.Description, downloadUrl }, SetOptions.MergeAll);

            DocumentReference dateRef = db.Collection("users").Document(userId)
                .Collection(DateTime.Parse(calendarViewModel.Date.ToString(CultureInfo.CurrentCulture))
                    .ToString("MM.yyyy")).Document(calendarViewModel.Date.ToString("dd"));
            await dateRef.SetAsync(new
            {
                month = calendarViewModel.Date.ToString("MM.yyyy")
            });
            DocumentReference taskRef;
            if (calendarViewModel.Id != null)
            {
                taskRef = dateRef.Collection("tasks").Document(calendarViewModel.Id);
            }
            else
            {
                taskRef = dateRef.Collection("tasks").Document(tasksRef?.Id);
            }
            taskRef?.SetAsync(new { reference = tasksRef, hours = calendarViewModel.Hours }, SetOptions.MergeAll);
        }

    }
}

