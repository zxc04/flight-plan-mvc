using System.ComponentModel.DataAnnotations;

namespace FlightPlan.Application.Reports
{
    public class SummaryReport
    {
        [Display(Name = "Total Flights")]
        public int TotalFlights { get; set; }

        [Display(Name = "Total Distance (Km)")]
        public int TotalDistance { get; set; }

        [Display(Name = "Total Fuel Consumption (L)")]
        public int TotalFuelConsumption { get; set; }
    }
}
