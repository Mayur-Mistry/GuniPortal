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
using System.Security.Claims;

namespace GuniPortal.Controllers
{
    [Authorize(Roles = "Faculty")]

    public class SubmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Submissions
        public async Task<IActionResult> Index()
        {
            var resu= _context.Submissions.Join(_context.Assignments, x => x.Assignment_Id,y=>y.Assignment_Id,(x,y)=>x);
            ViewData["Faculty_Id"] = User.FindFirstValue(ClaimTypes.NameIdentifier).ToUpper();

            var applicationDbContext = _context.Submissions.Include(s => s.Assignment).Include(s => s.Faculty);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Submissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submissions
                .Include(s => s.Assignment)
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.Submission_Id == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // GET: Submissions/Create
        public IActionResult Create(int id)
        {
            ViewData["Assignment_Id"] = id;
            ViewData["Faculty_Id"] = User.FindFirstValue(ClaimTypes.NameIdentifier).ToUpper();
            return View();
        }

        // POST: Submissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Submission_Id,StartingDate,EndingDate,Status,Faculty_Id,Assignment_Id")] Submission submission)
        {
            if (ModelState.IsValid)
            {

                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Assignment_Id"] = new SelectList(_context.Assignments, "Assignment_Id", "Assignment_Title", submission.Assignment_Id);
            ViewData["Faculty_Id"] = new SelectList(_context.Set<Faculty>(), "UserId", "UserId", submission.Faculty_Id);
            return View(submission);
        }

        // GET: Submissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null)
            {
                return NotFound();
            }
            ViewData["Assignment_Id"] = id;
            ViewData["Faculty_Id"] = User.FindFirstValue(ClaimTypes.NameIdentifier).ToUpper();
            return View(submission);
        }

        // POST: Submissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Submission_Id,StartingDate,EndingDate,Status,Faculty_Id,Assignment_Id")] Submission submission)
        {
            if (id != submission.Submission_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmissionExists(submission.Submission_Id))
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
            ViewData["Assignment_Id"] = new SelectList(_context.Assignments, "Assignment_Id", "Assignment_Title", submission.Assignment_Id);
            ViewData["Faculty_Id"] = new SelectList(_context.Set<Faculty>(), "UserId", "UserId", submission.Faculty_Id);
            return View(submission);
        }

        // GET: Submissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submissions
                .Include(s => s.Assignment)
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.Submission_Id == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // POST: Submissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            _context.Submissions.Remove(submission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

      /*  public ActionResult AssignmentView()
        {
            
            return View();
        }*/
        public async Task<IActionResult> AssignmentView()
        {
            var applicationDbContext = _context.Assignments.Include(a => a.Department).Include(a => a.student);
            return View(await applicationDbContext.ToListAsync());
        }

        private bool SubmissionExists(int id)
        {
            return _context.Submissions.Any(e => e.Submission_Id == id);
        }
    }
}
