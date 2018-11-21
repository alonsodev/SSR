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
        private static UnitOfWork oUnitOfWork;

        public UserBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.UserRepository;
        }

        public void Agregar(ControlCombustibleViewModel pControlCombustibleViewModel, bool pIsTransaction = false)
        {
            GL_ControlCombustible oGL_ControlCombustible = new GL_ControlCombustible
            {
                ControlCombustibleID = 0,
                Combustible = pControlCombustibleViewModel.Combustible,
                Comentarios = pControlCombustibleViewModel.Comentarios,
                FechaHora = pControlCombustibleViewModel.FechaHora,
                Horometro = pControlCombustibleViewModel.Horometro,
                Horometro2 = pControlCombustibleViewModel.Horometro2,
                LanchaID = pControlCombustibleViewModel.LanchaID,
                LanchaProveedoraID = pControlCombustibleViewModel.LanchaProveedoraID,
                NroFactura = pControlCombustibleViewModel.NroFactura,
                ProveedorGrifo = pControlCombustibleViewModel.ProveedorGrifo,
                Tipo = pControlCombustibleViewModel.Tipo,
                TipoAbastecimiento = pControlCombustibleViewModel.TipoAbastecimiento,
                AbastLanchaCtrlCombID = pControlCombustibleViewModel.AbastLanchaCtrlCombID,
                FechaCreacion = pControlCombustibleViewModel.FechaHora,
                FechaModificacion = pControlCombustibleViewModel.FechaHora,
                UsuarioCreacion = pControlCombustibleViewModel.Usuario,
                UsuarioModificacion = pControlCombustibleViewModel.Usuario
            };
            oRepositorio.Add(oGL_ControlCombustible);
            if (!pIsTransaction)
            {
                oUnitOfWork.SaveChanges();
            }


            pControlCombustibleViewModel.ControlCombustibleID = oGL_ControlCombustible.ControlCombustibleID;
        }
    }
}
