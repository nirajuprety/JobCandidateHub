using JobCandidateHub.Domain.Entities;
using JobCandidateHub.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class JobCandidateHubDbContext : DbContext
    {
        public JobCandidateHubDbContext()
        {

        }
        public JobCandidateHubDbContext(DbContextOptions<JobCandidateHubDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DbContext");

                optionsBuilder.UseNpgsql(connectionString);
            }
        }
        public DbSet<ECandidate> Candidate { get; set; }
        public DbSet<ECandidateHistory> CandidateHistory { get; set; }
        public override int SaveChanges()
        {
            if (MapperHelper._isUnitTest == false)
            {
                AddHistory<ECandidate, ECandidateHistory>(CandidateHistory);
            }
            return base.SaveChanges();
        }
        public void AddHistory<TEntity, THistoryEntity>(DbSet<THistoryEntity> historyTable)
             where TEntity : class
             where THistoryEntity : class
        {
            var addedEntities = ChangeTracker.Entries<TEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();
            foreach (var addedEntity in addedEntities)
            {
                var entity = addedEntity.Entity;
                var state = addedEntity.State;
                if (state == EntityState.Added)
                {
                    Entry(entity).State = EntityState.Added;
                    base.SaveChanges();
                    // Logic for handling newly added entity
                }
                // Save changes to generate the primary key Id for TEntity
                var historyEntity = MapperHelper.Mapper.Map<THistoryEntity>(entity);
                var entityIdProperty = historyEntity.GetType().GetProperty("Id");
                if (entityIdProperty != null)
                {
                    entityIdProperty.SetValue(historyEntity, 0);
                }
                historyTable.Add(historyEntity);
                Entry(historyEntity).State = EntityState.Added;
            }
        }
    }
}
