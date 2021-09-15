using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TA.IMPDM.Service
{
    public class ODataClientOptions
    {
        public CancellationToken Token { get; }

        public ODataClientOptions(CancellationToken token)
        {
            Token = token;
        }
    }
}
