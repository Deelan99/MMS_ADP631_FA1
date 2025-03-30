using Microsoft.AspNetCore.Mvc;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;

namespace MyApp.Namespace
{
    public class CitizenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitizenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Citizen
        public ActionResult Index()
        {
            var citizens = _context.Citizens.ToList();
            return View(citizens);
        }

        #region Citizen/Create
        // GET Citizen/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST Citizen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Citizen citizen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citizen);
                _context.SaveChanges();

                TempData["SuccessfulMessage"] = "New Citizen registered successfully";
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "There was an error registering the Citizen. Please try again.";
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");

            }
        }
        #endregion

        #region Citizen/Edit
        // GET Citizen/Edit/{id}
        public IActionResult Edit(int id)
        {
            var citizen = _context.Citizens.Find(id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // POST Citizen/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Citizen citizen)
        {
            if (id != citizen.CitizenID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(citizen);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        #region Citizen/Delete
        // GET Citizen/Delete/{id}
        public IActionResult Delete(int id)
        {
            var citizen = _context.Citizens.Find(id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);

        }

        // POST Citizen/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            try
            {
                var citizen = _context.Citizens.Find(id);
                if (citizen == null)
                {
                    return NotFound();
                }

                _context.Citizens.Remove(citizen);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        #region Citizen/Details/{id}
        // GET Citizen/Details/{id}
        // This method returns JSON instead of a View:
        public IActionResult Details(int id)
        {
            var citizen = _context.Citizens.Find(id);
            if (citizen == null) return NotFound();

            return Json(new
            {
                fullName = citizen.FullName,
                address = citizen.Address,
                phoneNumber = citizen.PhoneNumber,
                email = citizen.Email,
                dateOfBirth = citizen.DateOfBirth?.ToString("dd/MM/yyyy"),
                registrationDate = citizen.RegistrationDate.ToString("dd/MM/yyyy")
            });
        }
        #endregion
    }
}
