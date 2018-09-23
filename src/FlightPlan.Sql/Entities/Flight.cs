using System;

namespace FlightPlan.Sql.Entities
{
    public class Flight
    {
        public Guid Id { get; set; }

        public Guid? DepartureAirportId { get; set; }
        public Airport DepartureAirport { get; set; }

        public Guid? ArrivalAirportId { get; set; }
        public Airport ArrivalAirport { get; set; }

        public Guid? PlaneId { get; set; }
        public Plane Plane { get; set; }

        public int Distance { get; set; }
        public int TotalFuelConsumption { get; set; }
    }
}
