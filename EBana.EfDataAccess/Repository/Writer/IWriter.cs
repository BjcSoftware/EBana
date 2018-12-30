using System.Collections.Generic;

namespace EBana.EfDataAccess.Repository
{
    public interface IWriter<TEntity>
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(int entityID);
        void RemoveAll();
        void Save();
    }
}
