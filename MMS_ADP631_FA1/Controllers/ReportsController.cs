using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;

namespace MMS_ADP631_FA1.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET Report
        public IActionResult Index()
        {
            ViewBag.StaffList = _context.Staff.ToList();
            var reports = _context.Reports.ToList();
            return View(reports);
        }

        #region Report/Create
        // GET Report/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        #endregion

        #region Report/ReviewReport
        //GET Report/ReviewReport/{id}
        public IActionResult ReviewReport(int id)
        {
            var report = _context.Reports.FirstOrDefault(r => r.ReportID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Review/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Review(int id, Report report)
        {
            if (id != report.ReportID)
            {
                return NotFound();
            }

            var existingReport = _context.Reports.FirstOrDefault(r => r.ReportID == id);
            if (existingReport == null)
            {
                return NotFound();
            }

            try
            {
                existingReport.ReportType = report.ReportType;
                existingReport.Details = report.Details;
                existingReport.Status = report.Status;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating report: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        #region Report/Delete
        // GET Report/Delete/{id}
        public IActionResult Delete(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST Report/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            try
            {
                var report = _context.Reports.Find(id);
                if (report == null)
                {
                    return NotFound();
                }

                _context.Reports.Remove(report);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        #region Reports/Details
        [HttpGet("Reports/PendingReports")]
        public IActionResult Details(string? status)
        {
            var reports = _context.Reports.Where(r => r.Status == "Pending").ToList();
            return PartialView("_ReportDetails", reports);
        }

        // This method will return a JSON and not a view
        [HttpGet("Reports/Details/{id}")]
        public IActionResult Details(int id)
        {
            var report = _context.Reports
                .Include(r => r.Staff)
                .FirstOrDefault(r => r.ReportID == id);

            if (report == null) return NotFound();

            if (report.Staff == null)
            {
                return BadRequest("Staff details not found for this reports details.");
            }

            return Json(new
            {
                submissionDate = report.SubmissionDate.ToString("dd/MM/yyyy"),
                reportType = report.ReportType,
                details = report.Details,
                status = report.Status,
                citizenFullName = report.Staff.FullName,
                citizenEmail = report.Staff.Email,
                citizenPhoneNumber = report.Staff.PhoneNumber
            });
        }
        #endregion

    }
}
