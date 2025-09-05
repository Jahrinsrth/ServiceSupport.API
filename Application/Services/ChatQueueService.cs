using Application.Interfaces;
using DataStore;
using Domain.DTO;
using Domain.Entities;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ChatQueueService : IChatQueueService
    {
        private readonly ILogger<ChatQueueService> _logger;
        
        public ChatQueueService(ILogger<ChatQueueService> logger)
        {
            _logger = logger;
        }

        public ChatSessionResponseDto CreateChat(ChatSessionDto chatSessionDto)
        {
            ChatSessionResponseDto response  = new ChatSessionResponseDto();

            ChatSession chatSession = new ChatSession();
            chatSession.Id = chatSessionDto.Id;
            chatSession.CreatedDate = chatSessionDto.CreatedDate;
            chatSession.Status = ChatStatusTypeOptions.Pending;

            var capacity = CalculateTeamCapacity();
            var maxQueueLength = (int)(capacity * 1.5);

            if (GetChatQueueLength() >= maxQueueLength)
            {
                if (IsOfficeHours() && MemoryStore.OverflowAgents.Count > 0)
                {
                    var overflowCapacity = MemoryStore.OverflowAgents.Sum(a => a.MaxConcurrency);
                    if (GetChatQueueLength() >= overflowCapacity * 1.5)
                        response.Message = "Overflow queue full";
                }
                else
                {
                    response.Message = "Queue full";
                }
            }

            MemoryStore.ChatQueue.Enqueue(chatSession);
            response.Id = chatSession.Id;
            _logger.LogInformation($"Chat {chatSession.Id} enqueued.");

            return response;
        }

        public void Poll(ChatSession session)
        {
            if (session != null)
            {
                if (session.PollCount == 3)
                {
                    session.IsPollNeeded = false;
                    _logger.LogInformation($"Chat {session.Id} poll ended after 3 attempts.");
                }
                else 
                {
                    session.PollCount++;
                    session.IsActive = true;
                }            
            }
        }

        public void MonitorInactivity(ChatSession session)
        {
            if (session.PollCount < 3)
            {
                session.Status = ChatStatusTypeOptions.Inactive;
                session.IsActive = false;
                _logger.LogWarning($"Chat {session.Id} marked inactive.");
            }          
        }

        public void RemoveChat(ChatSession session)
        {
            if (GetChatQueueLength() > 0)
            {
                MemoryStore.ChatQueue.ToList().Remove(session);
            }
        }

        public List<ChatSession> GetAll()
        {
            return MemoryStore.ChatQueue.ToList();
        }

        public int GetChatQueueLength()
        {
            return MemoryStore.ChatQueue.Count;
        }

        private int CalculateTeamCapacity()
        {
            return (int)MemoryStore.Agents.Sum(a => a.MaxConcurrency);
        }

        private bool IsOfficeHours()
        { 
            // Assuming office hours are from 9 to 5
            var now = DateTime.UtcNow;
            return now.Hour >= 9 && now.Hour < 17;
        }

    }
}
