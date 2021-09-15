using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.OData;
using Xunit;

namespace TA.IMPDM.Service.Tests.OData
{
    public class ODataClientTests
    {
        const string host = "http://localhost";

        private IODataClient GetClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(host, UriKind.Absolute);
            var client = new ODataClient(httpClient, new ODataClientOptions(CancellationToken.None));
            return client;
        }

        [Fact]
        public async Task InsertAsync_When_Default_HttpClient_Throw_HttpRequestException()
        {
            var client = GetClient(new HttpClient());

            await Assert.ThrowsAsync<HttpRequestException>(() => 
                client.InsertAsync(new Uri("test", UriKind.Relative), "{'name':'value'}", CancellationToken.None));
        }

        [Fact]
        public async Task InsertAsync_When_Send_Valid_Request_Then_Receive_SuccessStatusCode()
        {
            var httpMock = new MockHttpMessageHandler();
            httpMock
                .Expect("/test")
                .WithContent("{'name':'value'}")
                .Respond(HttpStatusCode.Created);
                
            var client = GetClient(httpMock.ToHttpClient());

            var result = await client.InsertAsync(new Uri("test", UriKind.Relative), "{'name':'value'}", CancellationToken.None);

            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task InsertAsync_When_ServiceUnavailable_Then_Not_SuccessStatusCode()
        {
            var httpMock = new MockHttpMessageHandler();
            httpMock
                .Expect("/test")
                .Respond(HttpStatusCode.ServiceUnavailable);
            var client = GetClient(httpMock.ToHttpClient());

            var result = await client.InsertAsync(new Uri("test", UriKind.Relative), "{}", CancellationToken.None);

            Assert.False(result.IsSuccessStatusCode);
        }
    }
}
