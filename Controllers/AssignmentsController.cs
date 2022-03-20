using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuniPortal.Data;
using GuniPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GuniPortal.Controllers
{
    [Authorize(Roles = "Student")]

    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<MyIdentityUser> _userManager;

        public AssignmentsController(ApplicationDbContext context,
            UserManager<MyIdentityUser> userManager,
            IWebHostEnvironment hostEnvironment
            )
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = hostEnvironment;


        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Assignments.Include(a => a.Department).Include(a => a.student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Department)
                .Include(a => a.student)
                .FirstOrDefaultAsync(m => m.Assignment_Id == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            ViewData["Department_Id"] = new SelectList(_context.Departments, "Department_Id", "Department_Name");
            ViewData["Student_Id"] = User.FindFirstValue(ClaimTypes.NameIdentifier).ToUpper();
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Assignment_Id,Student_Id,Assignment_Title,Assignment_Discription,SubmissionFile,Department_Id")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(assignment.SubmissionFile.FileName);
                string fileextention = Path.GetExtension(assignment.SubmissionFile.FileName);
                assignment.Document = filename + DateTime.Now.ToString("yymmssfff") + fileextention;
                string path = Path.Combine(wwwRootPath, "Doc", filename);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await assignment.SubmissionFile.CopyToAsync(filestream);
                }

                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Department_Id"] = new SelectList(_context.Departments, "Department_Id", "Department_Name", assignment.Department_Id);
            ViewData["Student_Id"] = new SelectList(_context.Set<Student>(), "UserId", "EnrollmentID", assignment.Student_Id);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewData["Department_Id"] = new SelectList(_context.Departments, "Department_Id", "Department_Name", assignment.Department_Id);
            ViewData["Student_Id"] = new SelectList(_context.Set<Student>(), "UserId", "EnrollmentID", assignment.Student_Id);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Assignment_Id,Student_Id,Assignment_Title,Assignment_Discription,Document,Department_Id")] Assignment assignment)
        {
            if (id != assignment.Assignment_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Assignment_Id))
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
            ViewData["Department_Id"] = new SelectList(_context.Departments, "Department_Id", "Department_Name", assignment.Department_Id);
            ViewData["Student_Id"] = new SelectList(_context.Set<Student>(), "UserId", "EnrollmentID", assignment.Student_Id);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Department)
                .Include(a => a.student)
                .FirstOrDefaultAsync(m => m.Assignment_Id == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.Assignment_Id == id);
        }
    }
}
