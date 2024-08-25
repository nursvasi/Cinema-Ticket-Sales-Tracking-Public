using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sinema00.Models
{
    [Table("Odeme")]
    public partial class Odeme
    {
        [Key]
        [Column("OdemeID")]
        public int OdemeId { get; set; }
        [Column("BiletID")]
        public int? BiletId { get; set; }
        [StringLength(50)]
        public string OdemeTuru { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal OdemeTutari { get; set; }

        [ForeignKey("BiletId")]
        [InverseProperty("Odemes")]
        public virtual Bilet? Bilet { get; set; }
    }
}
