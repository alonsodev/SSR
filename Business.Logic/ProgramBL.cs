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
    public class ProgramBL
    {
        private static ProgramRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public ProgramBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.ProgramRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public ProgramViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<ProgramViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(ProgramViewModel pProgramViewModel)
        {
         

        
            programs oprograms =oRepositorio.FindById(pProgramViewModel.program_id);
            oprograms.name = pProgramViewModel.name;
            
            oprograms.user_id_modified = pProgramViewModel.user_id_modified;

            oprograms.date_modified = DateTime.Now;
            oRepositorio.Update(oprograms);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            programs oProgram = new programs
            {
                program_id = id,
            };
            oRepositorio.Delete(oProgram);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(ProgramViewModel pProgramViewModel)
        {


            programs oprograms = new programs
            {
                program_id = 0,
                name= pProgramViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pProgramViewModel.user_id_created

            };
            oRepositorio.Add(oprograms);
            oUnitOfWork.SaveChanges();
        }


    }
}
