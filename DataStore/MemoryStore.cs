using Domain.Entities;

namespace DataStore
{
    public static class MemoryStore
    {
        public static List<Agent> Agents = AgentSeeder.SeedAgents();
        public static List<Agent> OverflowAgents = AgentSeeder.SeedOverflowTeam();
        public static Queue<ChatSession> ChatQueue = new();
        public static bool ChatResponse = false;
    }
}
