using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        static public DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        static public DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        public CustomDateAttribute() : base(typeof(DateTime), firstDayOfMonth.ToString("d"), lastDayOfMonth.ToString("d"))
        {
            ErrorMessage = "The field \"Data\" must be between " + firstDayOfMonth.ToString("d") + " and " + lastDayOfMonth.ToString("d");
        }

    }
}
