﻿using Domain.Entities;
using EntityFramework.Extensions;
using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class InvestigatorInterestAreaRepository : Repository<investigators_interest_areas>
    {

        internal InvestigatorInterestAreaRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void deleteMultiple(int investigator_id)
        {
           
            Set.Where(a => a.investigator_id == investigator_id).Delete();
        }

    }
}
