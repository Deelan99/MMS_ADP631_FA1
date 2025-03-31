using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;

namespace MMS_ADP631_FA1.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET Service Request
        public ActionResult Index()
        {
            //var serviceRequests = _context.ServiceRequests.ToList();
            //return View(serviceRequests);
            var serviceRequests = _context.ServiceRequests.ToList();
            var citizens = _context.Citizens.ToList();
            var model = new Tuple<List<ServiceRequest>, List<Citizen>>(serviceRequests, citizens);
            return View(model);
        }

        #region ServiceRequest/Create
        // GET ServiceRequest/Create
        public IActionResult Create()
        {
            var citizens = _context.Citizens.ToList();
            var serviceRequest = new ServiceRequest();
            var model = new Tuple<ServiceRequest, List<Citizen>>(serviceRequest, citizens);
            return View(model);
        }

        // POST ServiceRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceRequest serviceRequest)
        {
            ModelState.Remove("Citizen"); // This is not needed

            // If the passed Citizen ID is 0
            if (serviceRequest.CitizenID == 0)
            {
                ModelState.AddModelError("CitizenID", "Please select a citizen.");
            }

            if (ModelState.IsValid)
            {
                _context.ServiceRequests.Add(serviceRequest);
                TempData["SuccessfulMessage"] = "Service Request created successfully";
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "There was an error creating the Service Request. Please try again.";

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region ServiceRequest/UpdateStatus/{id}
        // GET ServiceRequest/UpdateStatus/{id}
        public IActionResult UpdateStatus(int id)
        {
            var serviceRequest = _context.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }
            return View(serviceRequest);
        }

        // POST ServiceRequest/UpdateStatus/{id}
        [HttpPost, ]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, [Bind("RequestID,Status")] ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.RequestID)
            {
                return NotFound();
            }

            var existingRequest = _context.ServiceRequests.Find(id);
            if (existingRequest == null)
            {
                return NotFound();
            }

            try
            {
                existingRequest.Status = serviceRequest.Status;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        #region ServiceRequest/Details/{id}
        // GET ServiceRequest/Details/{id}
        public IActionResult Details(int id)
        {
            var serviceRequest = _context.ServiceRequests
                .Include(sr => sr.Citizen)
                .FirstOrDefault(sr => sr.RequestID == id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            if (serviceRequest.Citizen == null)
            {
                return BadRequest("Citizen details not found for this service request.");
            }

            return Json(new
            {
                serviceType = serviceRequest.ServiceType,
                requestDate = serviceRequest.RequestDate,
                status = serviceRequest.Status,
                citizenFullName = serviceRequest.Citizen.FullName,
                citizenEmail = serviceRequest.Citizen.Email,
                citizenPhoneNumber = serviceRequest.Citizen.PhoneNumber
            });
        }
        #endregion

        #region ServiceRequest/Delete/{id}
        // GET ServiceRequest/Delete/{id}
        public IActionResult Delete(int id)
        {
            var serviceRequest = _context.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            return View(serviceRequest);
        }

        // POST ServiceRequest/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            try
            {
                var serviceRequest = _context.ServiceRequests.Find(id);
                if (serviceRequest == null)
                {
                    return NotFound();
                }

                _context.ServiceRequests.Remove(serviceRequest);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion
    }
}
