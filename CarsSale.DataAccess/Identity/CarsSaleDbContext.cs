﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using CarsSale.DataAccess.Identity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarsSale.DataAccess.Identity
{
    public class CarsSaleDbContext: IdentityDbContext<CarsSaleUser, CarsSaleRole, int, CarsSaleLogin, CarsSaleUserRole, CarsSaleClaim>
    { 
        public CarsSaleDbContext()
        {
        }

        public CarsSaleDbContext(string connectionString)
            : base(connectionString)
        { }

        public static CarsSaleDbContext Create(string connectionString)
        {
            return new CarsSaleDbContext(connectionString);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarsSaleUser>().ToTable("User")
                .Property(r => r.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CarsSaleUser>().Property(r => r.PasswordHash).HasColumnName("Password");

            modelBuilder.Entity<CarsSaleRole>().ToTable("Role")
                .Property(r => r.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CarsSaleClaim>().ToTable("UserClaim")
                .Property(r => r.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CarsSaleLogin>().ToTable("UserLogin");
            modelBuilder.Entity<CarsSaleUserRole>().ToTable("UserRole");
        }
    }
}