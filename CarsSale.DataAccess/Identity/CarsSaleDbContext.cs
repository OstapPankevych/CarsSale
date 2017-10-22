using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using CarsSale.DataAccess.Identity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarsSale.DataAccess.Identity
{
    public class ApplicationDbContext: IdentityDbContext<CarsSaleUser, CarsSaleRole, int, CarsSaleLogin, CarsSaleUserRole, CarsSaleClaim>
    {
        public ApplicationDbContext(): base("IdentityCarsSaleEntities") { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarsSaleUser>().ToTable("User");
            modelBuilder.Entity<CarsSaleRole>().ToTable("Role");
            modelBuilder.Entity<CarsSaleClaim>().ToTable("UserClaim");
            modelBuilder.Entity<CarsSaleLogin>().ToTable("UserLogin");
            modelBuilder.Entity<CarsSaleUserRole>().ToTable("UserRole");

            modelBuilder.Entity<CarsSaleUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CarsSaleClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CarsSaleRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CarsSaleUser>().Property(r => r.PasswordHash).HasColumnName("Password");
        }
    }
}