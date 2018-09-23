using System.ComponentModel.DataAnnotations;

namespace FlightPlan.Application.Reports
{
    public class SummaryReport
    {
        [Display(Name = "Total Flights")]
        public int TotalFlights { get; set; }

        [Display(Name = "Total Distance")]
        public int TotalDistance { get; set; }

        [Display(Name = "Total Fuel Consumption")]
        public int TotalFuelConsumption { get; set; }
    }
}
