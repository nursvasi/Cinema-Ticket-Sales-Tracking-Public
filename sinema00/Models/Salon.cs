using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sinema00.Models
{
    [Table("Salon")]
    public partial class Salon
    {
        public Salon()
        {
            Seans = new HashSet<Sean>();
        }

        [Key]
        [Column("SalonID")]
        public int SalonId { get; set; }
        [StringLength(100)]
        public string SalonAdi { get; set; } = null!;
        public int KoltukSayisi { get; set; }

        [InverseProperty("Salon")]
        public virtual ICollection<Sean> Seans { get; set; }
    }
}
