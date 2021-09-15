using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service
{
    public class RetryODataServiceOptions
    {
        public int RetryCount { get; }
        public TimeSpan Delay { get; }

        public RetryODataServiceOptions(int retryCount, TimeSpan delay)
        {
            RetryCount = retryCount;
            Delay = delay;
        }
    }
}
