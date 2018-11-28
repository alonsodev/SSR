using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProgramRepository : Repository<programs>
    {
        internal ProgramRepository(ApplicationDbContext context)
            : base(context)
        {
        }
       
    }
}
