using FlightPlan.Application.Repositories;
using FlightPlan.Sql.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlan.Sql.Repositories
{
    public class DataInitializer : IDataInitializer
    {
        private readonly DatabaseContext _context;

        public DataInitializer(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Initialize()
        {
            _context.Database.EnsureCreated();

            if (!_context.Airports.Any())
            {
                var airports = new Airport[]
                {
                    new Airport() { Name = "Paris", Latitude = 49.0096941, Longitude = 2.5457358 },
                    new Airport() { Name = "London", Latitude = 51.4700256, Longitude = -0.4564842 },
                    new Airport() { Name = "Frankfurt", Latitude = 50.037936, Longitude = 8.5599631 },
                    new Airport() { Name = "New York", Latitude = 40.6413151, Longitude = -73.7803278 },
                };

                foreach (var airport in airports)
                {
                    _context.Airports.Add(airport);
                }
            }

            if (!_context.Planes.Any())
            {
                var planes = new Plane[]
                {
                    new Plane() { Model = "A380", FuelConsumptionPer100Km = 400, TakeoffFuelConsumption = 50 },
                    new Plane() { Model = "A381", FuelConsumptionPer100Km = 450, TakeoffFuelConsumption = 60 },
                    new Plane() { Model = "A382", FuelConsumptionPer100Km = 500, TakeoffFuelConsumption = 70 },
                    new Plane() { Model = "A383", FuelConsumptionPer100Km = 550, TakeoffFuelConsumption = 80 }
                };

                foreach (var plane in planes)
                {
                    _context.Planes.Add(plane);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
