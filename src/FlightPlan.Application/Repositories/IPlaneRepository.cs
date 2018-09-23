using FlightPlan.Application.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightPlan.Application.Repositories
{
    public interface IPlaneRepository
    {
        Task<List<Plane>> GetAll();
        Task<Plane> Get(Guid? id);
        Task<Guid?> CreateOrUpdate(Plane plane);
        Task Delete(Guid? id);
    }
}
