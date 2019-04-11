using TimeTracker.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeTracker.Models.Repositories.Abstract
{
    public interface ICalendarRepo
    {
        Task<Dictionary<int, int>> Get(string userId);

        void Save(CalendarViewModel calendarViewModel, string userId);
    }
}