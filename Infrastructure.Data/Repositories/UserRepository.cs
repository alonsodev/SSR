﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<users>
    {
        internal UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }


    }
}
