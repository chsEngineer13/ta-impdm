using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB;
using TA.IMPDM.Service.DB.Interfaces;

namespace TA.IMPDM.Service.Services
{
    public class DbService : IDbService
    {
        private readonly Integra2Context dbContext;
        private readonly DbServiceOptions options;

        public DbService(Integra2Context dbContext, DbServiceOptions options)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task ChangePacketStatusAsync(long packetId, 
            PacketStatus newStatus, 
            string logMessage, 
            CancellationToken token = default(CancellationToken))
        {
            var packet = await dbContext
                .Packets
                .Include(x => x.PacketLog)
                .SingleAsync(x => x.Id == packetId, token)
                .ConfigureAwait(false);

            packet.StatusId = (long)newStatus;
            packet.PacketLog
                .Add(new PacketLog()
                {
                    PacketId = packetId,
                    StatusId = (long)newStatus,
                    MsgText = logMessage,
                    MsgTime = DateTime.Now,
                    MsgTypeId = 1
                });
            await dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Packet>> GetPacketsWithFinalStatusAsync(CancellationToken token)
        {
            return await dbContext
                .Packets
                .Where(x => x.StatusId == (long)PacketStatus.Final)
                .ToListAsync(token)
                .ConfigureAwait(false);
        }

        public IAsyncEnumerable<IVisitable> GetPacketPartsAsync(Packet packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            var tables = options.PacketPartsTables(dbContext, new DbServiceOptions.PacketPartsFilter(packet.Id));
            var list = new List<IAsyncEnumerator<IVisitable>>();
            foreach (var table in tables)
            {
                var items = table.ToAsyncEnumerable();
                list.Add(items.GetEnumerator());
            }
            return new AsyncEnumerable<IVisitable>(new CompositeAsyncEnumerator<IVisitable>(list.AsEnumerable()));
        }
    }
}
