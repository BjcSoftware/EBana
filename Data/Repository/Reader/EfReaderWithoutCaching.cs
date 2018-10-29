using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repository
{
    public class EfReaderWithoutCaching<TEntity> : IReader<TEntity>
        where TEntity : class
    {
        private readonly EfReader<TEntity> decoratedReader;

        public EfReaderWithoutCaching(EfReader<TEntity> decoratedReader)
        {
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
