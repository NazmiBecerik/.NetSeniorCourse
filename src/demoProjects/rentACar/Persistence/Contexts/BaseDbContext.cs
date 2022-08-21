using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Brand> Brands { get; set; }
      
        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        // Model oluşturulduğunda
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Brand classı veritabanında hangi tabloya karşılık gelmeli
            modelBuilder.Entity<Brand>(a =>
            {
                // Brands tablosuna karşılık gelmeli.
                a.ToTable("Brands").HasKey(k => k.Id);
                // Property olarak Id nin veritabanında kolon adı Id dir.
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });


            // Seed data ?
            // Seed data, biz bu tabloyu migration ettiğimizde bize test datası yaratmasını istersek kullanabileceğimiz yapıdır.
            Brand[] brandEntitySeeds = { new(1, "Bmw"), new(2, "Mercedes") };
            modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);

           
        }
    }
}
