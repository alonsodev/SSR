using Domain.Entities;
using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<users>
    {
        internal UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public List<SelectOptionItem> CommissionsSelector()
        {


            var lista = this.Context.Set<commissions>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.commission_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> KnowledgeAreasSelector()
        {
            var lista = this.Context.Set<knowledge_areas>();
           
            var query_select = lista.Select(a => new SelectOptionItem
            {
                Value = a.knowledge_area_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return query_select.ToList();
        }


      
        public List<SelectOptionItem> ConsultationTypesSelector()
        {
            var lista = this.Context.Set<consultation_types>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.consultation_type_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> EducationLevelsSelector()
        {
            var lista = this.Context.Set<education_levels>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.education_level_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> StatusSelector()
        {
            var lista = this.Context.Set<draft_laws_status>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.draft_law_status_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> OriginSelector()
        {
            var lista = this.Context.Set<draft_laws>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.origin.ToString(),
                Text = a.origin,
            }).Distinct().OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> EducationLevelsSelector(int educational_institution_id, int program_id)
        {
            var lista = this.Context.Set<snies>();
            var consulta = lista.Where(a => a.educational_institution_id == educational_institution_id&& a.program_id == program_id).Select(a => new SelectOptionItem
            {
                Value = a.education_level_id.ToString(),
                Text = a.education_levels.name,
            }).OrderBy(a=> a.Text);

            return consulta.ToList();
        }

        

        public List<SelectOptionItem> EducationalInstitutionsSelector()
        {
            var lista = this.Context.Set<educational_institutions>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.educational_institution_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> ReasonRejectsSelector()
        {
            var lista = this.Context.Set<reason_rejects>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.reason_reject_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> TagsSelector()
        {
            var lista = this.Context.Set<tags>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.tag_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> DebateSpeakersSelector()
        {
            var lista = this.Context.Set<users>();
            var consulta = lista.Where(a=> a.user_role_id==9).Select(a => new SelectOptionItem
            {
                Value = a.id.ToString(),
                Text = a.user_name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> AcademicLevelsSelector()
        {


            var lista = this.Context.Set<academic_levels>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.academic_level_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }


        public List<SelectOptionItem> InvestigationGroupsSelector(int institution_id)
        {


            var lista = this.Context.Set<investigation_groups>();
            var consulta = lista.Where(a => a.institution_id == institution_id).Select(a => new SelectOptionItem
            {
                Value = a.investigation_group_id.ToString(),
                Text = a.name,
                AdditionalField= a.code
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

       
        public List<interest_areas> InterestAreasByfilters(List<int> InterestAreas)
        {


            var lista = this.Context.Set<interest_areas>();
            var consulta = lista.Where(a => InterestAreas.Contains(a.interest_area_id));

            return consulta.ToList();
        }

       

        public List<commissions> CommissionsByfilters(List<int> Commissions)
        {


            var lista = this.Context.Set<commissions>();
            var consulta = lista.Where(a => Commissions.Contains(a.commission_id));

            return consulta.ToList();
        }
        public List<SelectOptionItem> DepartmentsSelector()
        {


            var lista = this.Context.Set<departments>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.department_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> MunicipalitiesSelector(int department_id)
        {


            var lista = this.Context.Set<municipalities>();
            var consulta = lista.Where(a=> a.department_id== department_id).Select(a => new SelectOptionItem
            {
                Value = a.municipality_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> InterestAreasSelector()
        {


            var lista = this.Context.Set<interest_areas>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.interest_area_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }
        public List<SelectOptionItem> ProgramsSelector()
        {
            var lista = this.Context.Set<programs>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.program_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> ProgramsSelector(int educational_institution_id)
        {
            var lista = this.Context.Set<snies>();
            var consulta = lista.Where(a=>a.educational_institution_id==educational_institution_id).Select(a => new SelectOptionItem
            {
                Value = a.program_id.ToString(),
                Text = a.programs.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public List<SelectOptionItem> EducationLevelsSelector(int educational_institution_id)
        {
            var lista = this.Context.Set<snies>();
            var consulta = lista.Where(a => a.educational_institution_id == educational_institution_id).Select(a => new SelectOptionItem
            {
                Value = a.education_level_id.ToString(),
                Text = a.education_levels.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }
        public List<SelectOptionItem> GrantedTitlesSelector(int educational_institution_id, int education_level_id)
        {
            var lista = this.Context.Set<snies>();
            var consulta = lista.Where(a => a.educational_institution_id == educational_institution_id && a.education_level_id== education_level_id).Select(a => new SelectOptionItem
            {
                Value = a.snie_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }
        public List<SelectOptionItem> GendersSelector()
        {
            var lista = this.Context.Set<genders>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.gender_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }



        public List<SelectOptionItem> DocumentTypesSelector()
        {
            var lista = this.Context.Set<document_types>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.document_type_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);
            return consulta.ToList();
        }
        public CurrentUserViewModel GetCurrentUser(int user_id)
        {
           return  Set.Where(a => a.id == user_id).Select(a =>
                    new CurrentUserViewModel
                    {
                        user_id = a.id,
                        name = a.contact_name, // contact_name
                       pass = a.user_pass,
                        user_email = a.user_email,
                        status_id = a.user_status_id,
                        investigator_id = a.investigators.Select(i => i.investigator_id).Take(1).FirstOrDefault(),
                        permissions = a.roles.role_permissions.Select(p => p.permissions.id_permission).ToList(),
                        avatar = a.avatar,


                        
                        role_id = a.user_role_id,


                    }
                ).Take(1).FirstOrDefault();
        }

        public CurrentUserViewModel ValidarUsuario(string usuario, string contrasena, ref int tipo_error)
        {
            CurrentUserViewModel result = null;

            var cont = Set.Where(a => a.user_email == usuario).Count();

            if (cont > 0)
            {
                result = Set.Where(a => a.user_email == usuario).Select(a =>
                   new CurrentUserViewModel
                   {
                       user_id = a.id,
                       name = a.contact_name, // contact_name
                       pass = a.user_pass,
                       user_email = a.user_email,
                       status_id = a.user_status_id,
                       investigator_id = a.investigators.Select(i => i.investigator_id).Take(1).FirstOrDefault(),
                       permissions = a.roles.role_permissions.Select(p => p.permissions.id_permission).ToList(),
                       avatar = a.avatar,
                       role_id=a.user_role_id,
                       first_time=a.user_date_last_login.HasValue?0:1
                   
                   }
               ).Take(1).FirstOrDefault();

                if (result.pass != contrasena)
                    tipo_error = -3;
                else
                {
                    if (result.status_id !=1)
                        tipo_error = -2;
                    else
                    {
                        tipo_error = 0;
                    }
                }


            }
            else
            {
                tipo_error = -1;
            }

            return result;
        }

        public List<SelectOptionItem> NationalitiesSelector()
        {
            var lista = this.Context.Set<nationalities>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.nationality_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public int? ObtenerPonente(string author_aux)
        {
            return Set.Where(a => a.user_name.ToLower() == author_aux.ToLower()).Select(a => a.id).Take(1).FirstOrDefault();
        }

        public List<SelectOptionItem> RolesSelector()
        {
            var lista = this.Context.Set<roles>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.role_id.ToString(),
                Text = a.role,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }
        public List<SelectOptionItem> EstatusUserSelector()
        {
            var lista = this.Context.Set<user_status>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.user_status_id.ToString(),
                Text = a.name,
            }).OrderBy(a => a.Text);

            return consulta.ToList();
        }

        public bool VerificarDuplicado(int user_id, string email)
        {

            email = email.Trim().ToLower();
            var count = Set.Where(a => a.id != user_id && a.user_email.ToLower() == email).Count();

            return count == 0;
        }
        public UserViewModel GetByUserCodeActivate(string user_code)
        {
            var query = Set.Where(a => a.user_code_activate == user_code).Select(a => new UserViewModel
            {

                id = a.id,
                user_name = a.user_name,
                user_email = a.user_email,
                user_pass = a.user_pass,
                user_role_id = a.user_role_id,
                user_status_id = a.user_status_id,
                is_super_admin = a.is_super_admin,
                user_date_last_login = a.user_date_last_login,
                date_created = a.date_created,
                date_modified = a.date_modified,
                user_id_created = a.user_id_created,
                user_id_modified = a.user_id_modified,
                document_type_id = a.document_type_id,

                doc_nro = a.doc_nro,
                nationality_id = a.nationality_id,
                contact_name = a.contact_name,
                phone = a.phone,
                address = a.address,
            });

            return query.Take(1).FirstOrDefault();
        }

        public UserViewModel GetByUserCodeRecover(string user_code)
        {
            var query = Set.Where(a => a.user_code_recover == user_code).Select(a => new UserViewModel
            {

                id = a.id,
                user_name = a.user_name,
                user_email = a.user_email,
                user_pass = a.user_pass,
                user_role_id = a.user_role_id,
                user_status_id = a.user_status_id,
                is_super_admin = a.is_super_admin,
                user_date_last_login = a.user_date_last_login,
                date_created = a.date_created,
                date_modified = a.date_modified,
                user_id_created = a.user_id_created,
                user_id_modified = a.user_id_modified,
                document_type_id = a.document_type_id,

                doc_nro = a.doc_nro,
                nationality_id = a.nationality_id,
                contact_name = a.contact_name,
                phone = a.phone,
                address = a.address,
            });

            return query.Take(1).FirstOrDefault();
        }

        public  UserViewModel ObtenerUser(string user_email)
        {
            var query = Set.Where(a => a.user_email == user_email).Select(a => new UserViewModel
            {

                id = a.id,
                user_name = a.user_name,
                user_email = a.user_email,
                user_pass = a.user_pass,
                user_role_id = a.user_role_id,
                user_status_id = a.user_status_id,
                is_super_admin = a.is_super_admin,
                user_date_last_login = a.user_date_last_login,
                date_created = a.date_created,
                date_modified = a.date_modified,
                user_id_created = a.user_id_created,
                user_id_modified = a.user_id_modified,
                document_type_id = a.document_type_id,

                doc_nro = a.doc_nro,
                nationality_id = a.nationality_id,
                contact_name = a.contact_name,
                phone = a.phone,
                address = a.address,
            });

            return query.Take(1).FirstOrDefault();
        }
        
        public UserViewModel ObtenerUser(int id)
        {
            var query = Set.Where(a => a.id == id).Select(a => new UserViewModel
            {

                id = a.id,
                user_name = a.user_name,
                user_email = a.user_email,
                user_pass = a.user_pass,
                user_role_id = a.user_role_id,
                user_status_id = a.user_status_id,
                is_super_admin = a.is_super_admin,
                user_date_last_login = a.user_date_last_login,
                date_created = a.date_created,
                date_modified = a.date_modified,
                user_id_created = a.user_id_created,
                user_id_modified = a.user_id_modified,
                document_type_id = a.document_type_id,
               

                doc_nro = a.doc_nro,
                nationality_id = a.nationality_id,
                contact_name = a.contact_name,
                phone = a.phone,
                address = a.address,
                avatar=a.avatar
            });

            return query.Take(1).FirstOrDefault();
        }

        public List<UserViewModel> ObtenerPorConcepto(int concept_id)
        {
            IQueryable<users> queryFilters = Set;
            var query = queryFilters.Where(a => a.concept_debate_speakers.Where( b=> b.concept_id==concept_id).Count()>0).Select(a => new UserViewModel
            {
                id = a.id,
                user_name = a.user_name,
                user_email = a.user_email,
                user_pass = a.user_pass,
                user_role = a.roles.role,
                document_type = a.document_types.name,
                doc_nro = a.doc_nro,
                user_status = a.user_status.name,
                contact_name=a.contact_name

            });
            return query.ToList();
        }

        public List<UserViewModel> ObtenerPorPermiso(int permision_id)
        {
            IQueryable<users> queryFilters = Set;
            var query = queryFilters.Where(a=> a.roles.role_permissions.Where(p=> p.id_permission== permision_id).Count()>0).Select(a => new UserViewModel
            {
                id = a.id,
                user_name = a.user_name,
                user_email = a.user_email,
                user_pass = a.user_pass,
                user_role = a.roles.role,
                document_type = a.document_types.name,
                doc_nro = a.doc_nro,
                user_status = a.user_status.name,
                contact_name=a.contact_name,

            });
            return query.ToList();
        }

        public GridModel<UserViewModel> ObtenerLista(UserFiltersViewModel filters)
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


            GridModel<UserViewModel> resultado = new GridModel<UserViewModel>();
            IQueryable<users> queryFilters = Set;



            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.user_name.ToLower().Contains(srch) || s.user_email.ToLower().Contains(srch)
                || s.roles.role.ToLower().Contains(srch) || s.doc_nro.ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new UserViewModel
            {
                id = a.id,
                user_name = a.user_name,
                user_email = a.user_email,
                user_pass = a.user_pass,
                user_role = a.roles.role,
                document_type = a.document_types.name,
                doc_nro = a.doc_nro,
                user_status = a.user_status.name,

            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<UserViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

        public Select2Model ObtenerInstituciones(string term_search, int page)
        {

            Select2Model result = new Select2Model();


            var lista = this.Context.Set<institutions>();
            IQueryable<institutions> queryFilters = lista;

            if (!String.IsNullOrEmpty(term_search))
                queryFilters = lista.Where(a => a.name.Contains(term_search));
            result.total_count = queryFilters.Count();


            var query = queryFilters.Select(a => new Select2ItemModel
            {
                id = a.institution_id.ToString(),
                text = a.name

            });
            string sortExpression = "text asc";
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<Select2ItemModel>(query, sortExpression.Trim());
            result.items = query.Skip((page - 1) * 10).Take(10).ToList();

            return result;

        }
    }
}
