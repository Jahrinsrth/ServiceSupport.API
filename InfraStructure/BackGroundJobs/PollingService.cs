using Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InfraStructure.BackGroundJobs
{
    public class PollingService : BackgroundService
    {
        private readonly IChatQueueService _chatQueueService;
        private readonly ILogger<PollingService> _logger;

        public PollingService(IChatQueueService chatQueueService, ILogger<PollingService> logger)
        {
            _chatQueueService = chatQueueService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var chatSessions = _chatQueueService.GetAll();

                    foreach (var session in chatSessions)
                    {
                        if (session.IsPollNeeded)
                        {
                            for (int i = 1; i <= 3; i++)
                            {
                                _chatQueueService.Poll(session);
                            }
                        }
                    }

                    foreach (var session in chatSessions)
                    {
                        _chatQueueService.MonitorInactivity(session);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in PollingService");
                    throw;
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
