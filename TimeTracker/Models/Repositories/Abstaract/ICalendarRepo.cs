using TimeTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models.Repositories.Abstract
{
    public interface ICalendarRepo
    {
        Task<Dictionary<int, int>> Get(string UserId);

        string Save(CalendarViewModel calendarViewModel, string UserId);
    }
}