using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sinema00.Models
{
    [Table("Musteri")]
    public partial class Musteri
    {
        public Musteri()
        {
            Bilets = new HashSet<Bilet>();
        }

        [Key]
        [Column("MusteriID")]
        public int MusteriId { get; set; }
        [StringLength(100)]
        public string Adi { get; set; } = null!;
        [StringLength(100)]
        public string Soyadi { get; set; } = null!;
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [StringLength(20)]
        public string Telefon { get; set; } = null!;

        [InverseProperty("Musteri")]
        public virtual ICollection<Bilet> Bilets { get; set; }
    }
}
