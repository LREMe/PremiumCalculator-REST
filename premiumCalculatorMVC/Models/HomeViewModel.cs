using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TranzactProgrammingChallengeMVC.Models
{
    public class HomeViewModel
    {
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public SelectList? States { get; set; }


        [Display(Name = "State")]
        [Required]
        public string? SelectedState { get; set; }

        public SelectList? Plans { get; set; }
        [Display(Name = "Plan")]
        [Required]
        public string? SelectedPlan { get; set; }

        public SelectList? Periods { get; set; }
        [Display(Name = "Period")]
        public string? SelectedPeriod { get; set; }

        [Display(Name = "Age")]
        [Required]
        public int Age { get; set; }
    }
}
