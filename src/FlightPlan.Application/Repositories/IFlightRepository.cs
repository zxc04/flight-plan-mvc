using FlightPlan.Application.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightPlan.Application.Repositories
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetAll();
        Task<Flight> Get(Guid? id);
        Task<Guid?> CreateOrUpdate(Flight flight);
        Task Delete(Guid? id);
    }
}
