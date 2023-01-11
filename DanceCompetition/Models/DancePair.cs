using System;

namespace DanceCompetition.Models
{
    public class DancePair
    {
        public int Id { get; set; }
        public int grade1 { get; set; }
        public int grade2 { get; set; }
        public int grade3 { get; set; }

        public double getAverageGrade()
        {
            return Math.Round((grade1 + grade2 + grade3) / 3.0, 1);
        }

    }
}
