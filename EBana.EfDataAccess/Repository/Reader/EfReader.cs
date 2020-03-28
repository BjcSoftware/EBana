using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EBana.EfDataAccess.Repository
{
    public class EfReader<TEntity> : IReader<TEntity>
        where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> Entities;

        public EfReader(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Context = context;
            Entities = Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.AsEnumerable().Where(predicate.Compile());
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities;
        }
    }
}
