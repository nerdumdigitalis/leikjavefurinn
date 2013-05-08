using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leikjavefur.Entities;
using System.Data.Entity;

namespace Leikjavefur.Contexts
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}