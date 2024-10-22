using JobCandidateHub.Domain.Interface;
using JobCandidateHub.Domain.Interface.Core;
using JobCandidateHub.Infrastructure.Repository;
using JobCandidateHub.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Infrastructure
{
    public static class JobCandidateHubInfrastructureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JobCandidateHubDbContext>(x => x.UseNpgsql(configuration["ConnectionStrings:DbContext"]), ServiceLifetime.Scoped);
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IServiceFactory, JobCandidateHubServiceFactory>();
            services.AddScoped(typeof(IServiceRepository<>), typeof(JobCandidateHubServiceRespository<>));
            return services;
        }
    }
}
