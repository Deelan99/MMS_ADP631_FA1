using Microsoft.AspNetCore.Mvc;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;

namespace MMS_ADP631_FA1.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET Staff
        public ActionResult Index()
        {
            var staff = _context.Staff.ToList();
            return View(staff);
        }

        #region Staff/Create
        // GET Staff/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);

        }
        #endregion

        #region Staff/Edit/{id}
        // GET Staff/Edit/{id}
        public IActionResult Edit(int id)
        {
            var staffMember = _context.Staff.Find(id);
            if (staffMember == null)
            {
                return NotFound();
            }
            return View(staffMember);
        }

        // POST Staff/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Staff staff)
        {
            if (id != staff.StaffID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    if (!_context.Staff.Any(s => s.StaffID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }
        #endregion

        #region Staff/Details/{id}
        // GET Staff/Details/{id}
        public IActionResult Details(int id)
        {
            var staffMember = _context.Staff.Find(id);
            if (staffMember == null)
            {
                return NotFound();
            }

            return Json(new
            {
                fullName = staffMember.FullName,
                department = staffMember.Department,
                position = staffMember.Position,
                phoneNumber = staffMember.PhoneNumber,
                email = staffMember.Email,
                hireDate = staffMember.HireDate.ToString("dd/MM/yyyy")
            }); 
        }
        #endregion

        #region Staff/Delete/{id}
        // GET Staff/Delete/{id}
        public IActionResult Delete(int id)
        {
            var staffMember = _context.Staff.Find(id);
            if (staffMember == null)
            {
                return NotFound();
            }
            return View(staffMember);
        }

        // POST Staff/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            var staffMember = _context.Staff.Find(id);
            _context.Staff.Remove(staffMember);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
