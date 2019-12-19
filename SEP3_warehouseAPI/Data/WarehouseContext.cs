using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SEP3_warehouseAPI.Model;

namespace SEP3_warehouseAPI.Data
{
    public partial class WarehouseContext : DbContext
    {
        public WarehouseContext()
        {
        }

        public WarehouseContext(DbContextOptions<WarehouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WarehouseItem> Stock { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=206.189.31.131;Username=postgres;Password=postgres;Database=warehouse");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarehouseItem>(entity =>
            {
                entity.HasKey(e => e.id)
                    .HasName("stock_pk");

                entity.ToTable("stock", "warehouse");

                entity.HasIndex(e => e.barcode)
                    .HasName("stock_barcode_uindex")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnName("item_id");

                entity.Property(e => e.barcode).HasColumnName("barcode");

                entity.Property(e => e.description)
                    .HasColumnName("item_description")
                    .HasColumnType("character varying");

                entity.Property(e => e.name)
                    .HasColumnName("item_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.amount).HasColumnName("stock");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
