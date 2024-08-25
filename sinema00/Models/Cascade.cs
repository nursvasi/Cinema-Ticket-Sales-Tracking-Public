using Microsoft.AspNetCore.Mvc.Rendering;

namespace sinema00.Models
{
    public class Cascade
    {
        public IEnumerable<SelectListItem> FilmLists { get; set; }
        public IEnumerable<SelectListItem> SeansLists { get; set; }
    }
}
