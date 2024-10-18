using PashaVacancyProject.Domain.DInfrastucture;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Logic.Infrastucture.Paiging;
using System.Linq.Expressions;

namespace PashaVacancyProject.Logic.ViewModel
{
    public class VacancyVM
    {
        //Tehsil
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


    }


    public class VacancyFilterVM
    {

        public string? Title { get; set; }



    }

    public class VacancyFilterPagingVM : PaigingViewModels<VacancyVM, Vacancy>
    {


        public VacancyFilterVM? Filter { set; get; }

        internal Expression<Func<Vacancy, bool>> FilterExpression
        {
            get
            {


                BaseExpression = x => true;
                
                if (this.Filter != null)
                {
                    Expressions = new List<Expression<Func<Vacancy, bool>>>();
                    if (!string.IsNullOrEmpty(Filter.Title))
                    {
                        Expressions.Add(x => x.Title == Filter.Title);
                    }

                }
               

                return BaseExpression.CombineExpression(Expressions);
            }
        }



    }
}
