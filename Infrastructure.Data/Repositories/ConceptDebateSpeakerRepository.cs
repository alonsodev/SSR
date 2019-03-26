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
    public class ConceptDebateSpeakerRepository : Repository<concept_debate_speakers>
    {
        internal ConceptDebateSpeakerRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void DeleleMultiple(int concept_id)
        {
            Set.Where(a => a.concept_id == concept_id).Delete();
        }
    }
   
}
