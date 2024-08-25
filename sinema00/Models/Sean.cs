using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sinema00.Models
{
    public partial class Sean
    {
        public Sean()
        {
            Bilets = new HashSet<Bilet>();
        }

        [Key]
        [Column("SeansID")]
        public int SeansId { get; set; }
        [Column("FilmID")]
        public int? FilmId { get; set; }
        [Column("SalonID")]
        public int? SalonId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SeansSaati { get; set; }

        [ForeignKey("FilmId")]
        [InverseProperty("Seans")]
        public virtual Film? Film { get; set; }
        [ForeignKey("SalonId")]
        [InverseProperty("Seans")]
        public virtual Salon? Salon { get; set; }
        [InverseProperty("Seans")]
        public virtual ICollection<Bilet> Bilets { get; set; }
    }
}
