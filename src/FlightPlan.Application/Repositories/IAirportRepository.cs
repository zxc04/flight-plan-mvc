using FlightPlan.Application.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightPlan.Application.Repositories
{
    public interface IAirportRepository
    {
        Task<List<Airport>> GetAll();
        Task<Airport> Get(Guid? id);
        Task<Guid?> CreateOrUpdate(Airport airport);
        Task Delete(Guid? id);
    }
}
