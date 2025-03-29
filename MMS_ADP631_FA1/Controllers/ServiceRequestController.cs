using Microsoft.AspNetCore.Mvc;
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
            var serviceRequests = _context.ServiceRequests.ToList();
            return View(serviceRequests);
        }

        #region ServiceRequest/Create
        // GET ServiceRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST ServiceRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceRequest);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceRequest);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.RequestID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Add(serviceRequest);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceRequest);
        }
        #endregion

        #region ServiceRequest/Details/{id}
        // GET ServiceRequest/Details/{id}
        public IActionResult Details(int id)
        {
            var serviceRequest = _context.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            return View(serviceRequest);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            var serviceRequest = _context.ServiceRequests.Find(id);
            _context.ServiceRequests.Remove(serviceRequest);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
