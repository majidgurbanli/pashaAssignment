namespace PashaVacancyProject.Domain.DInfrastucture.DAbstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;

        void SaveChanges();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        int? UserID { set; get; }

        string SaveChangesDetached();

        Task<string> SaveChangesDetachedAsync();
        void Detach();


    }
}
