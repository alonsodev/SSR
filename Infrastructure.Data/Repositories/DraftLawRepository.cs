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
    public class DraftLawRepository : Repository<draft_laws>
    {
        internal DraftLawRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public bool VerificarDuplicado(int draft_law_id, int draft_law_number)
        {


            var count = Set.Where(a => a.draft_law_id != draft_law_id && a.draft_law_number == draft_law_number).Count();

            return count == 0;
        }
        public DraftLawViewModel ObtenerPorNroProyectoMigrar(int draft_law_number)
        {
            var query = Set.Where(a => a.draft_law_number == draft_law_number).Select(a => new DraftLawViewModel
            {

                draft_law_id = a.draft_law_id,
                draft_law_number = a.draft_law_number,

                date_created = a.date_created,
                date_modified = a.date_modified,
                user_id_created = a.user_id_created,
                user_id_modified = a.user_id_modified,

            });

            return query.Take(1).FirstOrDefault();
        }

        public List<DraftLawViewModel> ObtenerNotificables()
        {
            var query = Set.Where(a => a.active == true && a.notified == false && a.draft_laws_status.notifiable == true).Select(a => new DraftLawViewModel
            {

                draft_law_id = a.draft_law_id,
                draft_law_number = a.draft_law_number,
                interest_area = a.interest_areas.name,
                interest_area_id = a.interest_area_id,
                origin = a.origin,
                title = a.title,
                commission_id = a.commission_id,
                commission = a.commissions.name,
                status=a.draft_laws_status.name,
                date_created = a.date_created,
                date_modified = a.date_modified,
                user_id_created = a.user_id_created,
                user_id_modified = a.user_id_modified,

            });

            return query.ToList();
        }

        public DraftLawViewModel ObtenerPorNroProyectoMigrar(int draft_law_number, int draft_law_status_id)
        {
            var query = Set.Where(a => a.draft_law_number == draft_law_number && a.status_id == draft_law_status_id).Select(a => new DraftLawViewModel
            {

                draft_law_id = a.draft_law_id,
                draft_law_number = a.draft_law_number,

                date_created = a.date_created,
                date_modified = a.date_modified,
                user_id_created = a.user_id_created,
                user_id_modified = a.user_id_modified,

            });

            return query.Take(1).FirstOrDefault();
        }
        public DraftLawViewModel Obtener(int draft_law_id)
        {
            var query = Set.Where(a => a.draft_law_id == draft_law_id).Select(a => new DraftLawViewModel
            {

                draft_law_id = a.draft_law_id,
                draft_law_number = a.draft_law_number,
                title = a.title,
                author = a.author,
                origin = a.origin,
                date_presentation = a.date_presentation,
                commission_id = a.commission_id,
                debate_speaker = a.debate_speaker,
                debate_speaker2 = a.debate_speaker2,
                status = a.draft_laws_status.name,
                status_comment = a.status_comment,
                interest_area_id = a.interest_area_id,
                initiative = a.initiative,
                summary = a.summary,
                link = a.link,
                date_created = a.date_created,
                date_modified = a.date_modified,
                user_id_created = a.user_id_created,
                user_id_modified = a.user_id_modified,

            });

            return query.Take(1).FirstOrDefault();
        }

        public void ActualizarNotificacion(List<DraftLawViewModel> list)
        {

            List<int> draft_law_ids = list.Select(a => a.draft_law_id).ToList();
            Set.Where(t => draft_law_ids.Contains(t.draft_law_id))
            .Update(t => new draft_laws { notified = true });
        }

        public GridModel<DraftLawViewModel> ObtenerMisProyectosLey(DataTableAjaxPostModel filters, List<int> commissions, List<int> interest_areas)
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


            GridModel<DraftLawViewModel> resultado = new GridModel<DraftLawViewModel>();
            IQueryable<draft_laws> queryFilters = Set;

            queryFilters = queryFilters.Where(a => commissions.Contains(a.commission_id.HasValue ? a.commission_id.Value : -1) && interest_areas.Contains(a.interest_area_id.HasValue ? a.interest_area_id.Value : -1));

            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.title.ToLower().Contains(srch) || s.author.ToLower().Contains(srch) ||
                    s.origin.ToLower().Contains(srch) || s.commissions.name.ToLower().Contains(srch) || s.interest_areas.name.ToLower().Contains(srch) ||
                    s.draft_laws_status.name.ToLower().Contains(srch) || s.draft_law_number.ToString().ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new DraftLawViewModel
            {
                draft_law_id = a.draft_law_id,
                draft_law_number = a.draft_law_number,
                title = a.title,
                author = a.author,
                origin = a.origin,
                date_presentation = a.date_presentation,
                commission_id = a.commission_id,
                commission = a.commissions.name,
                debate_speaker = a.debate_speaker,
                debate_speaker2 = a.debate_speaker2,
                status = a.draft_laws_status.name,
                status_comment = a.status_comment,
                interest_area_id = a.interest_area_id,
                interest_area = a.interest_areas.name,
                initiative = a.initiative,
                summary = a.summary,
                link = a.link,
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "draft_law_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<DraftLawViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

        public GridModel<DraftLawViewModel> ObtenerLista(DataTableAjaxPostModel filters)
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


            GridModel<DraftLawViewModel> resultado = new GridModel<DraftLawViewModel>();
            IQueryable<draft_laws> queryFilters = Set;



            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.title.ToLower().Contains(srch) || s.author.ToLower().Contains(srch) ||
                    s.origin.ToLower().Contains(srch) || s.commissions.name.ToLower().Contains(srch) || s.interest_areas.name.ToLower().Contains(srch) ||
                    s.draft_laws_status.name.ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new DraftLawViewModel
            {
                draft_law_id = a.draft_law_id,
                draft_law_number = a.draft_law_number,
                title = a.title,
                author = a.author,
                origin = a.origin,
                date_presentation = a.date_presentation,
                commission_id = a.commission_id,
                commission = a.commissions.name,
                debate_speaker = a.debate_speaker,
                debate_speaker2 = a.debate_speaker2,
                status = a.draft_laws_status.name,
                status_comment = a.status_comment,
                interest_area_id = a.interest_area_id,
                interest_area = a.interest_areas.name,
                initiative = a.initiative,
                summary = a.summary,
                link = a.link,
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "draft_law_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<DraftLawViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

    }
}
