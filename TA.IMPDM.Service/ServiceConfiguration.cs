using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB;
using TA.IMPDM.Service.DB.Interfaces;
using TA.IMPDM.Service.OData;
using TA.IMPDM.Service.Services;

namespace TA.IMPDM.Service
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(HostBuilderContext context, 
            IServiceCollection services, 
            TimedHostedServiceCancellationTokenSource hostedServiceCancellationTokenSource)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddSingleton(hostedServiceCancellationTokenSource);

            // mapper
            services.AddSingleton<Mapster.IAdapter>(sp => {
                var typeConfig = new Mapster.TypeAdapterConfig();
                MapperConfig.ConfigureMapper(typeConfig);
                return new Mapster.Adapter(typeConfig);
            });

            // hosted service options
            services.Configure<TimedHostedServiceOptions>(context.Configuration);

            // options for db service
            // Таблицы связанные с Packet
            services.AddSingleton<DbServiceOptions>(new DbServiceOptions((dbContext, filter) =>
                // sic! порядок таблиц важен!
                new List<IQueryable<IVisitable>>()
                {
                    dbContext.Constructions.Where(x => x.SrcPacketId == filter.SrcPacketId).OrderBy(x => x.Id),
                    dbContext.Contracts.Where(x => x.SrcPacketId == filter.SrcPacketId).OrderBy(x => x.Id),
                    dbContext.ConstrParts.Where(x => x.SrcPacketId == filter.SrcPacketId).OrderBy(x => x.Id),
                    dbContext.Buildings.Where(x => x.SrcPacketId == filter.SrcPacketId).OrderBy(x => x.Id),
                    dbContext.Docsets.Where(x => x.SrcPacketId == filter.SrcPacketId).OrderBy(x => x.Id),
                    //dbContext.Documents.Where(x => x.SrcPacketId == srcPacketId).OrderBy(x => x.Id)
                }));
            // options for odata service
            // маппинг типов на адреса OData
            services.AddSingleton<ODataServiceOptions>(new ODataServiceOptions(new Dictionary<Type, string>()
                {
                    //{ typeof(StreamObjects.StreamConstruction), "Constructions" },
                    { typeof(StreamObjects.StreamConstruction), "StreamConstructions" },
                    { typeof(StreamObjects.StreamConstrPart), "StreamConstrParts" },
                    { typeof(StreamObjects.StreamBuilding), "StreamBuildings" },
                    { typeof(StreamObjects.StreamContract), "StreamContracts" },
                    { typeof(StreamObjects.StreamDocset), "StreamDocsets" },
                    { typeof(StreamObjects.StreamDocument), "StreamDocuments" }
                }));

            // odata client
            services.AddHttpClient<IODataClient, ODataClient>((sp, httpClient) => {
                var options = sp.GetRequiredService<IOptionsMonitor<TimedHostedServiceOptions>>().CurrentValue;
                httpClient.BaseAddress = new Uri(options.SVCM.Host, UriKind.Absolute);
                if (options.SVCM.Auth == TimedHostedServiceOptions.SVCMOptions.SVCMAuth.Basic)
                {
                    var authParam = Encoding.Default.GetBytes($"{options.SVCM.User}:{options.SVCM.Password}");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authParam));
                }
            });
            services.AddTransient<ODataClientOptions>(sp => {
                var options = sp.GetRequiredService<IOptionsMonitor<TimedHostedServiceOptions>>();
                var cts = new CancellationTokenSource(options.CurrentValue.HttpClientTimeoutTimeSpan);
                return new ODataClientOptions(cts.Token);
            });

            services.AddTransient<Func<CancellationToken, Visitors.MapAndSendDataVisitor>>(sp => {
                var ctx = sp;
                return (token) => new Visitors.MapAndSendDataVisitor(ctx.GetRequiredService<IODataService>(), ctx.GetRequiredService<IMapperService>(), token);
            });

            // services
            services.AddSingleton<IDbService, DbService>();
            services.AddSingleton<IMapperService, MapperService>();
            services.AddSingleton<ProcessPacketsService>();
            services.AddSingleton<ODataService>();
            services.AddSingleton<RetryODataServiceOptions>(sp => {
                var options = sp.GetRequiredService<IOptionsMonitor<TimedHostedServiceOptions>>();
                return new RetryODataServiceOptions(options.CurrentValue.HttpClientRetryCount, options.CurrentValue.HttpClientRetryIntervalTimeSpan);
            });
            services.AddSingleton<IODataService, RetryODataService>(sp => {
                var inner = sp.GetRequiredService<ODataService>();
                var logger = sp.GetRequiredService<ILogger<RetryODataService>>();
                var options = sp.GetRequiredService<RetryODataServiceOptions>();
                return new RetryODataService(inner, logger, options);
            });
            services.AddSingleton<ISendDataService, LoggingSendDataService>(sp => {
                var childService = new SendDataService(sp.GetRequiredService<Func<CancellationToken, Visitors.MapAndSendDataVisitor>>());
                return new LoggingSendDataService(childService, sp.GetRequiredService<ILogger<LoggingSendDataService>>());
            });

            // db context
            services.AddDbContext<Integra2Context>((sp, options) => {
                var serviceOptions = sp.GetRequiredService<IOptionsMonitor<TimedHostedServiceOptions>>();
                var connStr = context.Configuration.GetConnectionString("IntegraDB");
                if (serviceOptions.CurrentValue.Provider == "MSSQL")
                    options.UseSqlServer(connStr);
                else
                    options.UseNpgsql(connStr);
            }, ServiceLifetime.Scoped);

            // hosted service
            services.AddHostedService<TimedHostedService>();
        }
    }
}
