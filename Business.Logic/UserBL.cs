using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business.Logic
{
    public class UserBL
    {
        private static UserRepository oRepositorio;
        private static InvestigatorRepository oRepositorioInvestigator;
        private static InvestigatorCommissionRepository oRepositorioInvestigatorCommission;
        private static InvestigatorInterestAreaRepository oRepositorioInvestigatorInterestArea;
        private static UnitOfWork oUnitOfWork;

        public UserBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.UserRepository;
            oRepositorioInvestigator = oUnitOfWork.InvestigatorRepository;
            oRepositorioInvestigatorCommission = oUnitOfWork.InvestigatorCommissionRepository;
            oRepositorioInvestigatorInterestArea = oUnitOfWork.InvestigatorInterestAreaRepository;

        }

        public void ActivarCuenta(int id)
        {
            users ousers = new users
            {
                id = id,
                user_status_id = 1,
                user_code_activate = null

            };
            oRepositorio.Update(ousers,
                                    a => a.id,
                                    a => a.user_status_id,
                                    a => a.user_code_activate

                                );
            oUnitOfWork.SaveChanges();
        }

        public UserViewModel GetByUserCodeActivate(string user_code)
        {
            return oRepositorio.GetByUserCodeActivate(user_code);
        }

        public bool VerificarDuplicado(int user_id, string email)
        {
            return oRepositorio.VerificarDuplicado(user_id, email);
        }

        public void CambiarPassword(CambiarPasswordViewModel pCambiarPasswordViewModel)
        {
            users ousers = oRepositorio.FindById(pCambiarPasswordViewModel.userd_id);
            ousers.user_pass = pCambiarPasswordViewModel.new_pass;
            ousers.user_code_recover = null;
            // ousers.user_status_id = 1;
            ousers.date_modified = DateTime.Now;
            oRepositorio.Update(ousers);
            oUnitOfWork.SaveChanges();
        }

        public UserViewModel GetByUserCodeRecover(string user_code)
        {
            return oRepositorio.GetByUserCodeRecover(user_code);
        }

        public UserViewModel ObtenerUser(int pIntID)
        {

            return oRepositorio.ObtenerUser(pIntID);
        }

        public UserViewModel ObtenerUser(string user_mail)
        {

            return oRepositorio.ObtenerUser(user_mail);
        }

        public GridModel<UserViewModel> ObtenerLista(UserFiltersViewModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }
        public void ModificarMicuenta(UserViewModel pUserViewModel)
        {



            users ousers = oRepositorio.FindById(pUserViewModel.id);
            ousers.user_name = pUserViewModel.user_name;
            //ousers.user_email = pUserViewModel.user_email;
            // ousers.user_role_id = pUserViewModel.user_role_id;
            //ousers.user_status_id = pUserViewModel.user_status_id;

            ousers.document_type_id = pUserViewModel.document_type_id;

            ousers.doc_nro = pUserViewModel.doc_nro;
            ousers.nationality_id = pUserViewModel.nationality_id;
            ousers.contact_name = pUserViewModel.contact_name;
            ousers.phone = pUserViewModel.phone;
            ousers.address = pUserViewModel.address;
            if (pUserViewModel.avatar != null)
                ousers.avatar = pUserViewModel.avatar;

            ousers.user_id_modified = pUserViewModel.user_id_modified;
            ousers.date_modified = DateTime.Now;
            oRepositorio.Update(ousers);
            oUnitOfWork.SaveChanges();
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
        public CurrentUserViewModel GetCurrentUser(int user_id)
        {
            return oRepositorio.GetCurrentUser(user_id);
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

        public void ActualizarCodigoRecuperar(int id, string user_code)
        {
            users ousers = new users
            {
                id = id,
                user_code_recover = user_code

            };
            oRepositorio.Update(ousers,
                                    a => a.id,
                                    a => a.user_code_recover
                                );
            oUnitOfWork.SaveChanges();
        }
        public void ActualizarCodigoActivar(int id, string user_code)
        {
            users ousers = new users
            {
                id = id,
                user_code_activate = user_code

            };
            oRepositorio.Update(ousers,
                                    a => a.id,
                                    a => a.user_code_activate
                                );
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

        public int? AgregarInvestigador(InvestigatorViewModel pInvestigatorViewModel)
        {


            users ousers = new users
            {
                id = 0,
                user_name = pInvestigatorViewModel.user_name,
                user_email = pInvestigatorViewModel.user_email,
                user_pass = pInvestigatorViewModel.user_pass,
                contact_name = pInvestigatorViewModel.contact_name,
                user_role_id = 11,
                user_status_id = 2,
                document_type_id = pInvestigatorViewModel.document_type_id,

                doc_nro = pInvestigatorViewModel.doc_nro,
                nationality_id = pInvestigatorViewModel.nationality_id,
                //  contract_name = pInvestigatorViewModel.contract_name,
                phone = pInvestigatorViewModel.phone,
                address = pInvestigatorViewModel.address,
                address_municipality_id = pInvestigatorViewModel.address_municipality_id,

                address_country_id = pInvestigatorViewModel.address_country_id,
                date_created = DateTime.Now,
                user_id_created = pInvestigatorViewModel.user_id_created,
                user_code_activate = pInvestigatorViewModel.user_code_activate,
                user_code_recover = pInvestigatorViewModel.user_code_recover,

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
                program_id = pInvestigatorViewModel.program_id,
                educational_institution_id = pInvestigatorViewModel.educational_institution_id,
                education_level_id = pInvestigatorViewModel.education_level_id,
                CVLAC = pInvestigatorViewModel.CVLAC,
            };
            foreach (int interest_area_id in pInvestigatorViewModel.interest_areas)
            {
                oRepositorioInvestigatorInterestArea.Add(new investigators_interest_areas
                {
                    interest_area_id = interest_area_id,
                    investigator_id = oinvestigators.investigator_id,
                    date_created = DateTime.Now,
                    user_id_created = pInvestigatorViewModel.user_id_created,
                    date_modified = DateTime.Now,
                    user_id_modified = pInvestigatorViewModel.user_id_created,
                });
            }

            foreach (int commission_id in pInvestigatorViewModel.commissions)
            {
                oRepositorioInvestigatorCommission.Add(new investigators_commissions
                {
                    commission_id = commission_id,
                    investigator_id = oinvestigators.investigator_id,
                    date_created = DateTime.Now,
                    user_id_created = pInvestigatorViewModel.user_id_created,
                    date_modified = DateTime.Now,
                    user_id_modified = pInvestigatorViewModel.user_id_created,
                });
            }

            oinvestigators = oRepositorioInvestigator.Add(oinvestigators);

            oUnitOfWork.SaveChanges();
            return oinvestigators.user_id;
        }


        public Select2Model ObtenerInstituciones(string term_search, int page)
        {
            return oRepositorio.ObtenerInstituciones(term_search, page);
        }

        public int? ObtenerPonente(string author_aux)
        {
            return oRepositorio.ObtenerPonente(author_aux);

        }

        public void ModificarInvestigator(InvestigatorViewModel pInvestigatorViewModel)
        {


            using (var scope = new TransactionScope())
            {
                investigators oinvestigators = oRepositorioInvestigator.FindById(pInvestigatorViewModel.investigator_id);

                users ousers = oRepositorio.FindById(oinvestigators.user_id);



                ousers.user_name = pInvestigatorViewModel.user_name;
                // ousers.user_email = pInvestigatorViewModel.user_email;
                // ousers.user_pass = pInvestigatorViewModel.user_pass;
                ousers.contact_name = pInvestigatorViewModel.contact_name;
                ousers.document_type_id = pInvestigatorViewModel.document_type_id;

                ousers.doc_nro = pInvestigatorViewModel.doc_nro;
                ousers.nationality_id = pInvestigatorViewModel.nationality_id;
                //  contract_name = pInvestigatorViewModel.contract_name,
                ousers.phone = pInvestigatorViewModel.phone;
                ousers.address = pInvestigatorViewModel.address;
                ousers.address_municipality_id = pInvestigatorViewModel.address_municipality_id;

                ousers.address_country_id = pInvestigatorViewModel.address_country_id;

                ousers.user_id_modified = pInvestigatorViewModel.user_id_modified;
                ousers.date_modified = DateTime.Now;

                if (pInvestigatorViewModel.avatar != null)
                {
                    ousers.avatar = pInvestigatorViewModel.avatar;
                }
                oRepositorio.Update(ousers);


                //investigators oinvestigators = oRepositorioInvestigator.FindById(pInvestigatorViewModel.investigator_id);
                oinvestigators.user_id = ousers.id;
                oinvestigators.first_name = pInvestigatorViewModel.first_name;
                oinvestigators.second_name = pInvestigatorViewModel.second_name;
                oinvestigators.last_name = pInvestigatorViewModel.last_name;
                oinvestigators.second_last_name = pInvestigatorViewModel.second_last_name;

                oinvestigators.gender_id = pInvestigatorViewModel.gender_id;
                oinvestigators.mobile_phone = pInvestigatorViewModel.mobile_phone;
                oinvestigators.birthdate = pInvestigatorViewModel.birthdate;

                oinvestigators.institution_id = pInvestigatorViewModel.institution_id;
                oinvestigators.investigation_group_id = pInvestigatorViewModel.investigation_group_id;
                oinvestigators.program_id = pInvestigatorViewModel.program_id;
                oinvestigators.educational_institution_id = pInvestigatorViewModel.educational_institution_id;
                oinvestigators.education_level_id = pInvestigatorViewModel.education_level_id;
                oinvestigators.CVLAC = pInvestigatorViewModel.CVLAC;

                oRepositorioInvestigatorCommission.deleteMultiple(pInvestigatorViewModel.investigator_id);
                oRepositorioInvestigatorInterestArea.deleteMultiple(pInvestigatorViewModel.investigator_id);
                foreach (int interest_area_id in pInvestigatorViewModel.interest_areas)
                {
                    oRepositorioInvestigatorInterestArea.Add(new investigators_interest_areas
                    {
                        interest_area_id = interest_area_id,
                        investigator_id = pInvestigatorViewModel.investigator_id,
                        date_created = DateTime.Now,
                        user_id_created = pInvestigatorViewModel.user_id_created,
                        date_modified = DateTime.Now,
                        user_id_modified = pInvestigatorViewModel.user_id_created,
                    });
                }

                foreach (int commission_id in pInvestigatorViewModel.commissions)
                {
                    oRepositorioInvestigatorCommission.Add(new investigators_commissions
                    {
                        commission_id = commission_id,
                        investigator_id = pInvestigatorViewModel.investigator_id,
                        date_created = DateTime.Now,
                        user_id_created = pInvestigatorViewModel.user_id_created,
                        date_modified = DateTime.Now,
                        user_id_modified = pInvestigatorViewModel.user_id_created,
                    });
                }







                oRepositorioInvestigator.Update(oinvestigators);
                oUnitOfWork.SaveChanges();

                scope.Complete();
            }




        }


        public InvestigatorViewModel ObtenerInvestigator(int pIntID)
        {

            return oRepositorioInvestigator.Obtener(pIntID);
        }

    }
}
