using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TA.IMPDM.Service.StreamObjects;

namespace TA.IMPDM.Service.Services
{
    public class RetryODataService : IODataService
    {
        private readonly IODataService innerService;
        private readonly ILogger logger;
        private readonly RetryODataServiceOptions options;
        private int RetryCount => options.RetryCount;
        private TimeSpan Delay => options.Delay;

        public RetryODataService(IODataService innerService, ILogger logger, RetryODataServiceOptions options)
        {
            this.innerService = innerService ?? throw new ArgumentNullException(nameof(innerService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<Result> SendAsync<T>(T streamObject, System.Threading.CancellationToken token) where T : IStreamObject
        {
            int retryIndex = 0;
            Result result;
            bool retry = false;
            do
            {
                retryIndex++;
                result = await innerService
                    .SendAsync(streamObject, token)
                    .ConfigureAwait(false);
                if (result.ErrorMessageCode == Result.ErrorCode.HttpCanRetry && retryIndex < RetryCount)
                {
                    logger.LogInformation($"Ошибка при отправке данных, повтор через {Delay.TotalSeconds} с.");
                    await Task.Delay(Delay, token).ConfigureAwait(false);
                    retry = true;
                }
                else if (result.ErrorMessageCode == Result.ErrorCode.HttpCanRetry)
                {
                    result = Result.Error(Result.ErrorCode.HttpFail, result.ErrorMessage);
                    retry = false;
                }
                else
                {
                    retry = false;
                }
            }
            while (retry && retryIndex < RetryCount);

            return result;
        }
    }
}
