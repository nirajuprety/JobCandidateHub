using JobCandidateHub.Application.Manager.Implementation;
using JobCandidateHub.Application.Manager.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobCandidateHub.Application
{
    public static class JobCandidateHubApplicationService
    {

        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICandidateManager, CandidateManager>();
            return services;
        }
    }
}
