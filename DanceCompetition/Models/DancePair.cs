namespace DanceCompetition.Models
{
    public class DancePair
    {
        public int Id { get; set; }
        public int grade1 { get; set; }
        public int grade2 { get; set; }
        public int grade3 { get; set; } 

        public double averageGrade
        {
            get
            {
                return (grade1+grade2+grade3)/3;
            }
        }

    }
}
