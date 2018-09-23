using FlightPlan.Application.Repositories;
using FlightPlan.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport = FlightPlan.Application.Domain.Airport;
using Flight = FlightPlan.Application.Domain.Flight;
using Plane = FlightPlan.Application.Domain.Plane;

namespace FlightPlan.Sql.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly DatabaseContext _context;

        public FlightRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<List<Flight>> GetAll()
        {
            return await _context
                .Flights
                .Include(x => x.DepartureAirport)
                .Include(x => x.ArrivalAirport)
                .Include(x => x.Plane)
                .Select(x => FromEntity(x))
                .ToListAsync();
        }

        public async Task<Flight> Get(Guid? id)
        {
            var entity = await _context
                .Flights
                .Include(x => x.DepartureAirport)
                .Include(x => x.ArrivalAirport)
                .Include(x => x.Plane)
                .FirstOrDefaultAsync(m => m.Id == id);

            return FromEntity(entity);
        }

        public async Task<Guid?> CreateOrUpdate(Flight flight)
        {
            if (flight.Id == null || flight.Id == Guid.Empty)
                return await Create(flight);
            else
                return await Update(flight);
        }

        public async Task Delete(Guid? id)
        {
            Entities.Flight entity = await _context.Flights.FindAsync(id);
            if (entity != null)
            {
                _context.Flights.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<Guid?> Create(Flight flight)
        {
            Entities.Flight entity = ToEntity(flight);
            entity.Id = Guid.NewGuid();

            _context.Add(entity);
            await _context.SaveChangesAsync();

             return entity.Id;
        }

        private async Task<Guid?> Update(Flight flight)
        {
            Entities.Flight entity = ToEntity(flight);

            _context.Attach(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return flight.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Flights.Any(e => e.Id == flight.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        private Flight FromEntity(Entities.Flight entity)
        {
            if (entity == null)
                return null;

            return new Flight(entity.Id,
                new Airport(entity.DepartureAirport.Id, entity.DepartureAirport.Name, entity.DepartureAirport.Latitude, entity.DepartureAirport.Longitude),
                new Airport(entity.ArrivalAirport.Id, entity.ArrivalAirport.Name, entity.ArrivalAirport.Latitude, entity.ArrivalAirport.Longitude),
                new Plane(entity.Plane.Id, entity.Plane.Model, entity.Plane.FuelConsumptionPer100Km, entity.Plane.TakeoffFuelConsumption));
        }

        private Entities.Flight ToEntity(Flight flight)
        {
            if (flight == null)
                return null;

            return new Entities.Flight()
            {
                Id = flight.Id,
                DepartureAirportId = flight.DepartureAirport.Id,
                ArrivalAirportId = flight.ArrivalAirport.Id,
                PlaneId = flight.Plane.Id,
                Distance = flight.Distance,
                TotalFuelConsumption = flight.TotalFuelConsumption
            };
        }
    }
}
