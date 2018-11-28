﻿using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IDisposable
    {
        #region Fields
        private readonly ApplicationDbContext _context;

        private UserRepository _userRepository;
        private PermissionRepository _permissionRepository;
        private RoleRepository _roleRepository;
        private RolePermissionRepository _rolePermissionRepository;

        private InvestigatorRepository _investigatorRepository;

        private InstitutionRepository _institutionRepository;
        private ProgramRepository _programRepository;
        private InvestigationGroupRepository _investigationGroupRepository;
        private InterestAreaRepository _interestAreaRepository;

        private CommissionRepository _commissionRepository;

        #endregion

        #region Constructors
        public UnitOfWork(string nameOrConnectionString)
        {
            _context = new ApplicationDbContext(nameOrConnectionString);
        }
        #endregion

        #region IUnitOfWork Members

        public CommissionRepository CommissionRepository
        {
            get { return _commissionRepository ?? (_commissionRepository = new CommissionRepository(_context)); }

        }
        public InstitutionRepository InstitutionRepository
        {
            get { return _institutionRepository ?? (_institutionRepository = new InstitutionRepository(_context)); }

        }

        public ProgramRepository ProgramRepository
        {
            get { return _programRepository ?? (_programRepository = new ProgramRepository(_context)); }

        }

        public InvestigationGroupRepository InvestigationGroupRepository
        {
            get { return _investigationGroupRepository ?? (_investigationGroupRepository = new InvestigationGroupRepository(_context)); }

        }

        public InvestigatorRepository InvestigatorRepository
        {
            get { return _investigatorRepository ?? (_investigatorRepository = new InvestigatorRepository(_context)); }

        }

        public InterestAreaRepository InterestAreaRepository
        {
            get { return _interestAreaRepository ?? (_interestAreaRepository = new InterestAreaRepository(_context)); }

        }

        public RolePermissionRepository RolePermissionRepository
        {
            get { return _rolePermissionRepository ?? (_rolePermissionRepository = new RolePermissionRepository(_context)); }
        }
        public UserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }
        public PermissionRepository PermissionRepository
        {
            get { return _permissionRepository ?? (_permissionRepository = new PermissionRepository(_context)); }
        }
        public RoleRepository RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(_context)); }
        }

        public int SaveChanges()
        {
            int intResultado = 0;
            try
            {

                intResultado = _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return intResultado;
        }



        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            this._userRepository = null;
            this._permissionRepository = null;
            this._roleRepository = null;

            this._rolePermissionRepository = null;

            this._investigatorRepository = null;

            this._institutionRepository = null;
            this._interestAreaRepository = null;
            this._programRepository = null;
            this._investigationGroupRepository = null;

            this._commissionRepository = null;

            _context.Dispose();
        }
        #endregion
    }
}
