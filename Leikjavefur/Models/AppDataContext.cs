using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Leikjavefur.Models
{
    public class AppDataContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}