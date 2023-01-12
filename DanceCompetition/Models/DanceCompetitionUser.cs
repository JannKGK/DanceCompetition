using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DanceCompetition.Models
{
    public class DanceCompetitionUser : IdentityUser
    {
        [StringLength(1024)]
        public string FavoriteRestaurant { get; set; }
    }
}
