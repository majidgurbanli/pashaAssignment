using System.Linq.Expressions;

namespace PashaVacancyProject.Domain.DInfrastucture.DAbstract
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> Find(Expression<Func<T, bool>> Expression);

        void Add(T Entity);

        void AddOrUpdate(T Entity);

        void DeleteRange(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        void Delete(T Entity);

        void Delete(IEnumerable<T> Entity);

        void Update(T Entity);

        void Update(T Entities, params Expression<Func<T, object>>[] AllUpdatedProperties);

        void Update(IEnumerable<T> Entities, params Expression<Func<T, object>>[] AllUpdatedProperties);
        void AddOrUpdateRange(IEnumerable<T> entities);

        void AddRangeAsync(IEnumerable<T> Entities);

        bool BulkInsert(IEnumerable<T> Entities, int UserID);

    }
}
