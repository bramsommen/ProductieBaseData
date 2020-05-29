using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ModelsBaseData;

namespace DAL
{
    public partial class ProductieBaseDataContext : DbContext
    {
        public ProductieBaseDataContext()
        {
        }

        public ProductieBaseDataContext(DbContextOptions<ProductieBaseDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cyclus> Cyclus { get; set; }
        public virtual DbSet<CyclusMaakInstelling> CyclusMaakInstelling { get; set; }
        public virtual DbSet<CyclusType> CyclusType { get; set; }
        public virtual DbSet<Eigenschap> Eigenschap { get; set; }
        public virtual DbSet<HmiMgmtExchange> HmiMgmtExchange { get; set; }
        public virtual DbSet<MaakInstelling> MaakInstelling { get; set; }
        public virtual DbSet<MachineOnderdeel> MachineOnderdeel { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductEigenschap> ProductEigenschap { get; set; }
        public virtual DbSet<ProductVersie> ProductVersie { get; set; }
        public virtual DbSet<ProductVersieCyclus> ProductVersieCyclus { get; set; }

        public virtual DbSet<GlobalProduct> GlobalProduct { get; set; }
        public virtual DbSet<GlobalProductEigenschap> GlobalProductEigenschap { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SIRIUSDB;Database= ProductieBaseData;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cyclus>(entity =>
            {
                entity.Property(e => e.CyclusTypeId).HasColumnName("CyclusTypeID");

                entity.Property(e => e.MachineOnderdeelId).HasColumnName("MachineOnderdeelID");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CyclusType)
                    .WithMany(p => p.Cyclus)
                    .HasForeignKey(d => d.CyclusTypeId)
                    .HasConstraintName("FK_Cyclus_CyclusType");

                entity.HasOne(d => d.MachineOnderdeel)
                    .WithMany(p => p.Cyclus)
                    .HasForeignKey(d => d.MachineOnderdeelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cyclus_MachineOnderdeel");
            });

            modelBuilder.Entity<CyclusMaakInstelling>(entity =>
            {
                entity.Property(e => e.CyclusId).HasColumnName("CyclusID");

                entity.Property(e => e.MaakInstellingId).HasColumnName("MaakInstellingID");

                entity.Property(e => e.ProductEigenschapId).HasColumnName("ProductEigenschapID");

                entity.Property(e => e.StaticWaarde)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Cyclus)
                    .WithMany(p => p.CyclusMaakInstelling)
                    .HasForeignKey(d => d.CyclusId)
                    .HasConstraintName("FK_CyclusMaakInstellingen_Cyclus");

                entity.HasOne(d => d.MaakInstelling)
                    .WithMany(p => p.CyclusMaakInstelling)
                    .HasForeignKey(d => d.MaakInstellingId)
                    .HasConstraintName("FK_CyclusMaakInstellingen_MaakInstellingen");

                entity.HasOne(d => d.ProductEigenschap)
                    .WithMany(p => p.CyclusMaakInstelling)
                    .HasForeignKey(d => d.ProductEigenschapId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CyclusMaakInstellingen_Eigenschappen");
            });

            modelBuilder.Entity<CyclusType>(entity =>
            {
                entity.Property(e => e.MachineOnderdeelId).HasColumnName("MachineOnderdeelID");

                entity.Property(e => e.Naam).IsRequired();

                entity.HasOne(d => d.MachineOnderdeel)
                    .WithMany(p => p.CyclusType)
                    .HasForeignKey(d => d.MachineOnderdeelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CyclusType_MachineOnderdeel");
            });

            modelBuilder.Entity<Eigenschap>(entity =>
            {
                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineOnderdeelId).HasColumnName("MachineOnderdeelID");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.MachineOnderdeel)
                    .WithMany(p => p.Eigenschap)
                    .HasForeignKey(d => d.MachineOnderdeelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Eigenschap_MachineOnderdeel");
            });

            modelBuilder.Entity<MaakInstelling>(entity =>
            {
                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineOnderdeelId).HasColumnName("MachineOnderdeelID");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.MachineOnderdeel)
                    .WithMany(p => p.MaakInstelling)
                    .HasForeignKey(d => d.MachineOnderdeelId)
                    .HasConstraintName("FK_MaakInstelling_MachineOnderdeel");
            });

            modelBuilder.Entity<MachineOnderdeel>(entity =>
            {
                entity.Property(e => e.Machine)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ArtikelCode)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineOnderdeelId).HasColumnName("MachineOnderdeelID");

                entity.HasOne(d => d.MachineOnderdeel)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.MachineOnderdeelId)
                    .HasConstraintName("FK_Product_MachineOnderdeel");
            });

            modelBuilder.Entity<ProductEigenschap>(entity =>
            {
                entity.Property(e => e.EigenschapId).HasColumnName("EigenschapID");

                entity.Property(e => e.Waarde)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Eigenschap)
                    .WithMany(p => p.ProductEigenschap)
                    .HasForeignKey(d => d.EigenschapId)
                    .HasConstraintName("FK_ProductEigenschappen_Eigenschappen");

                entity.HasOne(d => d.ProductVersie)
                    .WithMany(p => p.ProductEigenschap)
                    .HasForeignKey(d => d.ProductVersieId)
                    .HasConstraintName("FK_ProductEigenschappen_ProductVers");
            });

            modelBuilder.Entity<ProductVersie>(entity =>
            {
                entity.Property(e => e.Cad2d).HasColumnName("CAD2D");

                entity.Property(e => e.Cad3d).HasColumnName("CAD3D");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pdf).HasColumnName("PDF");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductVersie)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductVers_ProductBase");
            });

            modelBuilder.Entity<ProductVersieCyclus>(entity =>
            {
                entity.Property(e => e.CyclusId).HasColumnName("CyclusID");

                entity.HasOne(d => d.Cyclus)
                    .WithMany(p => p.ProductCyclus)
                    .HasForeignKey(d => d.CyclusId)
                    .HasConstraintName("FK_ProductCyclusMaakInstelling_Cyclus");

                entity.HasOne(d => d.ProductVersie)
                    .WithMany(p => p.ProductVersieCyclus)
                    .HasForeignKey(d => d.ProductVersieId)
                    .HasConstraintName("FK_ProductCyclusMaakInstelling_ProductVers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
