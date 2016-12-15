using DavidsonRFB.Election.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DavidsonRFB.Election.Business.Data
{
    public class ElectionContext : DbContext
    {
        #region Constructors

        public ElectionContext(DbContextOptions<ElectionContext> options): base(options)
        {
        }

        #endregion

        public DbSet<Models.Election> Elections { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Nominee> Nominees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VotingRight> VotingRights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Election>().ToTable("Election");
            modelBuilder.Entity<MembershipType>().ToTable("MembershipType");
            modelBuilder.Entity<Nominee>().ToTable("Nominee");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<RoleType>().ToTable("RoleType");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Vote>().ToTable("Vote");
            modelBuilder.Entity<VotingRight>().ToTable("VotingRight");
        }
    }
}
