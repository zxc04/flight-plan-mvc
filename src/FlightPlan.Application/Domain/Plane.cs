using System;
using System.ComponentModel.DataAnnotations;

namespace FlightPlan.Application.Domain
{
    public class Plane
    {
        public Guid Id { get; set; }

        [Display(Name = "Model")]
        [MaxLength(100)]
        public string Model { get; set; }

        [Display(Name = "Fuel Consumption (L / 100Km)")]
        public int FuelConsumptionPer100Km { get; set; }

        [Display(Name = "Takeoff Effort (L)")]
        public int TakeoffFuelConsumption { get; set; }

        public Plane()
        {
        }

        public Plane(Guid id, string model, int fuelConsumptionPer100Km, int takeoffFuelConsumption)
        {
            Id = id;
            Model = model;
            FuelConsumptionPer100Km = fuelConsumptionPer100Km;
            TakeoffFuelConsumption = takeoffFuelConsumption;
        }
    }
}
