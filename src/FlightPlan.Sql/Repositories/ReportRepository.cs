using FlightPlan.Application.Reports;
using FlightPlan.Application.Repositories;
using FlightPlan.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlan.Sql.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly DatabaseContext _context;

        public ReportRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<SummaryReport> GetSummaryReport()
        {
            return await _context.Flights
                 .Include(x => x.DepartureAirport)
                 .Include(x => x.ArrivalAirport)
                 .Include(x => x.Plane)
                 .GroupBy(x => 1)
                 .Select(g => new SummaryReport()
                 {
                     TotalFlights = g.Count(),
                     TotalDistance = g.Sum(x => x.Distance),
                     TotalFuelConsumption = g.Sum(x => x.TotalFuelConsumption)
                 })
                 .FirstOrDefaultAsync();
        }
    }
}
