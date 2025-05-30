namespace Crypto.Tests.MockObjects;

internal class MockSequenceHttpMessageHandler(IEnumerable<HttpResponseMessage> responses) : HttpMessageHandler {
   private readonly Queue<HttpResponseMessage> _responses = new(responses);

   protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
      if (_responses.Count == 0) throw new InvalidOperationException("No more responses configured");
      return Task.FromResult(_responses.Dequeue());
   }
}