﻿using desafio_pontotel_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_pontotel_backend.DAO
{
    public partial class DAOContext : DbContext
    {
        public DAOContext()
        {
        }

        public DAOContext(DbContextOptions<DAOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User ID=dtxhrirjjhhoaz;Password=8123f444992df73892db2e939cf4c8347acd70dc216a723d4c23857b408a10af;Host=ec2-34-226-18-183.compute-1.amazonaws.com;Port=5432;Database=da28e9qpcmjlhr;Pooling=true;SSL Mode=Prefer;TrustServerCertificate=True;");
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging();
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "pt_BR.UTF-8");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("cep");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.Complement)
                    .HasMaxLength(30)
                    .HasColumnName("complement");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.CpfPis)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("cpf_pis");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .HasColumnName("email");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("number");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("state");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("district");

                entity.Property(e => e.IsPis)
                    .HasMaxLength(1)
                    .HasColumnName("isPis");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("street");

                entity.Property(e => e.BirthDate)
                    .IsRequired()
                    .HasColumnName("birth_date");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
