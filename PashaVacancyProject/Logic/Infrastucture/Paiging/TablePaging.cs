namespace PashaVacancyProject.Logic.Infrastucture.Paiging
{
    public class TablePaging
    {
        public int ItemSizePerPage { get; set; } = 5;
        public int PageCount { get; set; } = 7;
        public int? DataCount { get; set; } = 0;

        public int? PagingCount { get; set; } = 0;
        public decimal? SumAmount { get; set; } = 0;

        public int SelectedPageIndex { get; set; } = 1;

        public SortOrderCust Sort { get; set; } = SortOrderCust.Desc;
        public string? Order { get; set; }
    }


    public enum SortOrderCust
    {
        Asc, Desc
    };
}
