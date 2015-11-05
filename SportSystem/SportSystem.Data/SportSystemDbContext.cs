
namespace SportSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportSystem.Models;
    using System.Data.Entity;

    public class SportSystemDbContext : IdentityDbContext<User>
    {
        public SportSystemDbContext()
            : base("SportSystemConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Vote> Votes { get; set; }


        public static SportSystemDbContext Create()
        {
            return new SportSystemDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasRequired(match => match.HomeTeam)
                .WithMany(team => team.HomeMatches)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                .HasRequired(match => match.AwayTeam)
                .WithMany(team => team.AwayMatches)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
