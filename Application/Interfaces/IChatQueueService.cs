using Domain.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IChatQueueService
    {
        public ChatSessionResponseDto CreateChat(ChatSessionDto chatSessionDto);
        public void RemoveChat(ChatSession session);
        public List<ChatSession> GetAll();
        public int GetChatQueueLength();
        public void Poll(ChatSession session);
        public void MonitorInactivity(ChatSession chatSession);
    }
}
