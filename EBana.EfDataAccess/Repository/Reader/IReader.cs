﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EBana.EfDataAccess.Repository
{
    public interface IReader<TEntity>
    {
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
    }
}
