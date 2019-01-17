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
    public class DebateSpeakerRepository : Repository<debate_speakers>
    {

        internal DebateSpeakerRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void DeleteMultiple(int draft_law_id)
        {
            Set.Where(a => a.draft_law_id == draft_law_id).Delete();
        }
    }
}
