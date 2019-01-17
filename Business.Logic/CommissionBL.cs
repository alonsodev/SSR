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
    public class CommissionBL
    {
        private static CommissionRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public CommissionBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.CommissionRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public CommissionViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }
      


        public Dictionary<string,int> ObtenerDiccionarioPorNombre(List<string> commisions,int user_id)
        {
            var olista= oRepositorio.ObtenerTodos();

            
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            foreach (var item in olista) {
                dictionary.Add(item.name, item.commission_id);
            }
            foreach (var commision in commisions)
            {
                if (!String.IsNullOrEmpty(commision) && !dictionary.ContainsKey(commision)){
                    commissions ocommissions = new commissions
                    {
                        commission_id = 0,
                        name = commision,
                        date_created = DateTime.Now,
                        user_id_created = user_id

                    };
                    ocommissions = oRepositorio.Add(ocommissions);
                    oUnitOfWork.SaveChanges();
                    dictionary.Add(ocommissions.name, ocommissions.commission_id);

                }
            }
            return dictionary;
        }


        public GridModel<CommissionViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(CommissionViewModel pCommissionViewModel)
        {
         

        
            commissions ocommissions =oRepositorio.FindById(pCommissionViewModel.commission_id);
            ocommissions.name = pCommissionViewModel.name;
            
            ocommissions.user_id_modified = pCommissionViewModel.user_id_modified;

            ocommissions.date_modified = DateTime.Now;
            oRepositorio.Update(ocommissions);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            commissions oCommission = new commissions
            {
                commission_id = id,
            };
            oRepositorio.Delete(oCommission);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(CommissionViewModel pCommissionViewModel)
        {


            commissions ocommissions = new commissions
            {
                commission_id = 0,
                name= pCommissionViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pCommissionViewModel.user_id_created

            };
            oRepositorio.Add(ocommissions);
            oUnitOfWork.SaveChanges();
        }


    }
}
