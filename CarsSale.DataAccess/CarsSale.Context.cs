﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarsSale.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CarsSaleEntities : DbContext
    {
        public CarsSaleEntities(string connectionString)
            : base(connectionString)
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADVERTISEMENT> ADVERTISEMENTs { get; set; }
        public virtual DbSet<BRAND> BRANDs { get; set; }
        public virtual DbSet<ENGINE> ENGINEs { get; set; }
        public virtual DbSet<ENGINE_FUEL> ENGINE_FUEL { get; set; }
        public virtual DbSet<FUEL> FUELs { get; set; }
        public virtual DbSet<REGION> REGIONs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TRANSMISSION_TYPE> TRANSMISSION_TYPE { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<VEHICL> VEHICLs { get; set; }
        public virtual DbSet<VEHICL_TYPE> VEHICL_TYPE { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
    }
}
