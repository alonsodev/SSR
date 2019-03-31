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
    public class InvestigatorCommissionRepository : Repository<investigators_commissions>
    {

        internal InvestigatorCommissionRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void DeleteMultiple(int investigator_id)
        {
           
            Set.Where(a => a.investigator_id == investigator_id).Delete();
        }

        public void DeleteMultipleByUser(int user_id)
        {

            Set.Where(a => a.investigators.user_id == user_id).Delete();
        }

    }
}
