using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PashaVacancyProject.Domain.DInfrastucture.DAbstract;
using PashaVacancyProject.Domain.Entities.Base;

namespace PashaVacancyProject.Domain.DInfrastucture
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoxDbContent DbContext;
        private bool disposed = false;

        public int? UserID { set; get; }

        public UnitOfWork(int UserID)
        {
            DbContext = new BoxDbContent();
            DbContext.DetachAllEntities();
            this.UserID = UserID;
        }
        public void SaveChanges()
        {
            EntitySetting();
            DbContext.SaveChanges();

        }

        //Tranzaksiyalarin nezere alinmasi
        public void BeginTransaction()
        {
            DbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            DbContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            DbContext.Database.RollbackTransaction();
        }


        public void Detach()
        {
            DbContext.DetachAllEntities();
        }
        public string SaveChangesDetached()
        {
            string DatabaseErrorMessage = string.Empty;
            try
            {
                EntitySetting();
                DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                DbContext.SaveChanges();
                DbContext.ChangeTracker.AutoDetectChangesEnabled = true;

            }
            catch (Exception Ex)
            {

                while (Ex.InnerException != null)
                {
                    Ex = Ex.InnerException;
                }

                DatabaseErrorMessage = Ex.Message;
            }
            DbContext.DetachAllEntities();
            return DatabaseErrorMessage;

        }
        public async Task<string> SaveChangesDetachedAsync()
        {


            string DatabaseErrorMessage = string.Empty;
            try
            {
                EntitySetting();
                DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                await DbContext.SaveChangesAsync();
                DbContext.ChangeTracker.AutoDetectChangesEnabled = true;

            }
            catch (Exception Ex)
            {

                while (Ex.InnerException != null)
                {
                    Ex = Ex.InnerException;
                }

                DatabaseErrorMessage = Ex.Message;
            }
            DbContext.DetachAllEntities();
            return DatabaseErrorMessage;



        }
        private void EntitySetting()
        {

            var AddEdEntity = DbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            var ModifiedEntity = DbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();

            foreach (EntityEntry Entry in AddEdEntity)
            {
                var BaseEntity = Entry.Entity as BaseEntity;
                if (BaseEntity != null)
                {
                    DbContext.Entry<BaseEntity>(BaseEntity).Property(x => x.EdituID).IsModified = false;
                    DbContext.Entry<BaseEntity>(BaseEntity).Property(x => x.EditDate).IsModified = false;

                    BaseEntity.EdituID = null;
                    BaseEntity.EditDate = null;
                    BaseEntity.ReguID = UserID;
                }
            }

            foreach (EntityEntry Entry in ModifiedEntity)
            {

                var BaseEntity = Entry.Entity as BaseEntity;
                if (BaseEntity != null)
                {
                    DbContext.Entry(BaseEntity).Property(x => x.ReguID).IsModified = false;
                    DbContext.Entry(BaseEntity).Property(x => x.RegDate).IsModified = false;
                    DbContext.Entry(BaseEntity).Property(x => x.EdituID).IsModified = true;
                    DbContext.Entry(BaseEntity).Property(x => x.EditDate).IsModified = true;

                    BaseEntity.EditDate = DateTime.Now;
                    BaseEntity.EdituID = UserID;
                }
            }

        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                DbContext.Dispose();

            }
            disposed = true;
        }


        IGenericRepository<T> IUnitOfWork.Repository<T>()
        {
            return new GenericRepository<T>(DbContext);
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
