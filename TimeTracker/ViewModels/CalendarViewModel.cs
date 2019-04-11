using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.ViewModels
{
    public class CalendarViewModel
    {
        [Required]
        [CustomDate]
        public DateTime Date { get; set; }
        [Required]
        [Range(0, 24, ErrorMessage = "The field \"Hours\" must be between 0 and 24.")]
        public int Hours { get; set; }
    }
    public class CustomDateAttribute : RangeAttribute
    {
        static public DateTime FirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        static public DateTime LastDayOfMonth = FirstDayOfMonth.AddMonths(1).AddDays(-1);

        public CustomDateAttribute() : base(typeof(DateTime), FirstDayOfMonth.ToString("d"), LastDayOfMonth.ToString("d"))
        {
            ErrorMessage = "The field \"Data\" must be between " + FirstDayOfMonth.ToString("d") + " and " + LastDayOfMonth.ToString("d");
        }

    }
}
