using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroVeloz.Api.Controllers
{
    public class ConectionsAirlineAirportController : Controller
    {
        // GET: ConectionsAirlineAirportController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ConectionsAirlineAirportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConectionsAirlineAirportController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConectionsAirlineAirportController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConectionsAirlineAirportController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConectionsAirlineAirportController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConectionsAirlineAirportController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ConectionsAirlineAirportController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
