using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcWorking.Models
{
    public class APIContext : DbContext
    {
        public APIContext()
            : base("name=DefaultConnection")
        {
        }           
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }
        
    }
}