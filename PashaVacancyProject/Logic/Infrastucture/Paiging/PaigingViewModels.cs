using PashaVacancyProject.Domain.DInfrastucture;
using System.Linq.Expressions;

namespace PashaVacancyProject.Logic.Infrastucture.Paiging
{
    public class PaigingViewModels<DestinationVM, DestinationDM> where DestinationVM : class
    {


        internal Expression<Func<DestinationDM, bool>> BaseExpression { set; get; }

        internal List<Expression<Func<DestinationDM, bool>>> Expressions = null;

        internal Expression<Func<DestinationDM, bool>> NullExpression = null;
        internal Func<DestinationDM, bool> BaseSearchFunc { set; get; }
        internal Expression<Func<DestinationDM, bool>> BaseFuncExpression { set; get; }

       


        internal List<Func<DestinationDM, bool>> SearchFunc = null;



        internal Func<DestinationDM, bool> NullSearchFunc = null;


        /// <summary>
        /// 
        /// </summary>
        public TablePaging Paging { set; get; } = new TablePaging()
        {
            ItemSizePerPage = 100
        };

        /// <summary>
        /// 
        /// </summary>
        public List<DestinationVM> DataSource { set; get; }

        /// <summary>
        /// 
        /// </summary>
        internal IQueryable<DestinationDM> _IQueryableSourceWithOutOrder;

        internal IQueryable<DestinationDM> IQueryableSourceWithOutOrder
        {
            get
            {

                return _IQueryableSourceWithOutOrder == null ? null : _IQueryableSourceWithOutOrder.Skip((Paging.SelectedPageIndex - 1) * Paging.ItemSizePerPage)
                                        .Take(Paging.ItemSizePerPage);
            }

            set
            {
                if (value != null) { Paging.DataCount = value.Count(); }
                _IQueryableSourceWithOutOrder = value;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        internal IQueryable<DestinationDM> _IQueryableSource;

        /// <summary>
        /// 
        /// </summary>
        internal IQueryable<DestinationDM> IQueryableSource
        {
            get
            {

                return _IQueryableSource.OrderByProperty(Paging.Order, Paging.Sort)
                                        .Skip((Paging.SelectedPageIndex - 1) * Paging.ItemSizePerPage)
                                        .Take(Paging.ItemSizePerPage);
            }

            set
            {
                try
                {
                    if (value != null) { Paging.DataCount = value.Count(); }
                    _IQueryableSource = value;

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal IQueryable<DestinationDM> _IQueryableSourceOnlySorted;
        /// <summary>
        /// 
        /// </summary>
        internal IQueryable<DestinationDM> IQueryableSourceOnlySorted
        {
            get => _IQueryableSourceOnlySorted.OrderByProperty(Paging.Order, Paging.Sort)
                                        .Skip((Paging.SelectedPageIndex - 1) * Paging.ItemSizePerPage)
                                        .Take(Paging.ItemSizePerPage);
            set
            {

                _IQueryableSourceOnlySorted = value;
            }

        }



    }
}
