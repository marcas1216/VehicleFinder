
using Microsoft.EntityFrameworkCore;
using Vehicles.Domain.Entities;

namespace Vehicles.Infrastructure.EF
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options)
            : base(options) { }

        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Reservation>()
                .Property(x => x.Status)
                .HasConversion<string>();
        }
    }
}
