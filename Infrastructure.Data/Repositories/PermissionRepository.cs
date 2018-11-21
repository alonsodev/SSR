using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PermissionRepository : Repository<role_permissions>
    {
        internal PermissionRepository(ApplicationDbContext context)
            : base(context)
        {
        }

    }
}
