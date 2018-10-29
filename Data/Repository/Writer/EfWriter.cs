using System.Collections.Generic;
using System.Data.Entity;

namespace Data.Repository
{
    public class EfWriter<TEntity> : IWriter<TEntity>
        where TEntity : class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> entities;

        public EfWriter(DbContext context)
        {
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

        public void Remove(int entityID)
        {
            TEntity entity = entities.Find(entityID);
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
