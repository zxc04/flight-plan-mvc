using System;

namespace FlightPlan.Sql.Entities
{
    public class Airport
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}