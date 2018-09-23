using FlightPlan.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlightPlan.Mvc.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportRepository _repository;

        public ReportsController(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Summary()
        {
            return View(await _repository.GetSummaryReport());
        }
    }
}