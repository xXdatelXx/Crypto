using Crypto.Queries.Queries.Price;
using Crypto.Tests.MockObjects;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Crypto.Tests.Queris;

public class GetPriceQueryHandlerTests {
   private GetPriceQueryHandler _handler;
   private HttpClient _httpClient;
   private Mock<IHttpClientFactory> _httpClientFactoryMock;
   private HttpMessageHandler _httpMessageHandler;

   [SetUp]
   public void SetUp() {
      _httpClientFactoryMock = new Mock<IHttpClientFactory>();
   }

   [Test]
   public void Handle_ThrowsException_WhenTimeIsInFuture() {
      var futureDate = DateTime.UtcNow.AddMinutes(10);
      var query = new GetPriceQuery("BTCUSDT", futureDate);
      _handler = new GetPriceQueryHandler(_httpClientFactoryMock.Object);

      Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
   }

   [Test]
   public async Task Handle_ReturnsPrice_WhenTimeIsProvided() {
      var expectedPrice = "65234.56";
      var mockResponse = new {
         result = new {
            list = new[] {
               new object[] { "val1", "val2", "val3", "val4", expectedPrice }
            }
         }
      };

      SetupMockHttp(JsonConvert.SerializeObject(mockResponse));

      var query = new GetPriceQuery("BTCUSDT", DateTime.UtcNow.AddMinutes(-10));

      _handler = new GetPriceQueryHandler(_httpClientFactoryMock.Object);

      float result = await _handler.Handle(query, CancellationToken.None);

      Assert.That(float.Parse(expectedPrice), Is.EqualTo(result));
   }

   [Test]
   public async Task Handle_ReturnsLatestPrice_WhenTimeIsNull() {
      var expectedPrice = "62345.67";
      var mockResponse = new {
         result = new {
            list = new[] {
               new {
                  lastPrice = expectedPrice
               }
            }
         }
      };

      SetupMockHttp(JsonConvert.SerializeObject(mockResponse));

      var query = new GetPriceQuery("BTCUSDT");

      _handler = new GetPriceQueryHandler(_httpClientFactoryMock.Object);

      float result = await _handler.Handle(query, CancellationToken.None);

      Assert.That(float.Parse(expectedPrice), Is.EqualTo(result));
   }

   private void SetupMockHttp(string jsonResponse) {
      _httpMessageHandler = new MockHttpMessageHandler(jsonResponse);
      _httpClient = new HttpClient(_httpMessageHandler);

      _httpClientFactoryMock
         .Setup(f => f.CreateClient(It.IsAny<string>()))
         .Returns(_httpClient);
   }
}