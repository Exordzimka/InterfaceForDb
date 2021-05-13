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
        public virtual DbSet<ItemPartition> ItemPartitions { get; set; }
        public virtual DbSet<ItemResource> ItemResources { get; set; }
        public virtual DbSet<Partition> Partitions { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("host=localhost;database=diplom;username=postgres;password=123");
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

            modelBuilder.Entity<ItemPartition>(entity =>
            {
                entity.ToTable("item_partition");

                entity.HasIndex(e => e.Id, "item_partition_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.PartitionId).HasColumnName("partition_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemPartitions)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("item_partition_item_id_fk");

                entity.HasOne(d => d.Partition)
                    .WithMany(p => p.ItemPartitions)
                    .HasForeignKey(d => d.PartitionId)
                    .HasConstraintName("item_partition_partition_id_fk");
            });

            modelBuilder.Entity<ItemResource>(entity =>
            {
                entity.ToTable("item_resource");

                entity.HasIndex(e => e.Id, "partition_resource_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('partition_resource_id_seq'::regclass)");

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

            modelBuilder.Entity<Partition>(entity =>
            {
                entity.ToTable("partition");

                entity.HasIndex(e => e.Id, "partition_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

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

                entity.Property(e => e.PartitionId).HasColumnName("partition_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("title");

                entity.HasOne(d => d.Partition)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.PartitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("resource_partition_id_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
