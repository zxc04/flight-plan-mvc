using FlightPlan.Application.Domain;
using System;
using Xunit;

namespace FlightPlan.UnitTests
{
    public class Flight_Should
    {
        [Fact]
        public void ComputeDistance_WhenAirportsPresent()
        {
            Airport departure = new Airport(Guid.NewGuid(), "Departure", 49, 2.53);
            Airport arrival = new Airport(Guid.NewGuid(), "Arrival", 49.9, 2.53);
            Flight flight = new Flight(Guid.NewGuid(), departure, arrival, null);

            int expected = 100;

            int actual = flight.Distance;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ComputeZeroDistance_WhenArrivalIsMissing()
        {
            Airport departure = new Airport(Guid.NewGuid(), "Departure", 49, 2.53);
            Flight flight = new Flight(Guid.NewGuid(), departure, null, null);

            int expected = 0;

            int actual = flight.Distance;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ComputeZeroDistance_WhenDepartureIsMissing()
        {
            Airport arrival = new Airport(Guid.NewGuid(), "Arrival", 49.9, 2.53);
            Flight flight = new Flight(Guid.NewGuid(), null, arrival, null);

            int expected = 0;

            int actual = flight.Distance;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ComputeTotalFuelConsumption_WhenPlaneIsPresent()
        {
            Airport departure = new Airport(Guid.NewGuid(), "Departure", 49, 2.53);
            Airport arrival = new Airport(Guid.NewGuid(), "Arrival", 49.9, 2.53);
            Plane plane = new Plane(Guid.NewGuid(), "Model", 200, 20);
            Flight flight = new Flight(Guid.NewGuid(), departure, arrival, plane);

            int expected = 220;

            int actual = flight.TotalFuelConsumption;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ComputeZeroFuelConsumption_WhenPlaneIsMissing()
        {
            Airport departure = new Airport(Guid.NewGuid(), "Departure", 49, 2.53);
            Airport arrival = new Airport(Guid.NewGuid(), "Arrival", 49.9, 2.53);
            Flight flight = new Flight(Guid.NewGuid(), departure, arrival, null);

            int expected = 0;

            int actual = flight.TotalFuelConsumption;

            Assert.Equal(expected, actual);
        }
    }
}
