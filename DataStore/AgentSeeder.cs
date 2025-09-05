using Domain.Entities;
using Domain.Enum;

namespace DataStore
{
    public class AgentSeeder
    {
        private const int maxChatCountForAgent = 10;
        private const double JuniorMultiplier = 0.4;
        private const double MidLevelMultiplier = 0.6;
        private const double SeniorMultiplier = 0.8;
        private const double TeamLeadMultiplier = 0.5;

        public static List<Agent> SeedAgents()
        {
            return new List<Agent>
            {
                // Team A
                new Agent { Id = 11, Name = "TeamA_TeamLead", SeniorityLevel = SeniorityTypeOptions.TeamLead , MaxConcurrency = MaxCurrencyForTeamLead()},
                new Agent { Id = 12, Name = "TeamA_Mid1", SeniorityLevel = SeniorityTypeOptions.MidLevel, MaxConcurrency = MaxCurrencyForMidLevel()},
                new Agent { Id = 13, Name = "TeamA_Mid2", SeniorityLevel = SeniorityTypeOptions.MidLevel , MaxConcurrency = MaxCurrencyForMidLevel()},
                new Agent { Id = 14, Name = "TeamA_Junior", SeniorityLevel = SeniorityTypeOptions.Junior , MaxConcurrency = MaxCurrencyForJunior()},

                // Team B
                new Agent {  Id = 15, Name = "TeamB_Senior", SeniorityLevel = SeniorityTypeOptions.Senior , MaxConcurrency = MaxCurrencyForSenior()},
                new Agent {  Id = 16, Name = "TeamB_Mid", SeniorityLevel = SeniorityTypeOptions.MidLevel , MaxConcurrency = MaxCurrencyForMidLevel()},
                new Agent {  Id = 17, Name = "TeamB_Junior1", SeniorityLevel = SeniorityTypeOptions.Junior , MaxConcurrency = MaxCurrencyForJunior()},
                new Agent {  Id = 18, Name = "TeamB_Junior2", SeniorityLevel = SeniorityTypeOptions.Junior , MaxConcurrency = MaxCurrencyForJunior()},

                // Team C (Night Shift)
                new Agent { Id = 19, Name = "TeamC_Mid1", SeniorityLevel = SeniorityTypeOptions.MidLevel , MaxConcurrency = MaxCurrencyForMidLevel()},
                new Agent { Id = 20, Name = "TeamC_Mid2", SeniorityLevel = SeniorityTypeOptions.MidLevel , MaxConcurrency = MaxCurrencyForMidLevel()}
            };
        }

        public static List<Agent> SeedOverflowTeam()
        {
            return new List<Agent>
            {

                new Agent { Id = 21, Name = "Overflow_Junior_1", SeniorityLevel = SeniorityTypeOptions.Junior , MaxConcurrency = MaxCurrencyForJunior()},
                new Agent { Id = 22, Name = "Overflow_Junior_2", SeniorityLevel = SeniorityTypeOptions.Junior , MaxConcurrency = MaxCurrencyForJunior()},
                new Agent { Id = 23, Name = "Overflow_Junior_3", SeniorityLevel = SeniorityTypeOptions.Junior , MaxConcurrency = MaxCurrencyForJunior()},
                new Agent { Id = 24, Name = "Overflow_Junior_4", SeniorityLevel = SeniorityTypeOptions.Junior , MaxConcurrency = MaxCurrencyForJunior()},
                new Agent { Id = 25, Name = "Overflow_Junior_5", SeniorityLevel = SeniorityTypeOptions.Junior , MaxConcurrency = MaxCurrencyForJunior()},
                new Agent { Id = 26, Name = "Overflow_Junior_6", SeniorityLevel = SeniorityTypeOptions.Junior , MaxConcurrency = MaxCurrencyForJunior()},
            };
        }

        private static double MaxCurrencyForTeamLead() 
        {
            return 1 * TeamLeadMultiplier * maxChatCountForAgent;
        }

        private static double MaxCurrencyForSenior()
        {
            return 1 * SeniorMultiplier * maxChatCountForAgent;
        }
        private static double MaxCurrencyForMidLevel()
        {
            return 1 * MidLevelMultiplier * maxChatCountForAgent;
        }
        private static double MaxCurrencyForJunior()
        {
            return 1 * JuniorMultiplier * maxChatCountForAgent;
        }
    }
}
