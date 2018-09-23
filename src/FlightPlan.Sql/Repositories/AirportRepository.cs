using FlightPlan.Application.Repositories;
using FlightPlan.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport = FlightPlan.Application.Domain.Airport;

namespace FlightPlan.Sql.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly DatabaseContext _context;

        public AirportRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<List<Airport>> GetAll()
        {
            return await _context
                .Airports
                .Select(x => FromEntity(x))
                .ToListAsync();
        }

        public async Task<Airport> Get(Guid? id)
        {
            var entity = await _context.Airports.FirstOrDefaultAsync(m => m.Id == id);

            return FromEntity(entity);
        }

        public async Task<Guid?> CreateOrUpdate(Airport airport)
        {
            if (airport.Id == null || airport.Id == Guid.Empty)
                return await Create(airport);
            else
                return await Update(airport);
        }

        public async Task Delete(Guid? id)
        {
            Entities.Airport entity = await _context.Airports.FindAsync(id);
            if (entity != null)
            {
                _context.Airports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<Guid?> Create(Airport airport)
        {
            Entities.Airport entity = ToEntity(airport);
            entity.Id = Guid.NewGuid();

            _context.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        private async Task<Guid?> Update(Airport airport)
        {
            Entities.Airport entity = ToEntity(airport);

            _context.Attach(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return airport.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Airports.Any(e => e.Id == airport.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        private Airport FromEntity(Entities.Airport entity)
        {
            if (entity == null)
                return null;

            return new Airport(entity.Id, entity.Name, entity.Latitude, entity.Longitude);
        }

        private Entities.Airport ToEntity(Airport airport)
        {
            if (airport == null)
                return null;

            return new Entities.Airport()
            {
                Id = airport.Id,
                Name = airport.Name,
                Latitude = airport.Coordinates.Latitude,
                Longitude = airport.Coordinates.Longitude
            };
        }
    }
}
