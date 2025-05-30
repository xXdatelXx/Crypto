using System.Net;

namespace Crypto.Tests.MockObjects;

internal class MockHttpMessageHandler(string response) : HttpMessageHandler {
   protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
      return Task.FromResult(new HttpResponseMessage {
         StatusCode = HttpStatusCode.OK,
         Content = new StringContent(response)
      });
   }
}