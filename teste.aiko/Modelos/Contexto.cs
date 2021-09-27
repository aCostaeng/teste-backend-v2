using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace teste.aiko.Modelos
{
    public partial class Contexto : DbContext
    {
        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentModel> EquipmentModels { get; set; }
        public virtual DbSet<EquipmentModelStateHourlyEarning> EquipmentModelStateHourlyEarnings { get; set; }
        public virtual DbSet<EquipmentPositionHistory> EquipmentPositionHistories { get; set; }
        public virtual DbSet<EquipmentState> EquipmentStates { get; set; }
        public virtual DbSet<EquipmentStateHistory> EquipmentStateHistories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:Collation", "Portuguese_Angola.1252");

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("equipment", "operation");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EquipmentModelId).HasColumnName("equipment_model_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.EquipmentModel)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.EquipmentModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_equipment_model");
            });

            modelBuilder.Entity<EquipmentModel>(entity =>
            {
                entity.ToTable("equipment_model", "operation");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<EquipmentModelStateHourlyEarning>(entity =>
            {
                entity.ToTable("equipment_model_state_hourly_earnings", "operation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v1()");

                entity.Property(e => e.EquipmentModelId).HasColumnName("equipment_model_id");

                entity.Property(e => e.EquipmentStateId).HasColumnName("equipment_state_id");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.EquipmentModel)
                    .WithMany(p => p.EquipmentModelStateHourlyEarnings)
                    .HasForeignKey(d => d.EquipmentModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_equipment_model");

                entity.HasOne(d => d.EquipmentState)
                    .WithMany(p => p.EquipmentModelStateHourlyEarnings)
                    .HasForeignKey(d => d.EquipmentStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_equipment_state");
            });

            modelBuilder.Entity<EquipmentPositionHistory>(entity =>
            {

                entity.ToTable("equipment_position_history", "operation");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lon).HasColumnName("lon");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v1()");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentPositionHistories)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_equipment");
            });

            modelBuilder.Entity<EquipmentState>(entity =>
            {
                entity.ToTable("equipment_state", "operation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v1()");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnName("color");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<EquipmentStateHistory>(entity =>
            {
                entity.ToTable("equipment_state_history", "operation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v1()");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.EquipmentStateId).HasColumnName("equipment_state_id");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentStateHistories)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_equipment");

                entity.HasOne(d => d.EquipmentState)
                    .WithMany(p => p.EquipmentStateHistories)
                    .HasForeignKey(d => d.EquipmentStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_equipment_state");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
