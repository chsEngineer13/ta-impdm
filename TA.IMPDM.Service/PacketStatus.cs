using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service
{
    public enum PacketStatus : long
    {
        Initial = 0,
        Processing = 10,
        Aborted = 999,
        Final = 1000,

        ProcessRead = 1010,
        AbortRead = 1999,
        FinalRead = 2000,
    }
}
