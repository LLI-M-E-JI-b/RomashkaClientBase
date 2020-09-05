using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RomashkaClientBase.Model
{
    class ClientBaseContext : DbContext 
    {
        public ClientBaseContext() 
            : base("ClientBaseDbConnection")
        { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
