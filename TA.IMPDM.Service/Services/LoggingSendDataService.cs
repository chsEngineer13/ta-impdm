using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB.Interfaces;

namespace TA.IMPDM.Service.Services
{
    class LoggingSendDataService : ISendDataService
    {
        private readonly ISendDataService child;
        private readonly ILogger logger;

        public LoggingSendDataService(ISendDataService child, ILogger logger)
        {
            this.child = child ?? throw new ArgumentNullException(nameof(child));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> SendPacketPartAsync(IVisitable visitable, CancellationToken token)
        {
            try
            {
                var result = await child.SendPacketPartAsync(visitable, token).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message);
                return Result.Error(Result.ErrorCode.Exception, ex.Message);
            }
        }
    }
}
