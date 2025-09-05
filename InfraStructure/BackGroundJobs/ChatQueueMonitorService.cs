using Application.Interfaces;
using DataStore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InfraStructure.BackGroundJobs
{
    public class ChatQueueMonitorService : BackgroundService
    {
        private readonly IAgentAssignmentService _agentAssignmentService;
        private readonly IChatQueueService _chatQueueService;
        private readonly ILogger<ChatQueueMonitorService> _logger;

        public ChatQueueMonitorService(IAgentAssignmentService agentAssignmentService, IChatQueueService chatQueueService, ILogger<ChatQueueMonitorService> logger)
        {
            _agentAssignmentService = agentAssignmentService;
            _chatQueueService = chatQueueService;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    foreach (var session in _chatQueueService.GetAll())
                    {
                        if (!session.IsActive)
                        {
                            _chatQueueService.RemoveChat(session);
                            _logger.LogInformation($"Chat {session.Id} removed from queue.");

                            var agent = MemoryStore.Agents.FirstOrDefault(a => a.Id == session.AssignedAgentId);
                            if (agent != null)
                            {
                                agent.CurrentChats--;
                            }
                        }
                    }

                    _agentAssignmentService.AssignChats();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in ChatQueueMonitorService");
                    throw;
                }

                await Task.Delay(15000, stoppingToken); 
            }
        }
    }
}
