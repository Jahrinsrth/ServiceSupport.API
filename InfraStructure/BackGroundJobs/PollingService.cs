using Application.Interfaces;
using DataStore;
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

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
