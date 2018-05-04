using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RRInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace RRInventory.Data
{
    public class InventoryContext : DbContext
    {
            public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
            {
            }

            public DbSet<Account> Accounts { get; set; }
            public DbSet<Item> Items { get; set; }
            public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Image>().ToTable("Image");
        }
    }
}
