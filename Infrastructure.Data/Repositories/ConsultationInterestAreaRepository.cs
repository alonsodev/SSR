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
    public class ConsultationInterestAreaRepository : Repository<consultations_interest_areas>
    {

        internal ConsultationInterestAreaRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void DeleteByConsultation(int consultation_id)
        {
            Set.Where(a => a.consultation_id == consultation_id).Delete();
        }
    }
}
