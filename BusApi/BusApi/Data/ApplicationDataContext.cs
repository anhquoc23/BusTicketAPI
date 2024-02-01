using BusApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Data
{
    public class ApplicationDataContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDataContext() { }
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Station>().HasIndex(s => s.StationName).IsUnique();
            modelBuilder.Entity<Bus>().HasIndex(b => b.BusNumber).IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Bus> Buses { get; set; }
    }
}
