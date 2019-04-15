using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TimeTracker.ViewModels
{
    public class TaskViewModel
    {

        [Required]
        public string Name { get; set; }

        [Range(0, 24, ErrorMessage = "The field \"Hours\" must be between 0 and 24.")]
        public int Hours { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile Image { get; set; }
        public string downloadUrl { get; set; }
    }
}
