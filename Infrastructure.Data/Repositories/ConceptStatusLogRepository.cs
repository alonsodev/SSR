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



        public List<ConceptStatusLogViewModel> ObtenerCalificaciones(int concept_id)
        {
            var query = Set.Where(a => a.concept_id == concept_id && a.concept_status_id==5).Select(a => new ConceptStatusLogViewModel
            {

                concept_id = a.concept_id,
                concept_status_id = a.concept_status_id,
                qualification = a.qualification.HasValue ? a.qualification.Value : 0
            });

            return query.ToList();
        }
        public Boolean VerificarLeido(int concept_id,int user_id)
        {
            var count = Set.Where(a => a.concept_id == concept_id && a.concept_status_id == 4 && a.user_id_created== user_id).Count();

            return count>0;
        }
    }
}
