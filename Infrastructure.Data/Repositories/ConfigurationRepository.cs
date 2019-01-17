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
    public class ConfigurationRepository : Repository<configurations>
    {

        internal ConfigurationRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public ConfigurationViewModel Obtener()
        {
            var query = Set.Select(a => new ConfigurationViewModel
            {

                configuration_id = a.configuration_id,
                terms_conditions=a.terms_conditions,
                remove_titles_speaker=a.remove_titles_speaker,
                exclude_speakers=a.exclude_speakers
            });

            return query.Take(1).FirstOrDefault();
        }

    }
}
