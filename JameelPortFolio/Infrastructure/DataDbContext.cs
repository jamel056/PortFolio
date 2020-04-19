using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PortFolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Owner>().HasData(
                new Owner()
                {
                    Id = Guid.NewGuid(),
                    Avatar = "avatar.jpg",
                    Portfil = "Microsoft MVP / .NET Consultant"
                }
                );
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<PortFolioItem> PortFolioItems { get; set; }
    }
}
