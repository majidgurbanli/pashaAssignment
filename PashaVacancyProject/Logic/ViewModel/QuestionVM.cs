using PashaVacancyProject.Domain.DInfrastucture;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Logic.Infrastucture.Paiging;
using System.Linq.Expressions;

namespace PashaVacancyProject.Logic.ViewModel
{
    public class QuestionVM
    {
        //Tehsil
        public int ID { get; set; }
        public string QuestionText { get; set; }

        public int ApplicantID { get; set; }
        public int VacancyID { get; set; }


    }


    public class QuestionFilterVM
    {

        public string? Title { get; set; }



    }

    public class QuestionFilterPagingVM : PaigingViewModels<QuestionVM, Question>
    {


        public VacancyFilterVM? Filter { set; get; }

        internal Expression<Func<Question, bool>> FilterExpression
        {
            get
            {


                BaseExpression = x => true;

                if (this.Filter != null)
                {
                    Expressions = new List<Expression<Func<Question, bool>>>();
                    
                    //if (!string.IsNullOrEmpty(Filter.Title))
                    //{
                    //    Expressions.Add(x => x.Title == Filter.Title);
                    //}

                }


                return BaseExpression.CombineExpression(Expressions);
            }
        }



    }
}
