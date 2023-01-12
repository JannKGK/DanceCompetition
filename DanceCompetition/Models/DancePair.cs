using System;
using System.ComponentModel.DataAnnotations;

namespace DanceCompetition.Models
{
    public class DancePair
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string name { get; set; }

        [Range(0, 5, ErrorMessage = "Grade must be between 0 and 5.")]
        public int grade1 { get; set; }

        [Range(0, 5, ErrorMessage = "Grade must be between 0 and 5.")]
        public int grade2 { get; set; }

        [Range(0, 5, ErrorMessage = "Grade must be between 0 and 5.")]
        public int grade3 { get; set; }

        public double getAverageGrade()
        {
            return Math.Round((grade1 + grade2 + grade3) / 3.0, 1);
        }

    }
}
