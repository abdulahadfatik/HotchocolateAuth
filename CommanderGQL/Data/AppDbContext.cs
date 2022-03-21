using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CommanderGQL.Models;


namespace CommanderGQL.Data
{
    public class AppDbContext : DbContext
    {
        public  AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TokenSetting> TokenSettings { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder                                            
                .Entity<Platform>()
                .HasMany(p => p.Commands)
                .WithOne(p => p.Platform!)
                .HasForeignKey(p => p.PlatformId);

            modelBuilder
                .Entity<Command>()
                .HasOne(p => p.Platform)
                .WithMany(p => p.Commands)
                .HasForeignKey(p => p.PlatformId);
        }
    }
}
