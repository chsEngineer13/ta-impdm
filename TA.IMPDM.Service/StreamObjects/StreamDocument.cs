using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service.StreamObjects
{
    public class StreamDocument : IStreamObject
    {
        public long Id { get; set; }
        public string HidStr { get; set; }
        public string TimeModified { get; set; }
        public string HPidStr { get; set; }
        public string HParentType { get; set; }
        public string Name { get; set; }
        public string DevDep { get; set; }
        public int IzmNum { get; set; }
        public string HStatusStr { get; set; }
    }
}
