using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class RoleRepository : Repository<roles>
    {
        internal RoleRepository(ApplicationDbContext context)
            : base(context)
        {
        }

    }
}
