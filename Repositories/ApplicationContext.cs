using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using XmlParser.Models;
using XmlParser.Settings;

namespace XmlParser.Repositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<LicenseBroadcasting> LicenseBroadcasting { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
