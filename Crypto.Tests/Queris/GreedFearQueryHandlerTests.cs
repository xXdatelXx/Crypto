using Crypto.Queries.Queries.GreedFear;
using Crypto.Tests.MockObjects;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Crypto.Tests.Queris;

public class GreedFearQueryHandlerTests {
   private Mock<IConfiguration> _configurationMock;
   private GreedFearQueryHandler _handler;
   private HttpClient _httpClient;
   private Mock<IHttpClientFactory> _httpClientFactoryMock;

   [SetUp]
   public void SetUp() {
      _httpClientFactoryMock = new Mock<IHttpClientFactory>();
      _configurationMock = new Mock<IConfiguration>();
   }

   [Test]
   public async Task Handle_ReturnsFearGreedValue_WhenApiReturnsValidResponse() {
      // Arrange
      string expectedValue = "72";

      var mockResponse = new {
         data = new {
            value = expectedValue
         }
      };

      SetupMockHttp(JsonConvert.SerializeObject(mockResponse));
      _configurationMock.Setup(c => c["CoinMarketCapApiKey"]).Returns("fake-api-key");

      _handler = new GreedFearQueryHandler(_httpClientFactoryMock.Object, _configurationMock.Object);

      var query = new GreedFearQuery();

      // Act
      string result = await _handler.Handle(query, CancellationToken.None);

      // Assert
      Assert.That(result, Is.EqualTo(expectedValue));
   }

   private void SetupMockHttp(string jsonResponse) {
      var handler = new MockHttpMessageHandler(jsonResponse);
      _httpClient = new HttpClient(handler);

      _httpClientFactoryMock
         .Setup(f => f.CreateClient(It.IsAny<string>()))
         .Returns(_httpClient);
   }
}