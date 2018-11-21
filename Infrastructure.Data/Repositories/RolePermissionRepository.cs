using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class RolePermissionRepository : Repository<role_permissions>
    {
        internal RolePermissionRepository(ApplicationDbContext context)
            : base(context)
        {
        }

    }
}
