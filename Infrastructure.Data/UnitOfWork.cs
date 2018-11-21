using Infrastructure.Data.Repositories;
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
        #endregion

        #region Constructors
        public UnitOfWork(string nameOrConnectionString)
        {
            _context = new ApplicationDbContext(nameOrConnectionString);
        }
        #endregion

        #region IUnitOfWork Members
        public UserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
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
            _context.Dispose();
        }
        #endregion
    }
}
