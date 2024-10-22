using IdentityServer4.EntityFramework.Options;
using JobCandidateHub.Infrastructure.Mapper;
using JobCandidateHub.Infrastructure.Repository;
using JobCandidateHub.UnitTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace JobCandidateHub.UnitTest
{
    public class DatabaseFixture : IDisposable
    {
        public JobCandidateHubDbContext mockDbContext;
        public DatabaseFixture()
        {
            MapperHelper._isUnitTest = true;
            ConfigurationStoreOptions storeOptions = new ConfigurationStoreOptions();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(storeOptions);

            var builder = new DbContextOptionsBuilder<JobCandidateHubDbContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            builder.UseApplicationServiceProvider(serviceCollection.BuildServiceProvider());

            var databaseContext = new JobCandidateHubDbContext(builder.Options);

            databaseContext.Database.EnsureCreated();

            #region JobCandidate
            JobCandidateDataInfo.Init();
            var jobCandidateDetail = JobCandidateDataInfo.CandidatesList;
            databaseContext.Candidate.AddRange(jobCandidateDetail);
            #endregion

            databaseContext.SaveChanges();
            mockDbContext = databaseContext;
        }
        public void Dispose()
        {
            MapperHelper._isUnitTest = false;

            mockDbContext.Database.EnsureDeleted();
        }
    }
}
