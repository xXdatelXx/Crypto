using Crypto.Application.Logic.Commands.User.SendTrackingCurrencies;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Crypto.Function
{
    public class TrackUserCurrenciesFunction
    {
        //private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public TrackUserCurrenciesFunction(/*IMediator mediator, */ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TrackUserCurrenciesFunction>();
            //_mediator = mediator;
        }

        [Function("TrackUserCurrenciesFunction")]
        public void Run([TimerTrigger("* * * * *")] TimerInfo myTimer, CancellationToken token)
        {
            if (myTimer.ScheduleStatus is not null) 
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            var http = new HttpClient();
            
            //http.BaseAddress = new Uri("https://localhost:44396/");
            http.GetAsync("https://microsoft.com");
            //http.GetAsync("api/UserCRUD/SendTrackingCurrencies");
            //_mediator.Send(new SendTrackingCurrenciesCommand(), token);
        }
    }
}
