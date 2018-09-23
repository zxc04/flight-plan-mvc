using FlightPlan.Application.Repositories;
using FlightPlan.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plane = FlightPlan.Application.Domain.Plane;

namespace FlightPlan.Sql.Repositories
{
    public class PlaneRepository : IPlaneRepository
    {
        private readonly DatabaseContext _context;

        public PlaneRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<List<Plane>> GetAll()
        {
            return await _context
                .Planes
                .Select(x => FromEntity(x))
                .ToListAsync();
        }

        public async Task<Plane> Get(Guid? id)
        {
            var entity = await _context.Planes.FirstOrDefaultAsync(m => m.Id == id);

            return FromEntity(entity);
        }

        public async Task<Guid?> CreateOrUpdate(Plane plane)
        {
            if (plane.Id == null || plane.Id == Guid.Empty)
                return await Create(plane);
            else
                return await Update(plane);
        }

        public async Task Delete(Guid? id)
        {
            Entities.Plane entity = await _context.Planes.FindAsync(id);
            if (entity != null)
            {
                _context.Planes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<Guid?> Create(Plane plane)
        {
            Entities.Plane entity = ToEntity(plane);
            entity.Id = Guid.NewGuid();

            _context.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        private async Task<Guid?> Update(Plane plane)
        {
            Entities.Plane entity = ToEntity(plane);

            _context.Attach(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return plane.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Planes.Any(e => e.Id == plane.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        private Plane FromEntity(Entities.Plane entity)
        {
            if (entity == null)
                return null;

            return new Plane(entity.Id, entity.Model, entity.FuelConsumptionPer100Km, entity.TakeoffFuelConsumption);
        }

        private Entities.Plane ToEntity(Plane plane)
        {
            if (plane == null)
                return null;

            return new Entities.Plane()
            {
                Id = plane.Id,
                Model = plane.Model,
                FuelConsumptionPer100Km = plane.FuelConsumptionPer100Km,
                TakeoffFuelConsumption = plane.TakeoffFuelConsumption
            };
        }
    }
}
