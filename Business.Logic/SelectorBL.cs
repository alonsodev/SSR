﻿using Domain.Entities;
using Infrastructure.Data;

using Infrastructure.Data.Repositories;
using System;
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

        public List<SelectOptionItem> ConsultationTypesSelector()
        {
            return oRepositorio.ConsultationTypesSelector();
        }

        public List<SelectOptionItem> PeriodsSelector()
        {
            return oRepositorio.PeriodsSelector();
        }

        public List<SelectOptionItem> EducationLevelsSelector()
        {
            return oRepositorio.EducationLevelsSelector();
        }
        public List<SelectOptionItem> EducationLevelsSelector(int educational_institution_id, int program_id)
        {
            return oRepositorio.EducationLevelsSelector(educational_institution_id, program_id);
        }
        public List<SelectOptionItem> EducationalInstitutionsSelector()
        {
            return oRepositorio.EducationalInstitutionsSelector();
        }

        public List<SelectOptionItem> KnowledgeAreasSelector()
        {
            return oRepositorio.KnowledgeAreasSelector();
        }

        public List<SelectOptionItem> StatusSelector()
        {
            return oRepositorio.StatusSelector();
        }

        public List<SelectOptionItem> ReasonRejectsSelector()
        {
            return oRepositorio.ReasonRejectsSelector();
        }

        public List<SelectOptionItem> InstitutionsSelector(List<int> institution_ids)
        {
            return oRepositorio.InstitutionsSelector(institution_ids);
        }

        public List<SelectOptionItem> OriginSelector()
        {
            return oRepositorio.OriginSelector();
        }

        public List<SelectOptionItem> TagsSelector()
        {
            return oRepositorio.TagsSelector();
        }
        public List<SelectOptionItem> DebateSpeakersSelector()
        {
            return oRepositorio.DebateSpeakersSelector();
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
