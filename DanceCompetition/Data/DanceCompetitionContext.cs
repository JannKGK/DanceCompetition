using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DanceCompetition.Models;

namespace DanceCompetition.Data
{
    public class DanceCompetitionContext : DbContext
    {
        public DanceCompetitionContext (DbContextOptions<DanceCompetitionContext> options)
            : base(options)
        {
        }

        public DbSet<DanceCompetition.Models.DancePair> DancePair { get; set; } = default!;
    }
}
