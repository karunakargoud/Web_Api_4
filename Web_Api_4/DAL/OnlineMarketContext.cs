using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web_Api_4.Models;

namespace Web_Api_4.DAL
{
    public class OnlineMarketContext:DbContext
    {
        public OnlineMarketContext()
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Product");
        }
        public DbSet<Product> Products { get; set; }
    }
}