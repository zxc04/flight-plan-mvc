using FlightPlan.Application.Reports;
using System.Threading.Tasks;

namespace FlightPlan.Application.Repositories
{
    public interface IReportRepository
    {
        Task<SummaryReport> GetSummaryReport();
    }
}
