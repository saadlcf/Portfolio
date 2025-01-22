using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaadPortfolio.Models;

namespace SaadPortfolio.Data
{
    public class SaadPortfolioContext : DbContext
    {
        public SaadPortfolioContext (DbContextOptions<SaadPortfolioContext> options)
            : base(options)
        {
        }

        public DbSet<SaadPortfolio.Models.Project> Project { get; set; } = default!;
        public DbSet<SaadPortfolio.Models.Skill> Skill { get; set; } = default!;
        public DbSet<SaadPortfolio.Models.Experience> Experience { get; set; } = default!;
        public DbSet<SaadPortfolio.Models.Message> Message { get; set; } = default!;
    }
}
