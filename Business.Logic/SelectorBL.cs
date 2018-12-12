using Domain.Entities;
using Infrastructure.Data;

using Infrastructure.Data.Repositories;
using System.Collections.Generic;
using System.Configuration;

namespace Business.Logic
{
    public class SelectorBL
    {
        private static UserRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public SelectorBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.UserRepository;
        }
        public List<SelectOptionItem> ProgramsSelector(int educational_institution_id)
        {
            return oRepositorio.ProgramsSelector(educational_institution_id);
        }

        public List<SelectOptionItem> ProgramsSelector()
        {
            return oRepositorio.ProgramsSelector();
        }


        public List<SelectOptionItem> EducationLevelsSelector(int educational_institution_id, int program_id)
        {
            return oRepositorio.EducationLevelsSelector(educational_institution_id, program_id);
        }

        public List<SelectOptionItem> EducationalInstitutionsSelector()
        {
            return oRepositorio.EducationalInstitutionsSelector();
        }
        public List<SelectOptionItem> ReasonRejectsSelector()
        {
            return oRepositorio.ReasonRejectsSelector();
        }
        public List<SelectOptionItem> TagsSelector()
        {
            return oRepositorio.TagsSelector();
        }
        public List<SelectOptionItem> DepartmentsSelector()
        {
            return oRepositorio.DepartmentsSelector();
        }
        public List<SelectOptionItem> AcademicLevelsSelector()
        {
            return oRepositorio.AcademicLevelsSelector();
        }
        public List<SelectOptionItem> CommissionsSelector()
        {
            return oRepositorio.CommissionsSelector();
        }

        public List<SelectOptionItem> MunicipalitiesSelector(int department_id)
        {
            return oRepositorio.MunicipalitiesSelector(department_id);
        }
        public List<SelectOptionItem> InterestAreasSelector()
        {
            return oRepositorio.InterestAreasSelector();
        }

        public List<SelectOptionItem> InvestigationGroupsSelector(int institution_id)
        {
            return oRepositorio.InvestigationGroupsSelector(institution_id);
        }
        

        public List<SelectOptionItem> GendersSelector()
        {
            return oRepositorio.GendersSelector();
        }
        public List<SelectOptionItem> RolesSelector()
        {
            return oRepositorio.RolesSelector();
        }

        public List<SelectOptionItem> EstatusUserSelector()
        {
            return oRepositorio.EstatusUserSelector();
        }

        public List<SelectOptionItem> DocumentTypesSelector()
        {
            return oRepositorio.DocumentTypesSelector();
        }


        public List<SelectOptionItem> NationalitiesSelector()
        {
            return oRepositorio.NationalitiesSelector();
        }
    }
}
