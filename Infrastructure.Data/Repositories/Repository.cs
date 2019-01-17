using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class Repository<TEntity> where TEntity : class
    {
        private ApplicationDbContext _context;

        internal ApplicationDbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
        private DbSet<TEntity> _set;

        internal Repository(ApplicationDbContext context)
        {
            _context = context;

        }

        protected DbSet<TEntity> Set
        {
            get { return _set ?? (_set = _context.Set<TEntity>()); }
        }

        public List<TEntity> GetAll()
        {
            return Set.ToList();
        }

        public TEntity FindById(object id)
        {
            return Set.Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            return Set.Add(entity);
        }
        public void Update(TEntity obj, params Expression<Func<TEntity, object>>[] propertiesToUpdate)
        {
            _context.Set<TEntity>().Attach(obj);

            foreach (var p in propertiesToUpdate)
            {
                _context.Entry(obj).Property(p).IsModified = true;
            }
        }
        public void Update(TEntity entity)
        {

            /* if (!entity.ToString().Equals("Infrastructure.Data.GL_ControlCombustible"))
                {*/
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
                entry = _context.Entry(entity);
            }
            entry.State = EntityState.Modified;
            /*  }
                else {
                    var entry = _context.Entry(entity);
                    entry.CurrentValues.SetValues(entity);


                }*/



        }

        public void Delete(TEntity entity)
        {
            Set.Attach(entity);
            Set.Remove(entity);
        }
    }
    
}
