using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        private readonly IDatabaseContext _context;

        public Repository(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<TResult> SingleAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
            where TEntity : EntityBase, new()
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (predicate == null) throw new ArgumentNullException(nameof(selector));

            return _context.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .Select(selector)
                .SingleOrDefaultAsync();
        }

        public Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : EntityBase, new()
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return _context.Set<TEntity>()
                .AsNoTracking()
                .SingleOrDefaultAsync(predicate);
        }

        public Task<List<TEntity>> ListAsync<TEntity>() where TEntity : EntityBase, new()
        {
            return _context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<TEntity>> ListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase, new()
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return _context.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public Task<List<TResult>> ListAsync<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector) where TEntity : EntityBase, new()
        {
            return _context.Set<TEntity>()
               .AsNoTracking().Select(selector)
               .ToListAsync();
        }

        public Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : EntityBase, new()
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            async Task<TEntity> InsertAsync()
            {
                var now = DateTime.Now;
                entity.CreateDate = now;
                entity.MutationDate = now;

                var entry = await _context.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return entry.Entity;
            }

            return InsertAsync();
        }

        public Task<List<TEntity>> InsertRangeAsync<TEntity>(List<TEntity> entities) where TEntity : EntityBase, new()
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            if (entities.Count == 0) throw new InvalidOperationException(nameof(entities));

            async Task<List<TEntity>> InsertAsync()
            {
                var now = DateTime.Now;
                foreach (var entity in entities)
                {
                    entity.CreateDate = now;
                    entity.MutationDate = now;
                }

                await _context.Set<TEntity>().AddRangeAsync(entities).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return entities;
            }

            return InsertAsync();
        }
    }
}