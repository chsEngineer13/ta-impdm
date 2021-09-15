﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service.StreamObjects
{
    public class StreamConstruction : IStreamObject
    {
        public long Id { get; set; }
        public string HidStr { get; set; }
        public string TimeModified { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Gip { get; set; }
    }
}
