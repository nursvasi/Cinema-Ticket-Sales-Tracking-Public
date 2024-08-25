using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sinema00.Models;

namespace sinema00.Controllers
{
    public class SeansController : Controller
    {
        private readonly sinema00Context _context;

        public SeansController(sinema00Context context)
        {
            _context = context;
        }

        // GET: Seans
        public async Task<IActionResult> Index()
        {
            var sinema00Context = _context.Seans.Include(s => s.Film).Include(s => s.Salon);
            return View(await sinema00Context.ToListAsync());
        }

        // GET: Seans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seans == null)
            {
                return NotFound();
            }

            var sean = await _context.Seans
                .Include(s => s.Film)
                .Include(s => s.Salon)
                .FirstOrDefaultAsync(m => m.SeansId == id);
            if (sean == null)
            {
                return NotFound();
            }

            return View(sean);
        }

        // GET: Seans/Create
        public IActionResult Create()
        {
            ViewData["FilmId"] = new SelectList(_context.Films, "FilmId", "FilmAdi");
            ViewData["SalonId"] = new SelectList(_context.Salons, "SalonId", "SalonAdi");
            return View();
        }

        // POST: Seans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeansId,FilmId,SalonId,SeansSaati")] Sean sean)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sean);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmId"] = new SelectList(_context.Films, "FilmId", "FilmAdi", sean.FilmId);
            ViewData["SalonId"] = new SelectList(_context.Salons, "SalonId", "SalonAdi", sean.SalonId);
            return View(sean);
        }

        // GET: Seans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seans == null)
            {
                return NotFound();
            }

            var sean = await _context.Seans.FindAsync(id);
            if (sean == null)
            {
                return NotFound();
            }
            ViewData["FilmId"] = new SelectList(_context.Films, "FilmId", "FilmAdi", sean.FilmId);
            ViewData["SalonId"] = new SelectList(_context.Salons, "SalonId", "SalonAdi", sean.SalonId);
            return View(sean);
        }

        // POST: Seans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeansId,FilmId,SalonId,SeansSaati")] Sean sean)
        {
            if (id != sean.SeansId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sean);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeanExists(sean.SeansId))
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
            ViewData["FilmId"] = new SelectList(_context.Films, "FilmId", "FilmAdi", sean.FilmId);
            ViewData["SalonId"] = new SelectList(_context.Salons, "SalonId", "SalonAdi", sean.SalonId);
            return View(sean);
        }

        // GET: Seans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seans == null)
            {
                return NotFound();
            }

            var sean = await _context.Seans
                .Include(s => s.Film)
                .Include(s => s.Salon)
                .FirstOrDefaultAsync(m => m.SeansId == id);
            if (sean == null)
            {
                return NotFound();
            }

            return View(sean);
        }

        // POST: Seans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seans == null)
            {
                return Problem("Entity set 'sinema00Context.Seans'  is null.");
            }
            var sean = await _context.Seans.FindAsync(id);
            if (sean != null)
            {
                _context.Seans.Remove(sean);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeanExists(int id)
        {
            return (_context.Seans?.Any(e => e.SeansId == id)).GetValueOrDefault();
        }

        public JsonResult GetSeansBySalonAndFilmId(int salonId, int filmId)
        {
            var seanslar = _context.Seans
                .Where(s => s.SalonId == salonId && s.FilmId == filmId)
                .Select(s => new
                {
                    s.SeansId,
                    SeansSaati = s.SeansSaati.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();

            return new JsonResult(seanslar);
        }
    }
}
