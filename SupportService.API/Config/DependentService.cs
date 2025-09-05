using Application.Interfaces;
using Application.Services;
using InfraStructure.BackGroundJobs;

namespace SupportService.API.Config
{
    public static class DependentService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IChatQueueService, ChatQueueService>();
            services.AddSingleton<IAgentAssignmentService, AgentAssignmentService>();
            services.AddHostedService<ChatQueueMonitorService>();
            services.AddHostedService<PollingService>();
            return services;
        }
    }
}
