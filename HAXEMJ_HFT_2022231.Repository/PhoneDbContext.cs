using System;
using Microsoft.EntityFrameworkCore;
using HAXEMJ_HFT_2022231.Models;

namespace HAXEMJ_HFT_2022231.Repository
{
    public class PhoneDbContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<User> User { get; set; }

        public PhoneDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("phonedb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Phones)
                .HasForeignKey(p => p.ManufacturerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Phone>()
                .HasOne(u => u.User)
                .WithMany(p => p.OwnedPhones)
                .HasForeignKey(u => u.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Phone>().HasData(new Phone[]
            {
                new Phone("1#iPhone 14#Purple#APL113#12#5.5"),
                new Phone("2#iPhone 12#Black#APL113#11#3.4"),
                new Phone("3#iPhone 12#Black#APL113#11#2.1"),
                new Phone("4#Galaxy S20#Black#SMG140#13#4")
            });

            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User("11#John Smith"),
                new User("12#Tom Whatts"),
                new User("13#Steve Johnson")
            });

            modelBuilder.Entity<Manufacturer>().HasData(new Manufacturer[]
            {
                new Manufacturer("APL113#Apple#USA"),
                new Manufacturer("SMG140#Samsung#Korea"),
            });
        }
    }
}

