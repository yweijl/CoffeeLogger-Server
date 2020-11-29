using Core.Entities;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.UnitTests
{
    public class MockDatabase : IDatabaseContext, IDisposable
    {
        private readonly DatabaseContext _context;

        public MockDatabase(params EntityBase[] entities)
        {
            var options =
                new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDb").Options;

            _context = new DatabaseContext(options);

            foreach (var entity in entities)
            {
                _context.Add(GetEntity(entity));
            }

            _context.SaveChanges();
        }

        private static EntityBase GetEntity(EntityBase entity)
        {
            if (entity is Brand) return entity as Brand;
            if (entity is Coffee) return entity as Coffee;
            if (entity is Record) return entity as Record;
            if (entity is Flavor) return entity as Flavor;
            if (entity is RecordFlavor) return entity as RecordFlavor;

            throw new ArgumentOutOfRangeException(nameof(entity));
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           return _context.SaveChangesAsync(cancellationToken);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
    