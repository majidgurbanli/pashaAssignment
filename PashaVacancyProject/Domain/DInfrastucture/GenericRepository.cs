using Microsoft.EntityFrameworkCore;
using PashaVacancyProject.Domain.DInfrastucture.DAbstract;
using System.Linq.Expressions;
using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.DInfrastucture
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly BoxDbContent Context;
        private readonly DbSet<T> DbSet;

        public GenericRepository(BoxDbContent Context)
        {

            this.Context = Context;
            this.DbSet = Context.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = DbSet;

            return query;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> Expression)
        {

            IQueryable<T> query = DbSet.Where(Expression);

            return query;
        }

        public virtual void AddRangeAsync(IEnumerable<T> Entities)
        {
            DbSet.AddRangeAsync(Entities);
        }


        public virtual bool BulkInsert(IEnumerable<T> Entities, int UserID)
        {
            string ErrorMessage = string.Empty;
            var PartEntities = Entities.IEnumerablePartition(1000).ToList();

            Parallel.ForEach(PartEntities, (Entities, LoopState) =>
            {
                try
                {
                    BoxDbContent BulkDbContext = new BoxDbContent();

                    BulkDbContext.Set<T>().AddRangeAsync(Entities);


                    var AddEdEntities = BulkDbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();


                    Parallel.ForEach(AddEdEntities, AddEdEntity =>
                    {
                        var BaseEntity = AddEdEntity.Entity as BaseEntity;
                        if (BaseEntity != null)
                        {
                            BulkDbContext.Entry<BaseEntity>(BaseEntity).Property(x => x.EdituID).IsModified = false;
                            BulkDbContext.Entry<BaseEntity>(BaseEntity).Property(x => x.EditDate).IsModified = false;

                            BaseEntity.EdituID = null;
                            BaseEntity.EditDate = null;
                            BaseEntity.ReguID = UserID;
                        }
                    });

                    BulkDbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    ErrorMessage = Ex.Message;
                    LoopState.Break();
                }
            });

            return string.IsNullOrEmpty(ErrorMessage);

        }




        public virtual void AddOrUpdate(T Entity)
        {

            _ = !DbSet.Any(s => s == Entity) ? DbSet.Add(Entity) : DbSet.Update(Entity);

        }
        public virtual void AddOrUpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _ = !DbSet.Any(s => s == entity) ? DbSet.Add(entity) : DbSet.Update(entity);
            }
        }
        public virtual void Add(T Entity)
        {

            DbSet.Add(Entity);
        }

        public virtual void DeleteRange(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = DbSet.Where(predicate);

            DbSet.RemoveRange(query);
        }
        public virtual void Delete(T Entity)
        {

            DbSet.Remove(Entity);
        }
        public virtual void Delete(IEnumerable<T> Entity)
        {

            DbSet.RemoveRange(Entity);
        }
        public virtual void Update(IEnumerable<T> Entities)
        {
            foreach (var Entity in Entities)
            {
                Update(Entity);
            }
        }
        public virtual void Update(T Entity)
        {

            DbSet.Update(Entity);


        }
        public virtual void Update(T Entity, params Expression<Func<T, object>>[] AllUpdatedProperties)
        {
            var UpdatedProperties = AllUpdatedProperties.UpdatedProperties(Context).ToList();

            var KeyNames = Context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name);

            try
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;


                Context.Entry(Entity).State = EntityState.Modified;

                var AllPropertyNames = Context.Entry(Entity).CurrentValues.Properties.Select(x => x.Name).ToList();




                var PropertyNames = AllPropertyNames.Except(UpdatedProperties).Except(KeyNames).ToList();

                PropertyNames.ForEach(PropertyName => Context.Entry(Entity).Property(PropertyName).IsModified = false);

            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }



        public virtual void Update(IEnumerable<T> Entities, params Expression<Func<T, object>>[] AllUpdatedProperties)
        {

            var UpdatedProperties = AllUpdatedProperties.UpdatedProperties(Context).ToList();

            var KeyNames = Context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name);

            try
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;




                foreach (var Entity in Entities)
                {
                    Context.Entry(Entity).State = EntityState.Modified;

                    var AllPropertyNames = Context.Entry(Entity).CurrentValues.Properties.Select(x => x.Name).ToList();


                    var PropertyNames = AllPropertyNames.Except(UpdatedProperties).Except(KeyNames).ToList();

                    PropertyNames.ForEach(PropertyName => Context.Entry(Entity).Property(PropertyName).IsModified = false);
                }

            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }



    }
}
