using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TA.IMPDM.Service.OData
{
    public class ODataClient : IODataClient
    {
        private readonly HttpClient httpClient;
        private readonly CancellationToken token;

        public ODataClient(HttpClient httpClient, ODataClientOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.token = options.Token;
        }

        public async Task<ODataClientResult> InsertAsync(Uri address, string content, CancellationToken cancellationToken)
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(token, cancellationToken);

            HttpContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient
                .PostAsync(address, stringContent, cts.Token)
                .ConfigureAwait(false);
            string errorMessage = null;
            if (response.IsSuccessStatusCode == false && response.Content != null)
            {
                errorMessage = await response
                    .Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);
            }
            return new ODataClientResult(response.IsSuccessStatusCode, response.StatusCode, errorMessage);
        }
    }
}
