using Application.Interfaces;
using DataStore;
using Domain.Enum;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InfraStructure.BackGroundJobs
{
    public class ChatPollingMonitor : BackgroundService
    {
        private readonly IChatQueueService _chatQueueService;
        private readonly ILogger<ChatPollingMonitor> _logger;

        public ChatPollingMonitor(IChatQueueService chatQueueService, ILogger<ChatPollingMonitor> logger)
        {
            _chatQueueService = chatQueueService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    foreach (var session in MemoryStore.ChatQueue.ToList())
            //    {
            //        _chatQueueService.MonitorInactivity(session);
            //    }
            //    await Task.Delay(17000, stoppingToken);
            //}
        }
    }
}
