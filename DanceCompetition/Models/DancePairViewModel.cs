using System.Collections.Generic;

namespace DanceCompetition.Models
{
    public class DancePairViewModel
    {
        public List<DancePair> MissingGrade1 { get; set; }
        public List<DancePair> MissingGrade2 { get; set; }
        public List<DancePair> MissingGrade3 { get; set; }
    }
}
