using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TA.IMPDM.Service.DB;
using TA.IMPDM.Service.DB.Interfaces;

namespace TA.IMPDM.Service
{
    public class DbServiceOptions
    {
        public class PacketPartsFilter
        {
            public long SrcPacketId { get; }

            public PacketPartsFilter(long srcPacketId)
            {
                SrcPacketId = srcPacketId;
            }
        }

        public Func<Integra2Context, PacketPartsFilter, IReadOnlyList<IQueryable<IVisitable>>> PacketPartsTables { get; }

        public DbServiceOptions(Func<Integra2Context, PacketPartsFilter, IReadOnlyList<IQueryable<IVisitable>>> packetPartsTables)
        {
            PacketPartsTables = packetPartsTables ?? throw new ArgumentNullException(nameof(packetPartsTables));
        }
    }
}
