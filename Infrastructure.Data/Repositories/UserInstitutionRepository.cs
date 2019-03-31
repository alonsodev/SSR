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
    public class UserInstitutionRepository : Repository<user_institutions>
    {
        internal UserInstitutionRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void DeleleMultiple(int user_id)
        {
            Set.Where(a => a.user_id == user_id).Delete();
        }
    }
   
}
