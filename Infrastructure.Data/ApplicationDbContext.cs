using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    internal class ApplicationDbContext : DbContext
    {
        internal ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Database.CommandTimeout = 60 * 60;
        }
    }
}
