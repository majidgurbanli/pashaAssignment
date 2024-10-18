using AutoMapper;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Logic.DTO;
using PashaVacancyProject.Logic.Infrastucture.Concrete;

namespace PashaVacancyProject.Logic.FLogic
{
    public class ChoiceBusinessLogic : BaseApplicationLogic
    {
        public ChoiceBusinessLogic(IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor, mapper)
        {
        }

        public async Task<ApplicationLogicResult<ChoiceParentReM>> AddChoice(ChoiceParentReM choiceParent)
        {
            var question = UnitOfWork.Repository<Question>().Find(x => x.Id == choiceParent.QuestionID).FirstOrDefault();
            if(choiceParent.Choices.Count != 3)
            {
                return LogicGenericResult<ChoiceParentReM>(false, null, "3 ədəd variant olmalıdır");
            }
            if(question == null)
            {
                return LogicGenericResult<ChoiceParentReM>(false, null, "Qeyd edilən sual yoxdur");
            }
            var correctChoices = choiceParent.Choices.Where(x => x.IsCorrect == true).Count();
            if (correctChoices > 1)
            {
                return LogicGenericResult<ChoiceParentReM>(false, null, "Hər sualda 1 ədəd doğru cavab ola bilər");
            }
            List<Choice> choices = new List<Choice>();
            for(int i = 0; i < choiceParent.Choices.Count; i++)
            {
                Choice ch = new Choice()
                {
                    ChoiceText = choiceParent.Choices[i].ChoiceText,
                    IsCorrect = choiceParent.Choices[i].IsCorrect,
                    QuestionID = choiceParent.QuestionID

                };
                choices.Add(ch);

            }
            UnitOfWork.Repository<Choice>().AddOrUpdateRange(choices);
            UnitOfWork.SaveChanges();
            return LogicGenericResult<ChoiceParentReM>(true, choiceParent);
        }


    }
}
