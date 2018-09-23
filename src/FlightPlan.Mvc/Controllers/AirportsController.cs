using FlightPlan.Application.Domain;
using FlightPlan.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlightPlan.Mvc.Controllers
{
    public class AirportsController : Controller
    {
        private readonly IAirportRepository _repository;

        public AirportsController(IAirportRepository repository)
        {
            _repository = repository;
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

            var airport = await _repository.Get(id);

            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Latitude,Longitude")] Airport airport)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.CreateOrUpdate(airport);

                if (result == null)
                    return RedirectToAction(nameof(Index));

                return RedirectToAction(nameof(Details), new { id = result });
            }
            return View(airport);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _repository.Get(id);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Latitude,Longitude")] Airport airport)
        {
            if (id != airport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _repository.CreateOrUpdate(airport);

                if (result == null)
                    return NotFound();

                return RedirectToAction(nameof(Details), new { id = result });
            }
            return View(airport);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _repository.Get(id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
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
    }
}
