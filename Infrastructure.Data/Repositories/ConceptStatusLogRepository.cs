using Domain.Entities;
using EntityFramework.Extensions;
using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ConceptStatusLogRepository : Repository<concepts_status_logs>
    {
        internal ConceptStatusLogRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        
    }
   
}
