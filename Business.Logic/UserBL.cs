using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class UserBL
    {
        private static UserRepository oRepositorio;
        private static InvestigatorRepository oRepositorioInvestigator;
        private static UnitOfWork oUnitOfWork;

        public UserBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.UserRepository;
            oRepositorioInvestigator = oUnitOfWork.InvestigatorRepository;
        }


        public bool VerificarDuplicado(int user_id, string email)
        {
            return oRepositorio.VerificarDuplicado(user_id, email);
        }
        public UserViewModel ObtenerUser(int pIntID)
        {

            return oRepositorio.ObtenerUser(pIntID);
        }

        public GridModel<UserViewModel> ObtenerLista(UserFiltersViewModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(UserViewModel pUserViewModel)
        {



            users ousers = oRepositorio.FindById(pUserViewModel.id);
            ousers.user_name = pUserViewModel.user_name;
            ousers.user_email = pUserViewModel.user_email;
            ousers.user_role_id = pUserViewModel.user_role_id;
            ousers.user_status_id = pUserViewModel.user_status_id;

            ousers.document_type_id = pUserViewModel.document_type_id;

            ousers.doc_nro = pUserViewModel.doc_nro;
            ousers.nationality_id = pUserViewModel.nationality_id;
            ousers.contact_name = pUserViewModel.contact_name;
            ousers.phone = pUserViewModel.phone;
            ousers.address = pUserViewModel.address;


            ousers.user_id_modified = pUserViewModel.user_id_modified;
            ousers.date_modified = DateTime.Now;
            oRepositorio.Update(ousers);
            oUnitOfWork.SaveChanges();
        }


        public CurrentUserViewModel ValidarUsuario(string usuario, string contrasena, ref int tipo_error)
        {
            return oRepositorio.ValidarUsuario(usuario, contrasena, ref tipo_error);
        }

        

        public void Eliminar(int id)
        {
            users oUser = new users
            {
                id = id,
            };
            oRepositorio.Delete(oUser);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(UserViewModel pUserViewModel)
        {


            users ousers = new users
            {
                id = 0,
                user_name = pUserViewModel.user_name,
                user_pass = pUserViewModel.user_pass,
                user_email = pUserViewModel.user_email,
                user_role_id = pUserViewModel.user_role_id,
                user_status_id = pUserViewModel.user_status_id,
                document_type_id = pUserViewModel.document_type_id,

                doc_nro = pUserViewModel.doc_nro,
                nationality_id = pUserViewModel.nationality_id,
                contact_name = pUserViewModel.contact_name,
                phone = pUserViewModel.phone,
                address = pUserViewModel.address,

                date_created = DateTime.Now,
                user_id_created = pUserViewModel.user_id_created

            };
            oRepositorio.Add(ousers);
            oUnitOfWork.SaveChanges();
        }

        public void AgregarInvestigador(InvestigatorViewModel pInvestigatorViewModel)
        {


            users ousers = new users
            {
                id = 0,
                user_name = pInvestigatorViewModel.user_name,
                user_email = pInvestigatorViewModel.user_email,
                user_pass = pInvestigatorViewModel.user_pass,
                contact_name = pInvestigatorViewModel.contact_name,
                user_role_id = 11,
                user_status_id = 1,
                document_type_id = pInvestigatorViewModel.document_type_id,

                doc_nro = pInvestigatorViewModel.doc_nro,
                nationality_id = pInvestigatorViewModel.nationality_id,
                //  contract_name = pInvestigatorViewModel.contract_name,
                phone = pInvestigatorViewModel.phone,
                address = pInvestigatorViewModel.address,

                date_created = DateTime.Now,
                user_id_created = pInvestigatorViewModel.user_id_created

            };
            ousers = oRepositorio.Add(ousers);

            investigators oinvestigators = new investigators
            {
                investigator_id = 0,
                user_id = ousers.id,
                first_name = pInvestigatorViewModel.first_name,
                second_name = pInvestigatorViewModel.second_name,
                last_name = pInvestigatorViewModel.last_name,
                second_last_name = pInvestigatorViewModel.second_last_name,

                gender_id = pInvestigatorViewModel.gender_id,
                mobile_phone = pInvestigatorViewModel.mobile_phone,
                birthdate = pInvestigatorViewModel.birthdate,

                institution_id = pInvestigatorViewModel.institution_id,
                investigation_group_id = pInvestigatorViewModel.investigation_group_id,
                program_id = pInvestigatorViewModel.program_id


            };
            oinvestigators.interest_areas = oRepositorio.InterestAreasByfilters(pInvestigatorViewModel.interest_areas);

            oinvestigators = oRepositorioInvestigator.Add(oinvestigators);

            oUnitOfWork.SaveChanges();
        }

        public Select2Model ObtenerInstituciones(string term_search, int page)
        {
            return oRepositorio.ObtenerInstituciones(term_search, page);
        }
    }
}
