using System;
using System.Collections.Generic;

namespace TA.IMPDM.Service.DB
{
    public class Invoice
    {
        public long Id { get; set; }
        public long SrcPacketId { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public DateTime ObjectTime { get; set; }
        public DateTime InsertTime { get; set; }
        public string InvoiceNum { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Recipient { get; set; }
        public string Status { get; set; }
    }
}
