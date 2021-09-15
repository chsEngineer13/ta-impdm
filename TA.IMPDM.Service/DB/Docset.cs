using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB.Interfaces;
using TA.IMPDM.Service.Visitors;

namespace TA.IMPDM.Service.DB
{
    public class Docset : IVisitable, ISrcPacket
    {
        public long Id { get; set; }
        public long SrcPacketId { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public DateTime ObjectTime { get; set; }
        public string ParentObjectId { get; set; }
        public string ParentType { get; set; }
        public DateTime InsertTime { get; set; }
        public string Name { get; set; }
        public string Cipher { get; set; }
        public string DevDep { get; set; }
        public string OipKs { get; set; }
        public string Ccode { get; set; }
        public string Num { get; set; }
        public string Pstage { get; set; }
        public string DevOrg { get; set; }
        public string Cpcode { get; set; }
        public string Cpnum { get; set; }
        public string Bcode { get; set; }
        public string Bnum { get; set; }
        public string Mark { get; set; }
        public string MarkPath { get; set; }
        public string CipherDoc { get; set; }
        public string Bstage { get; set; }
        public string Cstage { get; set; }
        public int IzmNum { get; set; }
        public string Gip { get; set; }
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
