using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using DataAccessLayer.Context;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Commons;

namespace Web.Controllers
{
    public class TherapistsController : Controller
    {
        private readonly ITherapistService _service;

        public TherapistsController(ITherapistService therapistService)
        {
            _service = therapistService;
        }

        // GET: Therapists
        public async Task<IActionResult> Index(int page = 1, string? searchTerm = null)
        {
            PaginationResult<Therapist> result = await _service.GetTherapistPaginatedAsync(searchTerm, page, 10);
            ViewData["therapists"] = result;
            return View();
        }

        public async Task<IActionResult> Details(Guid therapist_id)
        {
            ViewData["therapists"] = await _service.GetTherapistWithIdAsync(therapist_id); 
            return View(await _service.GetTherapistWithIdAsync(therapist_id));
        }

        //// GET: Therapists/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var therapist = await _context.Therapists
        //        .Include(t => t.UserNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (therapist == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(therapist);
        //}

        //// GET: Therapists/Create
        //public IActionResult Create()
        //{
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
        //    return View();
        //}

        //// POST: Therapists/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ExperienceYears,Biography,Status,UserId,Id,CreatedAt,LastUpdate")] Therapist therapist)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        therapist.Id = Guid.NewGuid();
        //        _context.Add(therapist);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", therapist.UserId);
        //    return View(therapist);
        //}

        //// GET: Therapists/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var therapist = await _context.Therapists.FindAsync(id);
        //    if (therapist == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", therapist.UserId);
        //    return View(therapist);
        //}

        //// POST: Therapists/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("ExperienceYears,Biography,Status,UserId,Id,CreatedAt,LastUpdate")] Therapist therapist)
        //{
        //    if (id != therapist.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(therapist);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TherapistExists(therapist.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", therapist.UserId);
        //    return View(therapist);
        //}

        //// GET: Therapists/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var therapist = await _context.Therapists
        //        .Include(t => t.UserNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (therapist == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(therapist);
        //}

        //// POST: Therapists/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var therapist = await _context.Therapists.FindAsync(id);
        //    if (therapist != null)
        //    {
        //        _context.Therapists.Remove(therapist);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TherapistExists(Guid id)
        //{
        //    return _context.Therapists.Any(e => e.Id == id);
        //}
    }
}
