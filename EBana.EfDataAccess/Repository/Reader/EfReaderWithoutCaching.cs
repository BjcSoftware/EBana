using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EBana.EfDataAccess.Repository
{
    public class EfReaderWithoutCaching<TEntity> : IReader<TEntity>
        where TEntity : class
    {
        private readonly EfReader<TEntity> decoratedReader;

        public EfReaderWithoutCaching(EfReader<TEntity> decoratedReader)
        {
            if (decoratedReader == null)
                throw new ArgumentNullException(nameof(decoratedReader));

            this.decoratedReader = decoratedReader;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return decoratedReader.Find(predicate).AsQueryable().AsNoTracking();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return decoratedReader.GetAll().AsQueryable().AsNoTracking();
        }
    }
}
