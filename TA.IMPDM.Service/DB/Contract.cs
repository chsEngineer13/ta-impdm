using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB.Interfaces;
using TA.IMPDM.Service.Visitors;

namespace TA.IMPDM.Service.DB
{
    public class Contract : IVisitable, ISrcPacket
    {
        public long Id { get; set; }
        public long SrcPacketId { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public DateTime ObjectTime { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime? Cdate { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Dev { get; set; }
        public string InnerNumber { get; set; }
        public string OipKs { get; set; }
        public string Ccode { get; set; }
        public string Num { get; set; }
        public string Gip { get; set; }

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
