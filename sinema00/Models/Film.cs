using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sinema00.Models
{
    [Table("Film")]
    public partial class Film
    {
        public Film()
        {
            Seans = new HashSet<Sean>();
        }

        [Key]
        [Column("FilmID")]
        public int FilmId { get; set; }
        [StringLength(100)]
        public string FilmAdi { get; set; } = null!;
        [StringLength(100)]
        public string Yonetmen { get; set; } = null!;
        [StringLength(50)]
        public string Tur { get; set; } = null!;
        public string? Fotograf { get; set; }
        [NotMapped]
        [DisplayName("Upload Image File")]
        public IFormFile? ImageFile { get; set; }
        public string? Aciklama { get; set; }

        [InverseProperty("Film")]
        public virtual ICollection<Sean> Seans { get; set; }
    }
}
