using GeoCoordinatePortable;
using System;
using System.ComponentModel.DataAnnotations;

namespace FlightPlan.Application.Domain
{
    public class Airport
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        public GeoCoordinate Coordinates => new GeoCoordinate(Latitude, Longitude);

        public Airport()
        {
        }

        public Airport(Guid id, string name, double latitude, double longitude)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}