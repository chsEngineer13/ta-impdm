using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB.Interfaces;
using TA.IMPDM.Service.Visitors;

namespace TA.IMPDM.Service.DB
{
    public class Document : IVisitable, ISrcPacket
    {
        public long Id { get; set; }
        public long SrcPacketId { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public DateTime ObjectTime { get; set; }
        public string ParentObjectId { get; set; }
        public string ParentType { get; set; }
        public DateTime InsertTime { get; set; }
        public string Cipher { get; set; }
        public string CipherDoc { get; set; }
        public string Name { get; set; }
        public string DevDep { get; set; }
        public int IzmNum { get; set; }
        public string Status { get; set; }
        public bool? IsActual { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Task Accept(IAsyncVisitor visitor)
        {
            return visitor.VisitAsync(this);
        }
    }
}
