using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using System.IO;

namespace Lab2.Models
{
    public partial class PublicUtilitiesContext : DbContext
    {
        public PublicUtilitiesContext()
        {
        }

        public PublicUtilitiesContext(DbContextOptions<PublicUtilitiesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Tenants> Tenants { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("apartment");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('apartment_id_apartment_seq'::regclass)");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.TenantsCount).HasColumnName("tenants_count");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\" payment_id_payment_seq\"'::regclass)");

                entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("id_apartment");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("id_service");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("payment_method");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('payment_method_id_method_seq'::regclass)");

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasColumnName("method");
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.ToTable("services");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('services_id_service_seq'::regclass)");

                entity.Property(e => e.MethodId).HasColumnName("method_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(5,2)");

                entity.Property(e => e.Service).HasColumnName("service");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.MethodId)
                    .HasConstraintName("id_method");
            });

            modelBuilder.Entity<Tenants>(entity =>
            {
                entity.ToTable("tenants");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('tenants_id_tenant_seq'::regclass)");

                entity.Property(e => e.AccountNumber).HasColumnName("account_number");

                entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");

                entity.Property(e => e.FatherName)
                    .HasColumnName("father_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Tenants)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("id_apartment");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
