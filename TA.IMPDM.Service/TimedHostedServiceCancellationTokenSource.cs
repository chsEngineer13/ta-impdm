using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TA.IMPDM.Service
{
    public class TimedHostedServiceCancellationTokenSource
    {
        private readonly CancellationTokenSource cts;

        public CancellationToken Token => cts.Token;

        public TimedHostedServiceCancellationTokenSource(CancellationTokenSource cts = null)
        {
            this.cts = cts ?? new CancellationTokenSource();
        }

        public void Cancel()
        {
            cts.Cancel();
        }
    }
}
