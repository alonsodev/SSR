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
        public bool ExisteConcepto(int draft_law_id, int investigator_id)
        {
            int count = Set.Where(a => a.draft_law_id == draft_law_id && a.investigator_id == investigator_id).Count();
            return count > 0;

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
                bibliography = a.bibliography,
                pdf_path = a.pdf_path,
                certification_path = a.certification_path,
                speakers = a.draft_laws.debate_speakers.Select(t => t.user_id.ToString()).Distinct().ToList(),
                user_id_created = a.user_id_created,
                concept_status_id = a.concept_status_id,
                hash = a.hash
            });

            return query.Take(1).FirstOrDefault();
        }

        public ConceptHtmlViewModel ObtenerHtmlConcept(int concept_id)
        {
            var query = Set.Where(a => a.concept_id == concept_id).Select(a => new ConceptHtmlViewModel
            {

                concept_id = a.concept_id,
                fecha_presentacion = a.date_created,
                investigador = a.investigators.first_name + " " + a.investigators.second_name + " " + a.investigators.last_name + " " + a.investigators.second_last_name,
                cedula = a.investigators.users.doc_nro,
                institucion = a.investigators.institutions.name,
                grupo_vinculado = a.investigators.investigation_groups.name,
                codigo_grupo_vinculado = a.investigators.investigation_groups.code,
                fecha_elaboracion = a.date_created,
                bibliography = a.bibliography,
                concept = a.concept,
                draft_law_id = a.draft_law_id.Value,
                draft_law_number = a.draft_laws.draft_law_number,
                draft_law_title = a.draft_laws.title,
                draf_law_commission = a.draft_laws.commissions.name,
                draf_law_fecha_presentacion = a.draft_laws.date_presentation,
                draf_law_interested_area = a.draft_laws.interest_areas.name,
                draf_law_origen = a.draft_laws.origin,
                summary = a.summary,
                tags = a.concepts_tags.Select(b => b.tags.name.ToUpper()).ToList()
            });

            return query.Take(1).FirstOrDefault();
        }

        public CertificationHtmlViewModel ObtenerHtmlCertification(int concept_id)
        {
            var query = Set.Where(a => a.concept_id == concept_id).Select(a => new CertificationHtmlViewModel
            {

                concept_id = a.concept_id,
                fecha_presentacion = a.date_created,
                investigador = a.investigators.first_name + " " + a.investigators.second_name + " " + a.investigators.last_name + " " + a.investigators.second_last_name,
                cedula = a.investigators.users.doc_nro,
                institucion = a.investigators.institutions.name,
                grupo_vinculado = a.investigators.investigation_groups.name,

                draft_law_id = a.draft_law_id.Value,
                draft_law_title = a.draft_laws.title,
                fecha_aceptacion = a.date_created,
                fecha_certificacion = a.date_created,
                hash = a.hash.Value,
                ciudad = a.investigators.users.municipalities.name


            });

            return query.Take(1).FirstOrDefault();
        }
        public string ObtenerPdfpath(int concept_id)
        {
            var query = Set.Where(a => a.concept_id == concept_id).Select(a => a.pdf_path);

            return query.Take(1).FirstOrDefault();
        }

        public string ObtenerCertificadoPdfpath(int concept_id)
        {
            var query = Set.Where(a => a.concept_id == concept_id).Select(a => a.certification_path);

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



                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.draft_laws.title.ToLower().Contains(srch)
                    || s.draft_laws.author.ToLower().Contains(srch) || s.draft_laws.origin.ToLower().Contains(srch)
                  || s.draft_laws.commissions.name.ToLower().Contains(srch) || s.draft_laws.interest_areas.name.ToLower().Contains(srch) ||
                  s.draft_laws.draft_laws_status.name.ToLower().Contains(srch) || s.draft_laws.draft_law_number.ToString().ToLower().Contains(srch)
                  || s.concept_id.ToString().ToLower().Contains(srch)));


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

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.draft_laws.title.ToLower().Contains(srch)
                    || s.draft_laws.author.ToLower().Contains(srch) || s.draft_laws.origin.ToLower().Contains(srch)
                  || s.draft_laws.commissions.name.ToLower().Contains(srch) || s.draft_laws.interest_areas.name.ToLower().Contains(srch) ||
                  s.draft_laws.draft_laws_status.name.ToLower().Contains(srch) || s.draft_laws.draft_law_number.ToString().ToLower().Contains(srch)
                  || s.concept_id.ToString().ToLower().Contains(srch)));




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
            queryFilters = queryFilters.Where(a => a.concept_debate_speakers.Select(d => d.user_id).Contains(user_id));
            queryFilters = queryFilters.Where(a => a.concept_status_id == 2 || (a.concept_status_id == 4) || (a.concept_status_id == 5 && a.concepts_status_logs.Where(l => l.concept_status_id == 5 && l.user_id_created == user_id).Count() == 0));//falta mejorar



            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.draft_laws.title.ToLower().Contains(srch)
                     || s.draft_laws.author.ToLower().Contains(srch) || s.draft_laws.origin.ToLower().Contains(srch)
                   || s.draft_laws.commissions.name.ToLower().Contains(srch) || s.draft_laws.interest_areas.name.ToLower().Contains(srch) ||
                   s.draft_laws.draft_laws_status.name.ToLower().Contains(srch) || s.draft_laws.draft_law_number.ToString().ToLower().Contains(srch)
                   || s.concept_id.ToString().ToLower().Contains(srch)));




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

        public VerifyCertificationViewModel ObtenerVerificacionCertificado(Guid hash)
        {

            var query = Set.Where(a => a.hash == hash).Select(a => new VerifyCertificationViewModel
            {

                concept_id = a.concept_id,
                fecha_presentacion = a.date_created.Value,
                investigador = a.investigators.first_name + " " + a.investigators.second_name + " " + a.investigators.last_name + " " + a.investigators.second_last_name,
                cedula = a.investigators.users.doc_nro,
                institucion = a.investigators.institutions.name,
                grupo = a.investigators.investigation_groups.name,
                codigo_grupo = a.investigators.investigation_groups.code,
                nro_proyecto = a.draft_laws.draft_law_number.ToString(),
                titulo_proyecto = a.draft_laws.title,
                concept_status_id = a.concept_status_id,

            });

            return query.Take(1).FirstOrDefault();
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

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.draft_laws.title.ToLower().Contains(srch)
                    || s.draft_laws.author.ToLower().Contains(srch) || s.draft_laws.origin.ToLower().Contains(srch)
                  || s.draft_laws.commissions.name.ToLower().Contains(srch) || s.draft_laws.interest_areas.name.ToLower().Contains(srch) ||
                  s.draft_laws.draft_laws_status.name.ToLower().Contains(srch) || s.draft_laws.draft_law_number.ToString().ToLower().Contains(srch)
                  || s.concept_id.ToString().ToLower().Contains(srch)));




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

            queryFilters = queryFilters.Where(a => a.investigator_id == investigator_id && (a.concept_status_id == 2 || a.concept_status_id == 4 || a.concept_status_id == 5 || a.concept_status_id == 6));

            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.draft_laws.title.ToLower().Contains(srch)
                    || s.draft_laws.author.ToLower().Contains(srch) || s.draft_laws.origin.ToLower().Contains(srch)
                  || s.draft_laws.commissions.name.ToLower().Contains(srch) || s.draft_laws.interest_areas.name.ToLower().Contains(srch) ||
                  s.draft_laws.draft_laws_status.name.ToLower().Contains(srch) || s.draft_laws.draft_law_number.ToString().ToLower().Contains(srch)
                  || s.concept_id.ToString().ToLower().Contains(srch)));




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
            List<int> users = new List<int>();
            if (interest_area_id != 0)
            {
                users = queryFilters.Where(a => a.draft_laws.interest_area_id == interest_area_id).Select(a => a.investigators.user_id.Value).ToList();

            }




            var query = queryFilters.GroupBy(l => new { user_id = l.investigators.user_id.Value, contact_name = l.investigators.users.contact_name, institution = l.investigators.institutions.name, avatar = l.investigators.users.avatar }).Select(a => new RankingViewModel
            {
                position = 0,
                user_id = a.Key.user_id,
                investigator = a.Key.contact_name,
                institution = a.Key.institution,
                avatar = a.Key.avatar,
                point = a.Sum(c => c.qualification)
            });


            int count_records = 0;
            if (interest_area_id != 0)
            {
                count_records = query.Where(a => users.Contains(a.user_id.Value)).Count();
                resultado.rows = query.Where(a => users.Contains(a.user_id.Value)).OrderByDescending(a => a.point).Skip(filters.start).Take(filters.length).ToList();
            }
            else
            {
                count_records = query.Count();
                resultado.rows = query.OrderByDescending(a => a.point).Skip(filters.start).Take(filters.length).ToList();
            }
            int count_records_filtered = count_records;


            resultado.total = count_records;
            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }
    }
}
