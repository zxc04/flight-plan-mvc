using FlightPlan.Application.Domain;
using FlightPlan.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlightPlan.Mvc.Controllers
{
    public class PlanesController : Controller
    {
        private readonly IPlaneRepository _repository;

        public PlanesController(IPlaneRepository repository)
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

            var plane = await _repository.Get(id);

            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,FuelConsumptionPer100Km,TakeoffFuelConsumption")] Plane plane)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.CreateOrUpdate(plane);

                if (result == null)
                    return RedirectToAction(nameof(Index));

                return RedirectToAction(nameof(Details), new { id = result });
            }
            return View(plane);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await _repository.Get(id);
            if (plane == null)
            {
                return NotFound();
            }
            return View(plane);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Model,FuelConsumptionPer100Km,TakeoffFuelConsumption")] Plane plane)
        {
            if (id != plane.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _repository.CreateOrUpdate(plane);

                if (result == null)
                    return NotFound();

                return RedirectToAction(nameof(Details), new { id = result });
            }
            return View(plane);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await _repository.Get(id);
            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
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
