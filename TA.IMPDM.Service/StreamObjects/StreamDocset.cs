using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service.StreamObjects
{
    public class StreamDocset : IStreamObject
    {
        public long Id { get; set; }
        public string HidStr { get; set; }
        public string TimeModified { get; set; }
        public string HParentType { get; set; }
        public string HPidStr { get; set; }
        public string Name { get; set; }
        public string Cipher { get; set; }
        public string DevDep { get; set; }
        public string OipKs { get; set; }
        public string CustomerCode { get; set; }
        public string ContractNumber { get; set; }
        public string CipherStage { get; set; }
        public string Developer { get; set; }
        public string ConstrPartCode { get; set; }
        public string ConstrPartNumber { get; set; }
        public string BuildingCode { get; set; }
        public string BuildingNumber { get; set; }
        public string Mark { get; set; }
        public string MarkPath { get; set; }
        //public string CipherDoc { get; set; }
        public string Stage { get; set; }
        public string ContractStage { get; set; }
        public string Changeset { get; set; }
        public string Status { get; set; }
        public string Gip { get; set; }
    }
}
