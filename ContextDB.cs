  using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace Schedule_
{
    public class ContextDB : DbContext
    {

        public ContextDB() : base("DbConnectionString")
        {
            this.Database.CreateIfNotExists();
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Description> Descriptions { get; set; }
    }
}
