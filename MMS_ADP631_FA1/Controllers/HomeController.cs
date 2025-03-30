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
            _context = context;
        }

        public IActionResult Index()
        {
            var serviceRequests = _context.ServiceRequests.ToList();
            var citizens = _context.Citizens.ToList();
            var staff = _context.Staff.ToList();
            var pendingReports = _context.Reports.Where(r => r.Status == "Pending").ToList();

            var home = new Tuple<List<ServiceRequest>, List<Citizen>, List<Staff>, List<Report>>(serviceRequests, citizens, staff, pendingReports);
            return View(home);
        }
    }
}
