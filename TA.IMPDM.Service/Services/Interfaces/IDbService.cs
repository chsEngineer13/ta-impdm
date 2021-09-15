using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB;
using TA.IMPDM.Service.DB.Interfaces;

namespace TA.IMPDM.Service.Services
{
    public interface IDbService
    {
        Task ChangePacketStatusAsync(long packetId, PacketStatus newStatus, string logMessage, CancellationToken token = default(CancellationToken));

        Task<IEnumerable<Packet>> GetPacketsWithFinalStatusAsync(CancellationToken token);
        IAsyncEnumerable<IVisitable> GetPacketPartsAsync(DB.Packet packet);
    }
}
