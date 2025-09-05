using Application.Interfaces;
using DataStore;
using Domain.Entities;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class AgentAssignmentService : IAgentAssignmentService
    {
        private readonly List<Agent> _agents;
        private readonly IChatQueueService _chatQueueService;
        private readonly ILogger<AgentAssignmentService> _logger;

        public AgentAssignmentService(IChatQueueService chatQueueService, ILogger<AgentAssignmentService> logger)
        {
            _chatQueueService = chatQueueService;
            _logger = logger;
            _agents = AgentSeeder.SeedAgents();
        }

        public void AssignChats()
        {
            var sessions = _chatQueueService.GetAll().Where(s => s.Status == ChatStatusTypeOptions.Pending).ToList();

            foreach (var session in sessions)
            {
                var availableAgent = FindAvailableAgent();
                if (availableAgent != null)
                {
                    session.AssignedAgentId = availableAgent.Id;
                    session.Status = ChatStatusTypeOptions.Assigned;
                    availableAgent.CurrentChats++;

                    _logger.LogInformation($"Chat {session.Id} assigned to Agent {availableAgent.Name}");
                }
                else
                {
                    session.Status = ChatStatusTypeOptions.Refused;
                    _logger.LogWarning($"Chat {session.Id} refused due to no available agents.");
                }
            }
        }

        private Agent? FindAvailableAgent()
        {
            const int maxConCurrencyChats = 10;
            var availableAgent = _agents.Where(a => a.IsActive && a.CurrentChats < maxConCurrencyChats)
                                 .OrderBy(a => a.SeniorityLevel)
                                 .FirstOrDefault();

            return availableAgent;
        }
    }
}
