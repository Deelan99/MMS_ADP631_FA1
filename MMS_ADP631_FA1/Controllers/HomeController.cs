using Microsoft.AspNetCore.Mvc;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;

namespace MMS_ADP631_FA1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public IActionResult Index()
        {
            // This is to assist with the redirect logic of creating a new Citizen or Service Request
            if (TempData != null)
            {
                TempData["Home"] = true;
            }

            var serviceRequests = _context.ServiceRequests.ToList();
            var citizens = _context.Citizens.ToList();
            var staff = _context.Staff.ToList();
            var pendingReports = _context.Reports.Where(r => r.Status == "Pending").ToList();

            var home = new Tuple<List<ServiceRequest>, List<Citizen>, List<Staff>, List<Report>>(serviceRequests, citizens, staff, pendingReports);
            return View(home);
        }
    }
}
