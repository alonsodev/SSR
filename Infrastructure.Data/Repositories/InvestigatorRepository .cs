using Domain.Entities;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class InvestigatorRepository : Repository<investigators>
    {
        internal InvestigatorRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public InvestigatorViewModel Obtener(int investigator_id)
        {
            var query = Set.Where(a => a.investigator_id == investigator_id).Select(a => new InvestigatorViewModel
            {

                investigator_id = a.investigator_id,
                user_name = a.users.user_name,
                user_email = a.users.user_email,
                user_pass = a.users.user_pass,
                // user_role_id = a.users.user_role_id,
                // user_status_id = a.users.user_status_id,
                is_super_admin = a.users.is_super_admin,
                user_date_last_login = a.users.user_date_last_login,
                date_created = a.users.date_created,
                date_modified = a.users.date_modified,
                user_id_created = a.users.user_id_created,
                user_id_modified = a.users.user_id_modified,
                document_type_id = a.users.document_type_id,

                doc_nro = a.users.doc_nro,
                nationality_id = a.users.nationality_id,
                contact_name = a.users.contact_name,
                phone = a.users.phone,
                address = a.users.address,
                commissions = a.commissions.Select(c => c.commission_id).ToList(),
                interest_areas = a.interest_areas.Select(ia => ia.interest_area_id).ToList(),
            });

            return query.Take(1).FirstOrDefault();
        }
    }
}
