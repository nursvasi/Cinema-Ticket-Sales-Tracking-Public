using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace sinema00.Models
{
    public partial class sinema00Context : DbContext
    {
        public sinema00Context()
        {
        }

        public sinema00Context(DbContextOptions<sinema00Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Bilet> Bilets { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Musteri> Musteris { get; set; } = null!;
        public virtual DbSet<Odeme> Odemes { get; set; } = null!;
        public virtual DbSet<Salon> Salons { get; set; } = null!;
        public virtual DbSet<Sean> Seans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UE43HMH;initial Catalog=sinema00;trusted_connection=yes ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bilet>(entity =>
            {
                entity.Property(e => e.BiletId).ValueGeneratedNever();

                entity.HasOne(d => d.Musteri)
                    .WithMany(p => p.Bilets)
                    .HasForeignKey(d => d.MusteriId)
                    .HasConstraintName("FK__Bilet__MusteriID__2F10007B");

                entity.HasOne(d => d.Seans)
                    .WithMany(p => p.Bilets)
                    .HasForeignKey(d => d.SeansId)
                    .HasConstraintName("FK__Bilet__SeansID__2E1BDC42");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.FilmId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Musteri>(entity =>
            {
                entity.Property(e => e.MusteriId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Odeme>(entity =>
            {
                entity.Property(e => e.OdemeId).ValueGeneratedNever();

                entity.HasOne(d => d.Bilet)
                    .WithMany(p => p.Odemes)
                    .HasForeignKey(d => d.BiletId)
                    .HasConstraintName("FK__Odeme__BiletID__31EC6D26");
            });

            modelBuilder.Entity<Salon>(entity =>
            {
                entity.Property(e => e.SalonId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Sean>(entity =>
            {
                entity.HasKey(e => e.SeansId)
                    .HasName("PK__Seans__0359BD6039D860CE");

                entity.Property(e => e.SeansId).ValueGeneratedNever();

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Seans)
                    .HasForeignKey(d => d.FilmId)
                    .HasConstraintName("FK__Seans__FilmID__286302EC");

                entity.HasOne(d => d.Salon)
                    .WithMany(p => p.Seans)
                    .HasForeignKey(d => d.SalonId)
                    .HasConstraintName("FK__Seans__SalonID__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
