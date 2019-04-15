using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.ViewModels
{
    public class CalendarViewModel :  TaskViewModel
    {
        [Required]
        [CustomDate]
        public DateTime Date { get; set; }
    }
    public class CustomDateAttribute : RangeAttribute
    {
        private static DateTime FirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private static DateTime LastDayOfMonth = FirstDayOfMonth.AddMonths(1).AddDays(-1);

        public CustomDateAttribute() : base(typeof(DateTime), FirstDayOfMonth.ToString("d"), LastDayOfMonth.ToString("d"))
        {
            ErrorMessage = "The field \"Data\" must be between " + FirstDayOfMonth.ToString("d") + " and " + LastDayOfMonth.ToString("d");
        }

    }
}
