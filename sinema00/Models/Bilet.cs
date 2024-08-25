using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sinema00.Models
{
    [Table("Bilet")]
    public partial class Bilet
    {
        public Bilet()
        {
            Odemes = new HashSet<Odeme>();
        }

        [Key]
        [Column("BiletID")]
        public int BiletId { get; set; }
        [Column("SeansID")]
        public int? SeansId { get; set; }
        [Column("MusteriID")]
        public int? MusteriId { get; set; }
        [Column(TypeName = "money")]
        public decimal Fiyat { get; set; }

        [ForeignKey("MusteriId")]
        [InverseProperty("Bilets")]
        public virtual Musteri? Musteri { get; set; }
        [ForeignKey("SeansId")]
        [InverseProperty("Bilets")]
        public virtual Sean? Seans { get; set; }
        [InverseProperty("Bilet")]
        public virtual ICollection<Odeme> Odemes { get; set; }
    }
}
