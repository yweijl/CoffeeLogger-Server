using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository
    {
        Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : EntityBase, new();
        Task<List<TEntity>> InsertRangeAsync<TEntity>(List<TEntity> entities) where TEntity : EntityBase, new();
        List<TEntity> List<TEntity>() where TEntity : EntityBase, new();
        List<TEntity> List<TEntity>(Func<TEntity, bool> predicate) where TEntity : EntityBase, new();
        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase, new();
        TResult Single<TEntity, TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector) where TEntity : EntityBase, new();
    }
}