using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DanceCompetition.Models
{
    public class DanceCompetitionRole : IdentityRole
    {
        [StringLength(128, MinimumLength = 1)]
        public string DisplayName { get; set; }
    }
}
