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
    public class DraftLawStatusRepository : Repository<draft_laws_status>
    {

        internal DraftLawStatusRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public List<DraftLawStatusViewModel> ObtenerTodos()
        {
            var lista = this.Context.Set<draft_laws_status>();
            var query = lista.Select(a => new DraftLawStatusViewModel
            {

                draft_law_status_id = a.draft_law_status_id,
                notifiable = a.notifiable,
                name = a.name.Trim()
            });

            return query.ToList();
        }


    }
}
