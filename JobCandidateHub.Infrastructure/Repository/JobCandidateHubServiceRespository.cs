using JobCandidateHub.Domain.Interface.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class JobCandidateHubServiceRespository<t> : IServiceRepository<t>, IDisposable where t : class
    {
        JobCandidateHubDbContext db;
        DbSet<t> entity;

        public JobCandidateHubServiceRespository()
        {
            db = new JobCandidateHubDbContext();
            entity = db.Set<t>();
        }
        public JobCandidateHubServiceRespository(JobCandidateHubDbContext db)
        {
            this.db = db;
            entity = db.Set<t>();
        }
        public async Task<List<t>> ListAsync()
        {
            return await entity.AsNoTracking().ToListAsync();
        }
        public async Task<t> UpdateAsync(t model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            try
            {
                db.ChangeTracker.Clear();
                var newEntity = entity.Update(model);
                var newEntityToRet = newEntity.Entity;
                db.SaveChanges();

                return newEntityToRet;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw;
            }
        }
        public async Task<t> FindAsync(int id)
        {
            var newEntity = await entity.FindAsync(id);
            return newEntity;
        }
        public async Task<bool> AddRangeAsync(List<t> model)
        {
            try
            {
                db.ChangeTracker.Clear();
                await entity.AddRangeAsync(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception wx)
            {
                return false;
            }
        }

        public async Task<bool> RemoveRangeAsync(List<t> model)
        {
            try
            {
                entity.RemoveRange(model);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception wx)
            {
                return false;
            }
        }

        public async Task<t> AddAsync(t model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            try
            {
                db.ChangeTracker.Clear();
                var newEntity = await entity.AddAsync(model);
                var newEntityToRet = newEntity.Entity;
                db.SaveChanges();

                return newEntityToRet;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw;
            }
        }

        public async Task<int> RemoveAsync(t model)
        {
            db.ChangeTracker.Clear();
            entity.Remove(model);
            return await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            //your memory
            //your connection
            //place to clean
        }

    }
    [ExcludeFromCodeCoverage]
    public class JobCandidateHubServiceFactory:IDisposable, IServiceFactory
    {
        public JobCandidateHubDbContext db;
        public bool _isforTest;

        public JobCandidateHubServiceFactory()
        {
            db = new JobCandidateHubDbContext();
        }
        public JobCandidateHubServiceFactory(JobCandidateHubDbContext db, bool isforTest)
        {
            this.db = db;
            _isforTest = isforTest;    
        }
        public void Dispose()
        {
            if (!_isforTest)
            {
                db.Dispose();
            }
            // throw new NotImplementedException();
            //  db.Dispose();
        }
        public IServiceRepository<t> GetInstance<t>() where t : class
        {
            return new JobCandidateHubServiceRespository<t>(db);
        }
        public void BeginTransaction()
        {
            this.db.Database.BeginTransaction();
        }
        public void RollBack()
        {
            this.db.Database.RollbackTransaction();
        }

        public void CommitTransaction()
        {
            this.db.Database.CommitTransaction();
        }

        public void WriteLog(string message, object exception, string v)
        {
            // throw new NotImplementedException();
        }
    }
}
