using FlightPlan.Application.Domain;
using FlightPlan.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlan.Mvc
{
    public class FlightsController : Controller
    {
        private readonly IFlightRepository _repository;
        private readonly IAirportRepository _airportRepository;
        private readonly IPlaneRepository _planeRepository;

        public FlightsController(IFlightRepository repository, IAirportRepository airportRepository, IPlaneRepository planeRepository)
        {
            _repository = repository;
            _airportRepository = airportRepository;
            _planeRepository = planeRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _repository.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        public async Task<IActionResult> Create()
        {
            await SetViewData(null);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartureAirport,ArrivalAirport,Plane")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                flight.DepartureAirport = await _airportRepository.Get(flight.DepartureAirport.Id);
                flight.ArrivalAirport = await _airportRepository.Get(flight.ArrivalAirport.Id);
                flight.Plane = await _planeRepository.Get(flight.Plane.Id);

                var result = await _repository.CreateOrUpdate(flight);

                if (result == null)
                    return RedirectToAction(nameof(Index));

                return RedirectToAction(nameof(Details), new { id = result });
            }

            await SetViewData(flight);

            return View(flight);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _repository.Get(id);
            if (flight == null)
            {
                return NotFound();
            }

            await SetViewData(flight);

            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DepartureAirport,ArrivalAirport,Plane")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                flight.DepartureAirport = await _airportRepository.Get(flight.DepartureAirport.Id);
                flight.ArrivalAirport = await _airportRepository.Get(flight.ArrivalAirport.Id);
                flight.Plane = await _planeRepository.Get(flight.Plane.Id);

                var result = await _repository.CreateOrUpdate(flight);

                if (result == null)
                    return NotFound();

                return RedirectToAction(nameof(Details), new { id = result });
            }

            await SetViewData(flight);

            return View(flight);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _repository.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task SetViewData(Flight flight)
        {
            var airports = await _airportRepository.GetAll();
            var planes = await _planeRepository.GetAll();
            ViewData["ArrivalAirportId"] = new SelectList(airports, "Id", "Name", flight?.ArrivalAirport?.Id);
            ViewData["DepartureAirportId"] = new SelectList(airports, "Id", "Name", flight?.DepartureAirport?.Id);
            ViewData["PlaneId"] = new SelectList(planes, "Id", "Model", flight?.Plane?.Id);
        }
    }
}
