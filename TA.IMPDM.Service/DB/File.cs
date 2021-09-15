using System;
using System.Collections.Generic;

namespace TA.IMPDM.Service.DB
{
    public class File
    {
        public long Id { get; set; }
        public long SrcPacketId { get; set; }
        public long OwnerId { get; set; }
        public string ObjectType { get; set; }
        public string ParentObjectId { get; set; }
        public string ParentType { get; set; }
        public DateTime InsertTime { get; set; }
        public string FileName { get; set; }
        public long? FileSize { get; set; }
        public DateTime FileModifyTime { get; set; }
        public long? FileTypeId { get; set; }
    }
}
