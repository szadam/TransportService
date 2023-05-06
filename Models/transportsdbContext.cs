using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TransportService.Models
{
    public partial class transportsdbContext : DbContext
    {
        public transportsdbContext()
        {
        }

        public transportsdbContext(DbContextOptions<transportsdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Place> Places { get; set; } = null!;
        public virtual DbSet<Transport> Transports { get; set; } = null!;
        public virtual DbSet<Transportevent> Transportevents { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=postgres;Database=transportsdb;Username=user;Password=example");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("places");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country");
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.ToTable("transport");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DestinationPlacesId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("destination_places_id");

                entity.Property(e => e.Places).HasColumnName("places");

                entity.Property(e => e.SourcePlacesId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("source_places_id");

                entity.Property(e => e.Transportdate).HasColumnName("transportdate");

                entity.Property(e => e.Transporttype)
                    .HasMaxLength(255)
                    .HasColumnName("transporttype");

                entity.HasOne(d => d.DestinationPlaces)
                    .WithMany(p => p.TransportDestinationPlaces)
                    .HasForeignKey(d => d.DestinationPlacesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("destination_places_id");

                entity.HasOne(d => d.SourcePlaces)
                    .WithMany(p => p.TransportSourcePlaces)
                    .HasForeignKey(d => d.SourcePlacesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("source_places_id");
            });

            modelBuilder.Entity<Transportevent>(entity =>
            {
                entity.ToTable("transportevent");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Places).HasColumnName("places");
                
                entity.Property(e => e.TransportId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("transport_id");
                
                entity.Property(e => e.EventID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("event_id");
                
                entity.Property(e => e.Creationtime)
                    .HasColumnName("creationtime")
                    .HasDefaultValueSql("now()");
                
                entity.Property(e => e.Type)
                    .HasMaxLength(16)
                    .HasColumnName("type");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.Transportevents)
                    .HasForeignKey(d => d.TransportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transport_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
