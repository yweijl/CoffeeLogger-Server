using Core.Entities;
using Core.Enums;
using FluentAssertions;
using Infrastructure.Repositories;
using NUnit.Framework;
using Shared.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.UnitTests
{
    [TestFixture]
    public class RepositoryUnitTests
    {
        [Test]
        public async Task Repository_SingleAsync_Returns_Expected_Entity()
        {
            using var dbContext = new MockDatabase(
                new Coffee { Id = 1, BrandId = 1, CoffeeType = CoffeeType.Arabica, Country = "a" },
                new Coffee { Id = 2, BrandId = 2, CoffeeType = CoffeeType.Robusto, Country = "b" }
                );

            var repository = new Repository(dbContext);
            var result = await repository.SingleAsync<Coffee>(x => x.Id == 2).ConfigureAwait(false);
            result.Country.Should().Be("b");
        }

        [Test]
        public async Task Repository_SingleAsync_Throws_Exception()
        {
            using var dbContext = new MockDatabase();

            var repository = new Repository(dbContext);
            Func<Task> act = async () => { await repository.SingleAsync<Coffee>(null).ConfigureAwait(false); };
            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);

            Func<Task> act1 = async () => { await repository.SingleAsync<Coffee, int>(null, null).ConfigureAwait(false); };
            await act1.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Test]
        public async Task Repository_SingleAsync_Returns_Null_Entity_Does_Not_Exist()
        {
            using var dbContext = new MockDatabase(
                new Coffee { Id = 2, BrandId = 2, CoffeeType = CoffeeType.Robusto, Country = "b" }
                );

            var repository = new Repository(dbContext);
            var result = await repository.SingleAsync<Coffee>(x => x.Id == 1).ConfigureAwait(false);
            result.Should().BeNull();
        }

        [Test]
        public async Task Repository_SingleAsync_With_Selector_Tansforms_Corectly()
        {
            using var dbContext = new MockDatabase(
                new Coffee { Id = 1, BrandId = 1, CoffeeType = CoffeeType.Robusto, Country = "b" }
                );

            var repository = new Repository(dbContext);
            var result = await repository.SingleAsync<Coffee, (string, CoffeeType)>(x => x.Id == 1, y => new(y.Country, y.CoffeeType)).ConfigureAwait(false);
            result.Should().Be(("b", CoffeeType.Robusto));
        }

        [Test]
        public async Task Repository_ListAsync_Returns_Expected_Entities()
        {
            using var dbContext = new MockDatabase(
                new Coffee { Id = 1, BrandId = 1, CoffeeType = CoffeeType.Arabica, Country = "a" },
                new Coffee { Id = 2, BrandId = 2, CoffeeType = CoffeeType.Robusto, Country = "b" },
                new Brand { Id = 1, Name = "brand-1" },
                new Brand { Id = 2, Name = "brand-2" }
                );

            var repository = new Repository(dbContext);
            var result = await repository.ListAsync<Brand>().ConfigureAwait(false);
            result.Count.Should().Be(2);
            result.Should().BeOfType<List<Brand>>();
        }

        [Test]
        public async Task Repository_ListAsync_Returns_Empty_List()
        {
            using var dbContext = new MockDatabase(
                new Coffee { Id = 1, BrandId = 1, CoffeeType = CoffeeType.Arabica, Country = "a" },
                new Coffee { Id = 2, BrandId = 2, CoffeeType = CoffeeType.Robusto, Country = "b" },
                new Brand { Id = 1, Name = "brand-1" },
                new Brand { Id = 2, Name = "brand-2" }
                );

            var repository = new Repository(dbContext);
            var result = await repository.ListAsync<Flavor>().ConfigureAwait(false);
            result.Count.Should().Be(0);
            result.Should().BeOfType<List<Flavor>>();
        }

        [Test]
        public async Task Repository_ListAsync_Precicate_Returns_Expected_Entities()
        {
            using var dbContext = new MockDatabase(
                new Coffee { Id = 1, BrandId = 1, CoffeeType = CoffeeType.Arabica, Country = "a" },
                new Coffee { Id = 2, BrandId = 2, CoffeeType = CoffeeType.Robusto, Country = "b" },
                new Brand { Id = 1, Name = "brand-1" },
                new Brand { Id = 2, Name = "brand-2" }
                );

            var repository = new Repository(dbContext);
            var result = await repository.ListAsync<Brand>(x => x.Id == 2).ConfigureAwait(false);
            result.Count.Should().Be(1);
            result.First().Name.Should().Be("brand-2");
            result.Should().BeOfType<List<Brand>>();
        }

        [Test]
        public async Task Repository_ListAsync_Predicate_And_Selector_Transforms_As_Expected()
        {
            using var dbContext = new MockDatabase(
                new Coffee { Id = 1, BrandId = 1, CoffeeType = CoffeeType.Arabica, Country = "a" },
                new Coffee { Id = 2, BrandId = 2, CoffeeType = CoffeeType.Robusto, Country = "b" },
                new Coffee { Id = 3, BrandId = 2, CoffeeType = CoffeeType.Robusto, Country = "c" },
                new Brand { Id = 1, Name = "brand-1" },
                new Brand { Id = 2, Name = "brand-2" }
                );

            var repository = new Repository(dbContext);
            var result = await repository.ListAsync<Coffee, (long, string)>(x => x.BrandId == 2, y => new(y.BrandId, y.Country)).ConfigureAwait(false);
            result.Count.Should().Equals(new List<(long, string)> { (2, "b"), (2, "c") });
        }

        [Test]
        public async Task Repository_ListAsync_Selector_Transforms_As_Expected()
        {
            using var dbContext = new MockDatabase(
                new Coffee { Id = 1, BrandId = 1, CoffeeType = CoffeeType.Arabica, Country = "a" },
                new Coffee { Id = 2, BrandId = 2, CoffeeType = CoffeeType.Robusto, Country = "b" },
                new Coffee { Id = 3, BrandId = 2, CoffeeType = CoffeeType.Robusto, Country = "c" },
                new Brand { Id = 1, Name = "brand-1" },
                new Brand { Id = 2, Name = "brand-2" }
                );

            var repository = new Repository(dbContext);
            var result = await repository.ListAsync<Coffee, (long, string)>(y => new(y.BrandId, y.Country)).ConfigureAwait(false);
            result.Count.Should().Equals(new List<(long, string)> { (1, "a"), (2, "b"), (2, "c") });
        }

        [Test]
        public async Task Repository_ListAsync_Throws_Exception()
        {
            using var dbContext = new MockDatabase();

            var repository = new Repository(dbContext);
            Func<Task> act = async () => { await repository.ListAsync<Coffee>(null).ConfigureAwait(false); };
            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);

            Func<Task> act1 = async () => { await repository.ListAsync<Coffee, int>(null, null).ConfigureAwait(false); };
            await act1.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Test]
        public async Task Repository_InsertAsync_Works()
        {
            using var dbContext = new MockDatabase();

            var repository = new Repository(dbContext);
            var result = await repository.InsertAsync(new Flavor { Name = "v" }).ConfigureAwait(false);
            result.CreateDate.Date.Should().Be(DateTime.Today);
            result.MutationDate.Date.Should().Be(DateTime.Today);
        }

        [Test]
        public async Task Repository_InsertRangeAsync_Works()
        {
            using var dbContext = new MockDatabase();

            var repository = new Repository(dbContext);
            var result = await repository.InsertRangeAsync(new List<Flavor> { new Flavor { Name = "a" }, new Flavor { Name = "b" } }).ConfigureAwait(false);
            result.Select(x => x.Name).Should().Equal("a", "b");
        }

        [Test]
        public async Task Repository_InsertAsync_Empty_Entity_Throws_Exception()
        {
            using var dbContext = new MockDatabase();

            var repository = new Repository(dbContext);
            Func<Task> act = async () => { await repository.InsertAsync((Flavor)null); };
            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Test]
        public async Task Repository_InsertRangeAsync_Empty_And_Null_List_Throws_Exception()
        {
            using var dbContext = new MockDatabase();

            var repository = new Repository(dbContext);
            Func<Task> act = async () => { await repository.InsertRangeAsync((List<Flavor>)null); };
            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);

            Func<Task> act1 = async () => { await repository.InsertRangeAsync(new List<Flavor>()); };
            await act1.Should().ThrowAsync<InvalidOperationException>().ConfigureAwait(false);
        }

        [Test]
        public async Task Repository_UpdateAsync_Works()
        {
            var createDate = DateTime.Now.AddDays(-1);
            using var dbContext = new MockDatabase(
                new Coffee
                {
                    Id = 1,
                    BrandId = 1,
                    CoffeeType = CoffeeType.Robusto,
                    Country = "a",
                    Rating = 1,
                    CreateDate = createDate,
                    MutationDate = DateTime.Now.AddDays(-1)
                },
                new Coffee
                {
                    Id = 2,
                    BrandId = 2,
                    CoffeeType = CoffeeType.Arabica,
                    Country = "b",
                    Rating = 2,
                    CreateDate = DateTime.Now.AddDays(-2),
                    MutationDate = DateTime.Now.AddDays(-2)
                });

            var repository = new Repository(dbContext);

            var updatedEntity =
                await repository.Update<Coffee>(
                    x => x.Id == 1,
                    y => new Coffee
                    {
                        BrandId = 3,
                        Country = "c",
                    })
                .ConfigureAwait(false);

            updatedEntity.Should()
                .Equals(new Coffee
                {
                    Id = 2,
                    BrandId = 3,
                    Country = "c",
                    CoffeeType =
                    CoffeeType.Arabica,
                    CreateDate = createDate,
                    Rating = 1
                });

            updatedEntity.MutationDate.Date.Should().Be(DateTime.Today);
        }

        [Test]
        public async Task Repository_UpdateAsync_Throws_Exception()
        {
            var createDate = DateTime.Now.AddDays(-1);
            using var dbContext = new MockDatabase();

            var repository = new Repository(dbContext);

            Func<Task> act = async () =>
            {
                await repository.Update<Coffee>(
                    null, y => new Coffee
                    {
                        BrandId = 3,
                        Country = "c"
                    }).ConfigureAwait(false);
            };

            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);

            Func<Task> act1 = async () =>
            {
                await repository
                .Update<Coffee>(null, null)
                .ConfigureAwait(false);
            };

            await act1.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);

            Func<Task> act3 = async () =>
            {
                await repository
                .Update<Coffee>(x => x.Id == 1, null)
                .ConfigureAwait(false);
            };

            await act3.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }
    }
}