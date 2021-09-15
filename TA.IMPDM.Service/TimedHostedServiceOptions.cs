using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service
{
    public class TimedHostedServiceOptions
    {
        public class SVCMOptions
        {
            public enum SVCMAuth { None, Basic };
            public string Host { get; set; }
            public SVCMAuth Auth { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
        }

        private TimeSpan _interval;
        private TimeSpan _httpClientTimeout;
        private TimeSpan _httpClientRetryInterval;
        private int _httpClientRetryCount;

        public long Interval { get { return (long)_interval.TotalSeconds; } set { _interval = TimeSpan.FromSeconds(value); } }
        public TimeSpan IntervalTimeSpan => _interval;

        public long HttpClientTimeout {
            get { return (long)_httpClientTimeout.TotalSeconds; }
            set { _httpClientTimeout = TimeSpan.FromSeconds(value); }
        }
        public TimeSpan HttpClientTimeoutTimeSpan => _httpClientTimeout;

        public long HttpClientRetryInterval
        {
            get { return (long)_httpClientRetryInterval.TotalSeconds; }
            set { _httpClientRetryInterval = TimeSpan.FromSeconds(value); }
        }

        public TimeSpan HttpClientRetryIntervalTimeSpan => _httpClientRetryInterval;

        public int HttpClientRetryCount {
            get => _httpClientRetryCount >= 0
                ? _httpClientRetryCount
                : 0;
            set => _httpClientRetryCount = value;
        }

        public SVCMOptions SVCM { get; set; }
        public string Provider { get; set; } = "Postgres";
    }
}
