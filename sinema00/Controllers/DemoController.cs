using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using sinema00.Models;
using System.Linq;

namespace sinema00.Controllers
{
    public class DemoController : Controller
    {
        private readonly sinema00Context _context;
        Cascade cd = new Cascade();
        public DemoController(sinema00Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            cd.FilmLists = new SelectList(_context.Films, "FilmId", "FilmAdi");
            cd.SeansLists = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            return View(cd);
        }

        public JsonResult GetSeansByFilmId(int filmId)
        {
            var seansLists = _context.Seans
                                     .Where(s => s.FilmId == filmId)
                                     .Select(s => new
                                     {
                                         text = s.SeansSaati.ToString("g"),  // Tarih formatı kontrol edilerek string'e çevrildi
                                         value = s.SeansId
                                     })
                                     .ToList();
            return Json(seansLists);
        }
    }
}
