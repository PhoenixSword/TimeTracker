using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.Models
{
    public class Calendar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public int Hours { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public Calendar(DateTime date, int hours, string userId)
        {
            Date = date;
            Hours = hours;
            UserId = userId;
        }

    }
}
