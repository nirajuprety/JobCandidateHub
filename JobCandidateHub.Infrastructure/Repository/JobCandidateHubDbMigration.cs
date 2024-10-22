using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobCandidateHub.Infrastructure.Repository
{
    public class JobCandidateHubDbMigration
    {
        public static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = new JobCandidateHubDbContext(serviceScope.ServiceProvider.GetService<
                           DbContextOptions<JobCandidateHubDbContext>>()))
                {
                    context.Database.Migrate();
                }


            }
        }
    }
}
