using Core.Entities;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

            _context.AddRange(entities);

            _context.SaveChanges();
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
    