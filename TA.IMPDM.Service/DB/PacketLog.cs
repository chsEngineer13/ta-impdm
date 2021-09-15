using System;
using System.Collections.Generic;

namespace TA.IMPDM.Service.DB
{
    public class PacketLog
    {
        public long Id { get; set; }
        public long PacketId { get; set; }
        public long StatusId { get; set; }
        public DateTime MsgTime { get; set; }
        public long MsgTypeId { get; set; }
        public string MsgText { get; set; }

        public Packet Packet { get; set; }
    }
}
