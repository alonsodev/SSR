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
    public class DraftLawStatusBL
    {
        private static DraftLawStatusRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public DraftLawStatusBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.DraftLawStatusRepository;
        }


        public Dictionary<string, DraftLawStatusViewModel> ObtenerDiccionarioPorNombre(List<string> lista, int user_id)
        {
            var olista = oRepositorio.ObtenerTodos();


            Dictionary<string, DraftLawStatusViewModel> dictionary = new Dictionary<string, DraftLawStatusViewModel>();

            foreach (var item in olista)
            {
                dictionary.Add(item.name, item);
            }
            foreach (var name in lista)
            {

                if (!String.IsNullOrEmpty(name) && !dictionary.ContainsKey(name))
                {
                    draft_laws_status odraft_laws_status = new draft_laws_status
                    {
                        draft_law_status_id = 0,
                        name = name,
                        notifiable = false,


                    };
                    odraft_laws_status = oRepositorio.Add(odraft_laws_status);
                    oUnitOfWork.SaveChanges();
                    DraftLawStatusViewModel oDraftLawStatusViewModel = new DraftLawStatusViewModel();
                    oDraftLawStatusViewModel.draft_law_status_id = odraft_laws_status.draft_law_status_id;
                    oDraftLawStatusViewModel.name = odraft_laws_status.name;
                    oDraftLawStatusViewModel.notifiable = odraft_laws_status.notifiable;

                    dictionary.Add(odraft_laws_status.name, oDraftLawStatusViewModel);

                }
            }
            return dictionary;
        }

    }
}
