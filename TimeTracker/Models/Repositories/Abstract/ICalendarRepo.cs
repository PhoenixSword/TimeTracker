using System;
using TimeTracker.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeTracker.Models.Repositories.Abstract
{
    public interface ICalendarRepo
    {
        Task<Dictionary<string, int>> GetAll(string userId, DateTime date);

        Task<IEnumerable<TaskViewModel>> GetTasks(string userId, DateTime date);

        Task<Dictionary<string, string>> GetAllTasks(string userId, DateTime date);

        Task<Dictionary<string, List<object>>> GetInfo(string userId, DateTime date);

        Task Save(CalendarViewModel calendarViewModel, string userId);
    }
}