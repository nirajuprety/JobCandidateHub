namespace JobCandidateHub.Domain.Interface.Core
{
    public interface IServiceFactory
    {
        IServiceRepository<t> GetInstance<t>() where t : class;
        void BeginTransaction();
        void RollBack();
        void CommitTransaction();
    }
}
