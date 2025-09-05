using Domain.Enum;

namespace Domain.Entities
{
    public class Agent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SeniorityTypeOptions SeniorityLevel { get; set; }
        public int CurrentChats { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsAvailable { get; set; } = true;
        public DateTime ShiftEndTime { get; set; }
        public double MaxConcurrency {  get; set; }
    }
}
