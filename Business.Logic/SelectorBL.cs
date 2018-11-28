﻿using Domain.Entities;
using Infrastructure.Data;

using Infrastructure.Data.Repositories;
using System.Collections.Generic;
using System.Configuration;

namespace Business.Logic
{
    public class SelectorBL
    {
        private static UserRepository  oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public SelectorBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.UserRepository;
        }
        public List<SelectOptionItem> InterestAreasSelector(int investigation_group_id)
        {
            return oRepositorio.InterestAreasSelector(investigation_group_id);
        }
        
        public List<SelectOptionItem> InvestigationGroupsSelector(int institution_id)
        {
            return oRepositorio.InvestigationGroupsSelector(institution_id);
        }
        public List<SelectOptionItem> ProgramsSelector()
        {
            return oRepositorio.ProgramsSelector();
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