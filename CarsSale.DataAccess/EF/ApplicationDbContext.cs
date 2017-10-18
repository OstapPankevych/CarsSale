using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using CarsSale.DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarsSale.DataAccess.EF
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationLogin, ApplicationUserRole, ApplicationClaim>
    {
        public ApplicationDbContext(): base("IdentityCarsSaleEntities") { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<ApplicationRole>().ToTable("Role");
            modelBuilder.Entity<ApplicationClaim>().ToTable("UserClaim");
            modelBuilder.Entity<ApplicationLogin>().ToTable("UserLogin");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRole");

            modelBuilder.Entity<ApplicationUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ApplicationUser>().Property(r => r.PasswordHash).HasColumnName("Password");
        }
    }
}