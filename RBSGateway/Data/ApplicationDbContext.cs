﻿using Microsoft.EntityFrameworkCore;
using RBSGateway.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RBSGateway.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :DbContext(options)
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceName> ResourceNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Resource>(entity =>
            {
               entity.HasKey(r => new { r.ResourceID,r.CompanyID });
                entity.HasOne(r => r.ResourceName)
                      .WithMany()
                      .HasForeignKey(r => r.ResourceNameId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Parent)
                      .WithMany(r => r.Items)
                      .HasForeignKey(r => new { r.ParentID, r.CompanyID})
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
