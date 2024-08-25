using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sinema00.Models;

namespace sinema00.Controllers
{
    [Authorize]//3.adım: eğer login durumundaysan sadece Home a girebilirsin.
    public class BiletsController : Controller
    {
        private readonly sinema00Context _context;

        public BiletsController(sinema00Context context)
        {
            _context = context;
        }

        // GET: Bilets
        public async Task<IActionResult> Index()
        {
            var sinema00Context = _context.Bilets.Include(b => b.Musteri).Include(b => b.Seans);
            return View(await sinema00Context.ToListAsync());
        }
         
        // GET: Bilets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bilets == null)
            {
                return NotFound();
            }

            var bilet = await _context.Bilets
                .Include(b => b.Musteri)
                .Include(b => b.Seans)
                .FirstOrDefaultAsync(m => m.BiletId == id);
            if (bilet == null)
            {
                return NotFound();
            }

            return View(bilet);
        }

        // GET: Bilets/Create
        public IActionResult Create()
        {
            ViewData["MusteriId"] = new SelectList(_context.Musteris, "MusteriId", "Email");
            ViewData["SeansId"] = new SelectList(_context.Seans, "SeansId", "SeansSaati");
            return View();
        }

        // POST: Bilets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BiletId,SeansId,MusteriId,Fiyat")] Bilet bilet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bilet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MusteriId"] = new SelectList(_context.Musteris, "MusteriId", "Email", bilet.MusteriId);
            ViewData["SeansId"] = new SelectList(_context.Seans, "SeansId", "SeansSaati", bilet.SeansId);
            return View(bilet);
        }

        // GET: Bilets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bilets == null)
            {
                return NotFound();
            }

            var bilet = await _context.Bilets.FindAsync(id);
            if (bilet == null)
            {
                return NotFound();
            }
            ViewData["MusteriId"] = new SelectList(_context.Musteris, "MusteriId", "Email", bilet.MusteriId);
            ViewData["SeansId"] = new SelectList(_context.Seans, "SeansId", "SeansSaati", bilet.SeansId);
            return View(bilet);
        }

        // POST: Bilets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BiletId,SeansId,MusteriId,Fiyat")] Bilet bilet)
        {
            if (id != bilet.BiletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bilet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiletExists(bilet.BiletId))
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
            ViewData["MusteriId"] = new SelectList(_context.Musteris, "MusteriId", "Email", bilet.MusteriId);
            ViewData["SeansId"] = new SelectList(_context.Seans, "SeansId", "SeansSaati", bilet.SeansId);
            return View(bilet);
        }

        // GET: Bilets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bilets == null)
            {
                return NotFound();
            }

            var bilet = await _context.Bilets
                .Include(b => b.Musteri)
                .Include(b => b.Seans)
                .FirstOrDefaultAsync(m => m.BiletId == id);
            if (bilet == null)
            {
                return NotFound();
            }

            return View(bilet);
        }

        // POST: Bilets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bilets == null)
            {
                return Problem("Entity set 'sinema00Context.Bilets'  is null.");
            }
            var bilet = await _context.Bilets.FindAsync(id);
            if (bilet != null)
            {
                _context.Bilets.Remove(bilet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BiletExists(int id)
        {
          return (_context.Bilets?.Any(e => e.BiletId == id)).GetValueOrDefault();
        }
    }
}
