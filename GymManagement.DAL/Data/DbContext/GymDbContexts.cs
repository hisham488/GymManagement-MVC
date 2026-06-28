using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagement.DAL.Data.DbContexts
{
    public class GymDbContexts : DbContext
    {
        public GymDbContexts(DbContextOptions<GymDbContexts> options) : base(options)
        {

        }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
