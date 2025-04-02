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
            var staff = _context.Staff.ToList();
            var reports = _context.Reports.ToList();
            var model = new Tuple<List<Report>, List<Staff>>(reports, staff);
            return View(model);
        }

        #region Report/Create
        // GET Report/Create
        public IActionResult Create()
        {
            var staff = _context.Staff.ToList();
            var reports = _context.Reports.ToList();
            var model = new Tuple<List<Report>, List<Staff>>(reports ?? new List<Report>(), staff ?? new List<Staff>());
            return View(model);
        }

        // POST Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                TempData["SuccessfulMessage"] = "Report created successfully";
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "There was an error creating the report. Please try again.";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

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

                _context.Update(existingReport);
                _context.SaveChanges();

                TempData["SuccessfulMessage"] = "Changes saved to the selected Report successfully";
                return Ok();
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "There was problem saving the changes made to the selected Report. Please try again.";
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

                TempData["SuccessfulMessage"] = "Report deleted successfully";
                return Ok();

            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "There was an error deleting the Report. Please try again.";
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
