using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IRepository
    {
        Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : EntityBase, new();
        Task<List<TEntity>> InsertRangeAsync<TEntity>(List<TEntity> entities) where TEntity : EntityBase, new();
        Task<List<TEntity>> ListAsync<TEntity>() where TEntity : EntityBase, new();
        Task<List<TResult>> ListAsync<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector) where TEntity : EntityBase, new();
        Task<List<TEntity>> ListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase, new();
        Task<List<TResult>> ListAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector) where TEntity : EntityBase, new();
        Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase, new();
        Task<TResult> SingleAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector) where TEntity : EntityBase, new();
        Task<TEntity> Update<TEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression) where TEntity : EntityBase, new();
    }
}