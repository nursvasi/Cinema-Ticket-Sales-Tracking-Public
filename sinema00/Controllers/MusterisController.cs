using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sinema00.Models;

namespace sinema00.Controllers
{
    public class MusterisController : Controller
    {
        private readonly sinema00Context _context;

        public MusterisController(sinema00Context context)
        {
            _context = context;
        }

        // GET: Musteris
        public async Task<IActionResult> Index()
        {
              return _context.Musteris != null ? 
                          View(await _context.Musteris.ToListAsync()) :
                          Problem("Entity set 'sinema00Context.Musteris'  is null.");
        }

        // GET: Musteris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Musteris == null)
            {
                return NotFound();
            }

            var musteri = await _context.Musteris
                .FirstOrDefaultAsync(m => m.MusteriId == id);
            if (musteri == null)
            {
                return NotFound();
            }

            return View(musteri);
        }

        // GET: Musteris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musteris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MusteriId,Adi,Soyadi,Email,Telefon")] Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musteri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musteri);
        }

        // GET: Musteris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Musteris == null)
            {
                return NotFound();
            }

            var musteri = await _context.Musteris.FindAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return View(musteri);
        }

        // POST: Musteris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MusteriId,Adi,Soyadi,Email,Telefon")] Musteri musteri)
        {
            if (id != musteri.MusteriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musteri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusteriExists(musteri.MusteriId))
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
            return View(musteri);
        }

        // GET: Musteris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Musteris == null)
            {
                return NotFound();
            }

            var musteri = await _context.Musteris
                .FirstOrDefaultAsync(m => m.MusteriId == id);
            if (musteri == null)
            {
                return NotFound();
            }

            return View(musteri);
        }

        // POST: Musteris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Musteris == null)
            {
                return Problem("Entity set 'sinema00Context.Musteris'  is null.");
            }
            var musteri = await _context.Musteris.FindAsync(id);
            if (musteri != null)
            {
                _context.Musteris.Remove(musteri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusteriExists(int id)
        {
          return (_context.Musteris?.Any(e => e.MusteriId == id)).GetValueOrDefault();
        }
    }
}
