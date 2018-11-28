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
    public class InstitutionRepository : Repository<institutions>
    {
        internal InstitutionRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public bool VerificarDuplicado(int institution_id, string name)
        {

            name = name.Trim().ToLower();
            var count = Set.Where(a => a.institution_id != institution_id && a.name.ToLower() == name).Count();

            return count == 0;
        }

        public InstitutionViewModel Obtener(int institution_id)
        {
            var query = Set.Where(a => a.institution_id == institution_id).Select(a => new InstitutionViewModel
            {

                institution_id = a.institution_id,
                name = a.name                
            });

            return query.Take(1).FirstOrDefault();
        }

       

        public GridModel<InstitutionViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            var searchBy = (filters.search != null) ? filters.search.value : null;


            string sortBy = "";
            string sortDir = "";

            if (filters.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = filters.columns[filters.order[0].column].data;
                sortDir = filters.order[0].dir.ToLower();
            }


            GridModel<InstitutionViewModel> resultado = new GridModel<InstitutionViewModel>();
            IQueryable<institutions> queryFilters = Set;



            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.name.ToLower().Contains(srch)) );


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new InstitutionViewModel
            {
                institution_id = a.institution_id,
                name = a.name
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "institution_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<InstitutionViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

    }
}
