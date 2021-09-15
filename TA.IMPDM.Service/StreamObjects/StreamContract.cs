using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service.StreamObjects
{
    public class StreamContract : IStreamObject
    {
        public long Id { get; set; }
        public string HidStr { get; set; }
        public string TimeModified { get; set; }
        public string Name { get; set; }
        public string CustomerCode { get; set; }
        public string Number { get; set; }
        public string ContractDate { get; set; }
        public string Gip { get; set; }
    }
}
