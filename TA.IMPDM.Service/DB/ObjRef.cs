using System;
using System.Collections.Generic;

namespace TA.IMPDM.Service.DB
{
    public class ObjRef
    {
        public long Id { get; set; }
        public long SrcPacketId { get; set; }
        public long OwnerId { get; set; }
        public string ParentObjectId { get; set; }
        public string ParentType { get; set; }
        public DateTime InsertTime { get; set; }
        public string RefObjectId { get; set; }
    }
}
