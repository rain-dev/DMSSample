using Moq;
using Moq.Protected;
using NUnit.Framework;
using Pinewood.DMSSample.Business.Clients;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pinewood.DMSSample.BusinessTest.Clients
{
    internal class PartAvailabilityClientTest
    {
        private Mock<HttpMessageHandler> _httpMessageHandler;
        private PartAvailabilityClient _partAvailabilityClient;
        [SetUp]
        public void Setup()
        {
            _httpMessageHandler = new Mock<HttpMessageHandler>();
            _partAvailabilityClient = new PartAvailabilityClient(new HttpClient(_httpMessageHandler.Object));

        }

        [TestCase("ABC")]
        [TestCase("EFG")]
        public async Task GetAvailability_ValidStockCode(string stockCode)
        {

            _httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(10.ToString())
                })
                .Verifiable();

            var count = await _partAvailabilityClient.GetAvailability(stockCode);

            Assert.AreEqual(10, count);
            _httpMessageHandler.VerifyAll();
        }

        [TestCase("ABC")]
        [TestCase("EFG")]
        public void GetAvailability_FailedRequest(string stockCode)
        {

            _httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                })
                .Verifiable();

            var ex = Assert.ThrowsAsync<Exception>(() => _partAvailabilityClient.GetAvailability(stockCode));


            Assert.IsNotNull(ex);
            Assert.AreEqual(ex.Message, $"Could not get part availability for {stockCode}");
        }
    }
}
