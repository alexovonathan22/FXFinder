using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FXFinder.Core.DBModels;

namespace FXFinder.Core.DataAccess
{
    public class WalletDbContext : DbContext 
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options)
     : base(options)
        { }
        public DbSet<FXUser> Users { get; set; }
        public DbSet<Wallet> WalletAccts { get; set; }
      
        // Todo : Seed DB with admin user credentials
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Seed();
        }
    }
}
