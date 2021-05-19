using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Diplom
{
    public partial class DiplomContext : DbContext
    {
        public DiplomContext()
        {
        }

        public DiplomContext(DbContextOptions<DiplomContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemItem> ItemItems { get; set; }
        public virtual DbSet<ItemResource> ItemResources { get; set; }
        public virtual DbSet<Measure> Measures { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Workshop> Workshops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=diplom1;Username=postgres;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.HasIndex(e => e.Id, "item_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("title");
            });

            modelBuilder.Entity<ItemItem>(entity =>
            {
                entity.ToTable("item_item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChildItemId).HasColumnName("child_item_id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.ParentItemId).HasColumnName("parent_item_id");

                entity.HasOne(d => d.ChildItem)
                    .WithMany(p => p.ItemItemChildItems)
                    .HasForeignKey(d => d.ChildItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("item_item_item_id_fk_2");

                entity.HasOne(d => d.ParentItem)
                    .WithMany(p => p.ItemItemParentItems)
                    .HasForeignKey(d => d.ParentItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("item_item_item_id_fk");
            });

            modelBuilder.Entity<ItemResource>(entity =>
            {
                entity.ToTable("item_resource");

                entity.HasIndex(e => e.Id, "partition_resource_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.ResourceId).HasColumnName("resource_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemResources)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("item_resource_item_id_fk");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ItemResources)
                    .HasForeignKey(d => d.ResourceId)
                    .HasConstraintName("partition_resource_resource_id_fk");
            });

            modelBuilder.Entity<Measure>(entity =>
            {
                entity.ToTable("measure");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("resource");

                entity.HasIndex(e => e.Id, "resource_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountOnStore)
                    .HasColumnName("count_on_store")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.MeasureId).HasColumnName("measureId");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("title");

                entity.Property(e => e.WorkshopId).HasColumnName("workshopId");

                entity.HasOne(d => d.Measure)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.MeasureId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("resource_measure_id_fk");

                entity.HasOne(d => d.Workshop)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.WorkshopId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("resource_workshop_id_fk");
            });

            modelBuilder.Entity<Workshop>(entity =>
            {
                entity.ToTable("workshop");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title).HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
