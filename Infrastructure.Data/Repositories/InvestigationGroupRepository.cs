using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class InvestigationGroupRepository : Repository<investigation_groups>
    {
        internal InvestigationGroupRepository(ApplicationDbContext context)
            : base(context)
        {
        }
       
    }
}
