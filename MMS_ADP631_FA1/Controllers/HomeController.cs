using Microsoft.AspNetCore.Mvc;
using MMS_ADP631_FA1.Data;

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
            var citizens = _context.Citizens.ToList();
            return View(citizens);
        }
    }
}
