using System.Net;
using Crypto.Data;
using Crypto.Data.Models;
using Crypto.Queries.Model;
using Crypto.Queries.Queries.Wallet;
using Crypto.Tests.MockObjects;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Crypto.Tests.Queris;

public class GetWalletQueryHandlerTests {
   private CryptoDBContext _dbContext;
   private GetWalletQueryHandler _handler;
   private Mock<IHttpClientFactory> _httpClientFactoryMock;

   [SetUp]
   public void SetUp() {
      var options = new DbContextOptionsBuilder<CryptoDBContext>()
         .UseInMemoryDatabase(Guid.NewGuid().ToString())
         .Options;

      _dbContext = new CryptoDBContext(options);

      _httpClientFactoryMock = new Mock<IHttpClientFactory>();

      _handler = new GetWalletQueryHandler(_dbContext, _httpClientFactoryMock.Object);
   }

   [Test]
   public async Task Handle_ReturnsWalletModel_WhenUserExistsAndBalanceAvailable() {
      var telegramId = "123456789";
      var apiKey = "test-key";
      var apiSecret = "test-secret";

      _dbContext.Users.Add(new User {
         TelegramId = telegramId,
         ByBitApiKey = apiKey,
         ByBitApiSicret = apiSecret
      });
      _dbContext.SaveChanges();

      var serverTimeResponse = JsonConvert.SerializeObject(new { time = 1234567890123 });
      var walletResponse = JsonConvert.SerializeObject(new {
         result = new {
            list = new[] {
               new {
                  coin = new[] {
                     new { coin = "BTC", walletBalance = "0.5" },
                     new { coin = "ETH", walletBalance = "1.2" },
                     new { coin = "USDT", walletBalance = "0" }
                  }
               }
            }
         }
      });

      var messageHandler = new MockSequenceHttpMessageHandler(new[] {
         new HttpResponseMessage {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(serverTimeResponse)
         },
         new HttpResponseMessage {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(walletResponse)
         }
      });

      var client = new HttpClient(messageHandler);
      _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);

      var query = new GetWalletQuery(telegramId);

      WalletModel result = await _handler.Handle(query, CancellationToken.None);

      Assert.That(result, Is.Not.Null);
      Assert.That(result.Assets, Has.Count.EqualTo(2));
      Assert.That(result.Assets.Any(a => a is { Item1: "BTC", Item2: 0.5f }));
      Assert.That(result.Assets.Any(a => a is { Item1: "ETH", Item2: 1.2f }));
   }

   [Test]
   public async Task Handle_ReturnsNull_WhenUserNotFound() {
      var query = new GetWalletQuery("999999999");

      WalletModel result = await _handler.Handle(query, CancellationToken.None);

      Assert.That(result, Is.Null);
   }
}