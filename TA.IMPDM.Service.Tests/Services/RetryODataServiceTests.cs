using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.Services;
using TA.IMPDM.Service.StreamObjects;
using Xunit;

namespace TA.IMPDM.Service.Tests.Services
{
    public class RetryODataServiceTests
    {
        const int retryCount = 3;

        private IODataService GetService(IODataService innerService)
        {
            var loggerMock = new Mock<ILogger>();
            return new RetryODataService(innerService, loggerMock.Object, new RetryODataServiceOptions(retryCount, TimeSpan.FromMilliseconds(0)));
        }

        [Fact]
        public async Task SendAsync_When_InnerReturnOK_Do_OneTry()
        {
            var streamObjectMock = new Mock<IStreamObject>();
            var innerMock = new Mock<IODataService>();
            innerMock.Setup(x => x.SendAsync(It.IsAny<IStreamObject>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(Result.OK()));

            var service = GetService(innerMock.Object);

            var result = await service.SendAsync(streamObjectMock.Object, CancellationToken.None);

            innerMock.Verify(x => x.SendAsync(It.IsAny<IStreamObject>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task SendAsync_When_InnerReturnHttpCanRetry_Do_Retry()
        {
            var streamObjectMock = new Mock<IStreamObject>();
            var innerMock = new Mock<IODataService>();
            int retryIndex = 0;
            innerMock.Setup(x => x.SendAsync(It.IsAny<IStreamObject>(), It.IsAny<CancellationToken>()))
                .Returns(() =>
                {
                    if (retryIndex++ < 2)
                        return Task.FromResult(Result.Error(Result.ErrorCode.HttpCanRetry, "Test error"));
                    else
                        return Task.FromResult(Result.OK());
                });

            var service = GetService(innerMock.Object);

            var result = await service.SendAsync(streamObjectMock.Object, CancellationToken.None);

            innerMock.Verify(x => x.SendAsync(It.IsAny<IStreamObject>(), It.IsAny<CancellationToken>()), Times.Exactly(retryCount));
            Assert.True(result.Success);
        }

        [Fact]
        public async Task SendAsync_When_InnerReturnHttpFail_Do_NoRetry()
        {
            var streamObjectMock = new Mock<IStreamObject>();
            var innerMock = new Mock<IODataService>();
            innerMock.Setup(x => x.SendAsync(It.IsAny<IStreamObject>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(Result.Error(Result.ErrorCode.HttpFail, "Test error")));

            var service = GetService(innerMock.Object);

            var result = await service.SendAsync(streamObjectMock.Object, CancellationToken.None);

            innerMock.Verify(x => x.SendAsync(It.IsAny<IStreamObject>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.False(result.Success);
        }
    }
}
