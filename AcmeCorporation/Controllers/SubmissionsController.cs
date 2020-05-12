using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcmeCorporation.Models;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace AcmeCorporation.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly AcmeCorporationContext _context;

        public SubmissionsController(AcmeCorporationContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(int? page)
        {
            var submissions = from s in _context.Submission
                                     select s;

            int pageSize = 10;
            int pageNumber = page ?? 1;

            return View(await submissions.ToPagedListAsync(pageNumber, pageSize));
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission
                .FirstOrDefaultAsync(m => m.SubmissionId == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName,EmailAdress,ProductSerial")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                IQueryable<Submission> Submissions = from s in _context.Submission
                                  where s.ProductSerial == submission.ProductSerial
                                  select s;
                IQueryable<PurchasedProduct> ValidSerials = from s in _context.PurchasedProduct
                                                            where s.ProductSerial == submission.ProductSerial
                                                            select s;

                if (Submissions.Count() < 2 && ValidSerials.Count() > 0)
                {
                    _context.Add(submission);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("ErrorPage", "Home", new { contentId = 3});
                }

            }
            return View(submission);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission.FindAsync(id);
            if (submission == null)
            {
                return NotFound();
            }
            return View(submission);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName,EmailAdress,ProductSerial")] Submission submission)
        {
            if (id != submission.SubmissionId)
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
                    if (!SubmissionExists(submission.SubmissionId))
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
            return View(submission);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission
                .FirstOrDefaultAsync(m => m.SubmissionId == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submission = await _context.Submission.FindAsync(id);
            _context.Submission.Remove(submission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmissionExists(int id)
        {
            return _context.Submission.Any(e => e.SubmissionId == id);
        }
    }
}
