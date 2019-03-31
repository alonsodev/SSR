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
    public class OriginRepository : Repository<origins>
    {

        internal OriginRepository(ApplicationDbContext context)
            : base(context)
        {
        }


        public List<OriginViewModel> ObtenerTodos()
        {
            var query = Set.Select(a => new OriginViewModel
            {

                origin_id = a.origin_id,
                name = a.name
            });

            return query.ToList();
        }

    }
}
