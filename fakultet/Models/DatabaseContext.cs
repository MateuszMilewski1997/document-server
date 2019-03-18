using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fakultet.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
