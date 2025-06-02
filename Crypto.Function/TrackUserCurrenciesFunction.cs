using Crypto.Application.Logic.Commands.User.SendTrackingCurrencies;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Crypto.Function
{
    public class TrackUserCurrenciesFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public TrackUserCurrenciesFunction(IMediator mediator, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TrackUserCurrenciesFunction>();
            _mediator = mediator;
        }

        [Function("TrackUserCurrenciesFunction")]
        public void Run([TimerTrigger("* * * * *")] TimerInfo myTimer)
        {
            if (myTimer.ScheduleStatus is not null) 
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            _mediator.Send(new SendTrackingCurrenciesCommand(), new CancellationToken());
        }
    }
}
