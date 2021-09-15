using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB;

namespace TA.IMPDM.Service
{
    public class TimedHostedService : BackgroundService, IDisposable
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly TimedHostedServiceCancellationTokenSource hostedToken;
        private readonly TimedHostedServiceOptions options;

        public TimedHostedService(IServiceScopeFactory scopeFactory, 
            IOptionsMonitor<TimedHostedServiceOptions> options,
            TimedHostedServiceCancellationTokenSource hostedToken)
        {
            this.scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            this.hostedToken = hostedToken ?? throw new ArgumentNullException(nameof(hostedToken));
            this.options = options?.CurrentValue ?? throw new ArgumentNullException(nameof(options));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            IServiceScope scope = null;
            ILogger logger = null;
            try
            {
                scope = scopeFactory.CreateScope();
                logger = scope.ServiceProvider.GetRequiredService<ILogger<TimedHostedService>>();
                var packetService = scope.ServiceProvider.GetRequiredService<ProcessPacketsService>();

                logger.LogDebug($"Service interval: {options.Interval}");

                while (!stoppingToken.IsCancellationRequested)
                {
                    await DoWorkAsync(packetService, logger, stoppingToken).ConfigureAwait(false);
                    await Task.Delay(options.IntervalTimeSpan, stoppingToken).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Операция была отменена");
            }
            catch (Exception ex)
            {
                logger?.LogCritical(ex.Message);
                // завершаем работу
                hostedToken.Cancel();
            }
            finally
            {
                scope?.Dispose();
            }
        }

        private async Task DoWorkAsync(ProcessPacketsService packetService, 
            ILogger logger, 
            CancellationToken token)
        {
            logger.LogDebug($"DoWork(): begin at {DateTime.Now}");
            await packetService.ProcessNewPacketsAsync(token).ConfigureAwait(false);
            logger.LogDebug($"DoWork(): end at {DateTime.Now}");
        }
    }
}
