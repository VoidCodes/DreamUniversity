using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SigmaUniversity.Models
{
    public class DataContext: DbContext
    {
        public DataContext(): base("conn") { }
        public DbSet<College> College { get; set; }
        public DbSet<ITAdmin> ITAdmin { get; set; }
    }
}