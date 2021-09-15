using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB.Interfaces;
using TA.IMPDM.Service.OData;
using TA.IMPDM.Service.StreamObjects;

namespace TA.IMPDM.Service.Services
{
    /// <summary>
    /// Сервис для отправки данные через ODATA
    /// </summary>
    public class ODataService : IODataService
    {
        private readonly IODataClient client;
        private readonly IReadOnlyDictionary<Type, string> oDataTable;

        public ODataService(IODataClient client, ODataServiceOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.oDataTable = options.StreamTypeToODataTable;
        }

        private Result GetHttpError(HttpStatusCode statusCode, string errorMessage)
        {
            switch (statusCode)
            {
                case HttpStatusCode.RequestTimeout:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.GatewayTimeout:
                    return Result.Error(Result.ErrorCode.HttpCanRetry, $"{statusCode.ToString()}: {errorMessage}");
                default:
                    return Result.Error(Result.ErrorCode.HttpFail, $"{statusCode.ToString()}: {errorMessage}");
            }
        }

        public async Task<Result> SendAsync<T>(T streamObject, CancellationToken token) where T : IStreamObject
        {
            var address = new Uri(oDataTable[streamObject.GetType()], UriKind.Relative);
            string json = JsonConvert.SerializeObject(streamObject);

            try
            {
                var result = await client
                    .InsertAsync(address, json, token)
                    .ConfigureAwait(false);
                return result.IsSuccessStatusCode
                    ? Result.OK()
                    : GetHttpError(result.StatusCode, result.ErrorMessage);
            }
            catch (OperationCanceledException)
            {
                return Result.Error(Result.ErrorCode.HttpCanRetry, "Истекло время ожидания");
            }
            catch (HttpRequestException ex)
            {
                return Result.Error(Result.ErrorCode.Exception, ex.Message);
            }
        }
    }
}
