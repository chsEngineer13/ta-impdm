using System;
using System.Collections.Generic;

namespace TA.IMPDM.Service.DB
{
    public class Packet
    {
        public Packet()
        {
            PacketLog = new HashSet<PacketLog>();
        }

        public long Id { get; set; }
        public string FileName { get; set; }
        public string FileSha256 { get; set; }
        public string FileContent { get; set; }
        public DateTime FileModificationTime { get; set; }
        public long FileSize { get; set; }
        public long StatusId { get; set; }
        public DateTime StatusTime { get; set; }

        public ICollection<PacketLog> PacketLog { get; set; }
    }
}
