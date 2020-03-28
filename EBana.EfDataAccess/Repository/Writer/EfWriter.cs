using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EBana.EfDataAccess.Repository
{
    public class EfWriter<TEntity> : IWriter<TEntity>
        where TEntity : class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> entities;

        public EfWriter(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            this.context = context;
            entities = this.context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entitiesToAdd)
        {
            entities.AddRange(entitiesToAdd);
        }

        public void Remove(int entityId)
        {
            TEntity entity = entities.Find(entityId);
            entities.Remove(entity);
        }

        public void RemoveAll()
        {
            entities.RemoveRange(entities);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
