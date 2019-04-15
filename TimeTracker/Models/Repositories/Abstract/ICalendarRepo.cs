using System;
using TimeTracker.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TimeTracker.Models.Repositories.Abstract
{
    public interface ICalendarRepo
    {
        Task<Dictionary<int, object>> GetAll(string userId, DateTime date);

        Task<IEnumerable<TaskViewModel>> GetTasks(string userId, DateTime date);

        Task Save(CalendarViewModel calendarViewModel, string userId);
    }
}