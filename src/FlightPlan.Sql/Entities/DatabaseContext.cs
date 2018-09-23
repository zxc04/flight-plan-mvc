using Microsoft.EntityFrameworkCore;

namespace FlightPlan.Sql.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>()
                .ToTable("Airports")
                .Property(x => x.Name).HasMaxLength(200)
                ;

            modelBuilder.Entity<Plane>()
                .ToTable("Planes")
                .Property(x => x.Model).HasMaxLength(100)
                ;

            modelBuilder.Entity<Flight>().ToTable("Flights");
            modelBuilder.Entity<Flight>().HasOne(x => x.DepartureAirport).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Flight>().HasOne(x => x.ArrivalAirport).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Flight>().HasOne(x => x.Plane).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
