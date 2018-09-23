using System;

namespace FlightPlan.Sql.Entities
{
    public class Plane
    {
        public Guid Id { get; set; }

        public string Model { get; set; }
        public int FuelConsumptionPer100Km { get; set; }
        public int TakeoffFuelConsumption { get; set; }
    }
}
