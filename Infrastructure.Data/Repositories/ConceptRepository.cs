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
    public class ConceptRepository : Repository<concepts>
    {
        internal ConceptRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public int NumeroPonentes(int concept_id)
        {
            var lista = this.Context.Set<debate_speakers>();

            int count = Set.Where(a => a.concept_id == concept_id).Select(a => a.draft_laws.debate_speakers.Count()).Take(1).FirstOrDefault();
            return count;
        }

        public int NumeroCalificaciones(int concept_id)
        {
            var lista = this.Context.Set<debate_speakers>();

            int count = Set.Where(a => a.concept_id == concept_id).Select(a => a.concepts_status_logs.Where(c => c.concept_status_id == 5).Count()).Take(1).FirstOrDefault();
            return count;
        }

        public ConceptViewModel Obtener(int concept_id)
        {
            var query = Set.Where(a => a.concept_id == concept_id).Select(a => new ConceptViewModel
            {

                concept_id = a.concept_id,
                concept = a.concept,
                summary = a.summary,
                investigator_id = a.investigator_id,
                commission_id = a.draft_laws.commission_id,
                draft_law_number = a.draft_laws.draft_law_number,
                title = a.draft_laws.title,
                draft_law_id = a.draft_laws.draft_law_id,
                tags = a.concepts_tags.Select(t => t.tag_id.ToString()).Distinct().ToList(),
                bibliography = a.bibliography
            });

            return query.Take(1).FirstOrDefault();
        }



        public GridModel<ConceptViewModel> ObtenerLista(DataTableAjaxPostModel filters, int investigator_id)
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


            GridModel<ConceptViewModel> resultado = new GridModel<ConceptViewModel>();
            IQueryable<concepts> queryFilters = Set;

            queryFilters = queryFilters.Where(a => a.investigator_id == investigator_id);

            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.concept.ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new ConceptViewModel
            {
                concept_id = a.concept_id,
                //  concept = a.concept,
                summary = a.summary,
                draft_law_id = a.draft_laws.draft_law_id,
                draft_law_number = a.draft_laws.draft_law_number,
                title = a.draft_laws.title,
                author = a.draft_laws.author,

                date_presentation = a.draft_laws.date_presentation,
                date_created = a.date_created,
                commission = a.draft_laws.commissions.name,
                status = a.concepts_status.name,


                interest_area = a.draft_laws.interest_areas.name,

                //summary_draft_law = a.draft_laws.summary,
                qualification = a.qualification
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "concept_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<ConceptViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

        public MyHistoryViewModel ObtenerMiHistorial(int investigator_id)
        {
            GridModel<RankingViewModel> resultado = new GridModel<RankingViewModel>();
            IQueryable<concepts> queryFilters = Set;

            var oMyHistoryViewModel = queryFilters.Where(a => (a.concept_status_id == 5 || a.concept_status_id == 6) && a.investigator_id == investigator_id).GroupBy(l => new { investigator_id = l.investigator_id }).Select(a => new MyHistoryViewModel
            {
                qualified_concepts = a.Count(),
                my_points = a.Sum(c => c.qualification)
            }).Take(1).FirstOrDefault();


            return oMyHistoryViewModel;
        }

        public GridModel<ConceptViewModel> ObtenerEmitidos(DataTableAjaxPostModel filters)
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


            GridModel<ConceptViewModel> resultado = new GridModel<ConceptViewModel>();
            IQueryable<concepts> queryFilters = Set;

            queryFilters = queryFilters.Where(a => a.concept_status_id == 1);

            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.concept.ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new ConceptViewModel
            {
                concept_id = a.concept_id,
                //  concept = a.concept,
                summary = a.summary,
                draft_law_id = a.draft_laws.draft_law_id,
                draft_law_number = a.draft_laws.draft_law_number,
                title = a.draft_laws.title,
                author = a.draft_laws.author,

                date_presentation = a.draft_laws.date_presentation,
                date_created = a.date_created,
                commission = a.draft_laws.commissions.name,
                status = a.concepts_status.name,
                investigator = a.investigators.users.contact_name,
                institution = a.investigators.institutions.name,

                interest_area = a.draft_laws.interest_areas.name,

                //summary_draft_law = a.draft_laws.summary,
                qualification = a.qualification
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "concept_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<ConceptViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

        public GridModel<ConceptViewModel> ObtenerPorCalificar(DataTableAjaxPostModel filters, int user_id)
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


            GridModel<ConceptViewModel> resultado = new GridModel<ConceptViewModel>();
            IQueryable<concepts> queryFilters = Set;

            queryFilters = queryFilters.Where(a => a.concept_status_id == 2 || (a.concept_status_id == 4) || (a.concept_status_id == 5 && a.concepts_status_logs.Where(l => l.concept_status_id == 5 && l.user_id_created == user_id).Count() == 0));//falta mejorar

            queryFilters = queryFilters.Where(a => a.draft_laws.debate_speakers.Select(d => d.user_id).Contains(user_id));

            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.concept.ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new ConceptViewModel
            {
                concept_id = a.concept_id,
                //  concept = a.concept,
                summary = a.summary,
                draft_law_id = a.draft_laws.draft_law_id,
                draft_law_number = a.draft_laws.draft_law_number,
                title = a.draft_laws.title,
                author = a.draft_laws.author,

                date_presentation = a.draft_laws.date_presentation,
                date_created = a.date_created,
                commission = a.draft_laws.commissions.name,
                status = a.concepts_status.name,
                investigator = a.investigators.users.contact_name,
                institution = a.investigators.institutions.name,

                interest_area = a.draft_laws.interest_areas.name,

                //summary_draft_law = a.draft_laws.summary,
                //qualification = a.qualification
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "concept_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<ConceptViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

        public GridModel<ConceptViewModel> ObtenerRecibidos(DataTableAjaxPostModel filters, int user_id)
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


            GridModel<ConceptViewModel> resultado = new GridModel<ConceptViewModel>();
            IQueryable<concepts> queryFilters = Set;

            queryFilters = queryFilters.Where(a => a.concepts_status_logs.Where(b => (b.concept_status_id == 5 || b.concept_status_id == 6) && b.user_id_created == user_id).Count() > 0);//falta mejorar

            queryFilters = queryFilters.Where(a => a.draft_laws.debate_speakers.Select(d => d.user_id).Contains(user_id));

            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.concept.ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new ConceptViewModel
            {
                concept_id = a.concept_id,
                //  concept = a.concept,
                summary = a.summary,
                draft_law_id = a.draft_laws.draft_law_id,
                draft_law_number = a.draft_laws.draft_law_number,
                title = a.draft_laws.title,
                author = a.draft_laws.author,

                date_presentation = a.draft_laws.date_presentation,
                date_created = a.date_created,
                commission = a.draft_laws.commissions.name,
                status = a.concepts_status.name,
                investigator = a.investigators.users.contact_name,
                institution = a.investigators.institutions.name,

                interest_area = a.draft_laws.interest_areas.name,

                //summary_draft_law = a.draft_laws.summary,
                qualification = a.concepts_status_logs.Where(b => (b.concept_status_id == 5 || b.concept_status_id == 6) && b.user_id_created == user_id).Select(c => c.qualification).Take(1).FirstOrDefault()
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "concept_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<ConceptViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

        public GridModel<ConceptViewModel> ObtenerCertificados(DataTableAjaxPostModel filters, int investigator_id)
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


            GridModel<ConceptViewModel> resultado = new GridModel<ConceptViewModel>();
            IQueryable<concepts> queryFilters = Set;

            queryFilters = queryFilters.Where(a => a.investigator_id == investigator_id && (a.concept_status_id == 5 || a.concept_status_id == 6));

            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.concept.ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new ConceptViewModel
            {
                concept_id = a.concept_id,
                //  concept = a.concept,
                summary = a.summary,
                draft_law_id = a.draft_laws.draft_law_id,
                draft_law_number = a.draft_laws.draft_law_number,
                title = a.draft_laws.title,
                author = a.draft_laws.author,

                date_presentation = a.draft_laws.date_presentation,
                date_created = a.date_created,
                commission = a.draft_laws.commissions.name,
                status = a.concepts_status.name,


                interest_area = a.draft_laws.interest_areas.name,

                //summary_draft_law = a.draft_laws.summary,
                qualification = a.qualification
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "concept_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<ConceptViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }


        public GridModel<RankingViewModel> ObtenerRanking(DataTableAjaxPostModel filters, int interest_area_id)
        {


            GridModel<RankingViewModel> resultado = new GridModel<RankingViewModel>();
            IQueryable<concepts> queryFilters = Set;

            queryFilters = queryFilters.Where(a => a.draft_laws.interest_area_id == interest_area_id);




            var query = queryFilters.GroupBy(l => new { contact_name = l.investigators.users.contact_name, institution = l.investigators.institutions.name, avatar = l.investigators.users.avatar }).Select(a => new RankingViewModel
            {
                position = 0,
                investigator = a.Key.contact_name,
                institution = a.Key.institution,
                avatar= a.Key.avatar,
                point = a.Sum(c => c.qualification)
            });

            int count_records = query.Count();
            int count_records_filtered = count_records;

            resultado.rows = query.OrderByDescending(a => a.point).Skip(filters.start).Take(filters.length).ToList();
            resultado.total = count_records;
            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }
    }
}
