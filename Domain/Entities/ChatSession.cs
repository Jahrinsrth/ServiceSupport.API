using Domain.Entities.Base;
using Domain.Enum;

namespace Domain.Entities
{
    public class ChatSession : Audit
    {
        public int Id { get; set; }
        public ChatStatusTypeOptions Status { get; set; }
        public int? AssignedAgentId { get; set; }
        public int PollCount { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsPollNeeded { get; set; } = true;
    }
}
