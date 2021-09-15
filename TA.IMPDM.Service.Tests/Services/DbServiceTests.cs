using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB;
using TA.IMPDM.Service.Services;
using Xunit;

namespace TA.IMPDM.Service.Tests.Services
{
    public class DbServiceTests
    {
        private static DbSet<T> GetDbSet<T>(IQueryable<T> queryable) where T : class
        {
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            //dbSet.Setup(x => x.Add(It.IsAny<T>())).Callback<T>(s => list.Add(s));
            return dbSet.Object;
        }

        private Integra2Context GetDbContext(Action<Mock<Integra2Context>> configure)
        {
            var mock = new Mock<Integra2Context>();
            configure(mock);
            return mock.Object;
        }

        private IDbService GetDbService(Integra2Context dbContext)
        {
            return new DbService(dbContext, new DbServiceOptions((сontext, filter) => null));
        }

        [Fact]
        public async Task GetPacketsWithFinalStatusAsync_Return_AllPacketsWithFinalStatus()
        {
            var packetList = new AsyncEnumerable<Packet>(new List<Packet>
            {
                new Packet() { StatusId = (long)PacketStatus.Final },
                new Packet() { StatusId = (long)PacketStatus.Aborted },
                new Packet() { StatusId = (long)PacketStatus.Initial },
                new Packet() { StatusId = (long)PacketStatus.Final }
            });
            var dbContext = GetDbContext(mock =>
            {
                mock.Setup(x => x.Packets)
                    .Returns(() => GetDbSet<Packet>(packetList));
            });
            var service = GetDbService(dbContext);

            var result = await service.GetPacketsWithFinalStatusAsync(CancellationToken.None);

            Assert.True(result.All(x => x.StatusId == (long)PacketStatus.Final));
        }
    }
}
