using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DanceCompetition.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DanceCompetition.Data
{
    public class DanceCompetitionContext : IdentityDbContext
    {
        public DanceCompetitionContext (DbContextOptions<DanceCompetitionContext> options)
            : base(options)
        {
        }

        public DbSet<DanceCompetition.Models.DancePair> DancePair { get; set; } = default!;
        public DbSet<DanceCompetitionUser> DanceCompetitionUsers { get; set; }
        public DbSet<DanceCompetitionRole> DanceCompetitionRoles { get; set; }

    }
}
