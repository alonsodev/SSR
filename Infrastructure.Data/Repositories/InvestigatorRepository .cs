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
                first_name=a.first_name,
                second_name=a.second_name,
                last_name=a.last_name,
                second_last_name= a.second_last_name,
                gender_id=a.gender_id,
                phone = a.users.phone,
                mobile_phone=a.mobile_phone,
                birthdate=a.birthdate,
                nationality_id = a.users.nationality_id,
                address_country_id=a.users.address_country_id,
                department_id=a.users.municipalities.department_id,
                address_municipality_id=a.users.address_municipality_id,
                educational_institution_id= a.educational_institution_id,
                program_id=a.program_id,
                education_level_id=a.education_level_id,
                CVLAC=a.CVLAC,

                institution_id=a.institution_id,
                institution=a.institutions.name,
                investigation_group_id=a.investigation_group_id,
                code_investigation_group=a.investigation_groups.code,

                commissions = a.investigators_commissions.Select(c => c.commission_id).ToList(),
                interest_areas = a.investigators_interest_areas.Select(ia => ia.interest_area_id).ToList(),

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
              
                contact_name = a.users.contact_name,
                
                address = a.users.address,
                avatar=a.users.avatar
              
            });

            return query.Take(1).FirstOrDefault();
        }
    }
}
