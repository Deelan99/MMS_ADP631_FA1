﻿using Microsoft.AspNetCore.Mvc;
using MMS_ADP631_FA1.Data;
using MMS_ADP631_FA1.Models;

namespace MMS_ADP631_FA1.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET Staff
        public ActionResult Index()
        {
            if (_context == null)
            {
                return Problem("Database context is not available.");
            }

            var staff = _context.Staff?.ToList() ?? new List<Staff>();
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
                
                if (TempData != null)
                {
                    TempData["SuccessfulMessage"] = "New Staff Member created successfully";
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (TempData != null)
                {
                    TempData["ErrorMessage"] = "There was an error creating the Staff Member. Please try again.";
                }

                return RedirectToAction(nameof(Index));
            }

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

            try
            {
                _context.Update(staff);
                _context.SaveChanges();

                if (TempData != null)
                {
                    TempData["SuccessfulMessage"] = "Changes saved to the selected Staff Member successfully";
                }

                return Ok();
            }
            catch (Exception)
            {
                if (TempData != null)
                {
                    TempData["ErrorMessage"] = "There was problem saving the changes made to the selected Staff Member. Please try again.";
                }

                return StatusCode(500, "Internal server error");
            }
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

        // POST Staff/DeleteConfirmation/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            try
            {
                var staffMember = _context.Staff.Find(id);
                if (staffMember == null)
                {
                    return NotFound();
                }
                _context.Staff.Remove(staffMember);
                _context.SaveChanges();

                if (TempData != null)
                {
                    TempData["SuccessfulMessage"] = "Staff Member deleted successfully";
                }

                return Ok();

            }
            catch (Exception)
            {
                if (TempData != null)
                {
                    TempData["ErrorMessage"] = "There was an error deleting the Staff Member. Please try again.";
                }

                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

    }
}
