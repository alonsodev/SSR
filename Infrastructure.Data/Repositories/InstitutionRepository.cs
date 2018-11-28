using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class InstitutionRepository : Repository<institutions>
    {
        internal InstitutionRepository(ApplicationDbContext context)
            : base(context)
        {
        }
       
    }
}
