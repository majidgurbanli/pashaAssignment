using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Logic.DTO;
using PashaVacancyProject.Logic.Infrastucture.Concrete;

namespace PashaVacancyProject.Logic.FLogic
{
    public class QuestionBusinessLogic: BaseApplicationLogic
    {
        public QuestionBusinessLogic(IHttpContextAccessor httpContextAccessor, IMapper Mapper) : base(httpContextAccessor, Mapper)
        {
        }
        public async Task<ApplicationLogicResult<QuestionRM>> AddQuestion(QuestionRM question)
        {
            Question q = new Question()
            {
                QuestionText = question.QuestionText
            };
            UnitOfWork.Repository<Question>().AddOrUpdate(q);
           string errorMessage =  UnitOfWork.SaveChangesDetached();
            if(errorMessage != string.Empty)
            {
                return LogicGenericResult<QuestionRM>(false, question);
            }
            question.ID = q.Id;
            return LogicGenericResult<QuestionRM>(true, question);
        }

        public async Task<ApplicationLogicResult> DeleteQuestion(int questionID)
        {
            var question = UnitOfWork.Repository<Question>().Find(x => x.Id == questionID).FirstOrDefault();
            if(question == null)
            {
                return LogicResult(false, null, "Sual tapılmadı!");
            }
            UnitOfWork.Repository<Question>().Delete(question);
            UnitOfWork.SaveChanges();
            return LogicResult(true, null, "Uğurla Silindi!");

        }

        public async Task<ApplicationLogicResult<QuestionRM>> UpdateQuestion(QuestionRM question)
        {
            Question q = new Question()
            {
                Id = question.ID.HasValue ? question.ID.Value : 0,
                QuestionText = question.QuestionText
            };
            UnitOfWork.Repository<Question>().AddOrUpdate(q);
            string errorMessage = UnitOfWork.SaveChangesDetached();
            if (errorMessage != string.Empty)
            {
                return LogicGenericResult<QuestionRM>(false, question);
            }
      
            return LogicGenericResult<QuestionRM>(true, question);
        }
        public async Task<ApplicationLogicResult<List<QuestionReM>>> GetAllGuestion()
        {
            var questions =  await UnitOfWork.Repository<Question>().GetAll().Select(z => new QuestionReM()
            {
                Id = z.Id,
                Text = z.QuestionText,
                Choices = z.Choices.Select(t=>new ChoiceReM()
                {
                    ID = t.ID,
                    ChoiceText = t.ChoiceText,
                    IsCorrect = t.IsCorrect
                }).ToList()

            }).ToListAsync();
            return LogicGenericResult<List<QuestionReM>>(true, questions);
        }

        public async Task<ApplicationLogicResult> AddQuestionToVacancy(int questionID, int vacancyID)
        {
            var questionVacancy = new VacancyQuestion()
            {
                QuestionId = questionID,
                VacancyId = vacancyID
            };
            UnitOfWork.Repository<VacancyQuestion>().Add(questionVacancy);
            UnitOfWork.SaveChanges();
            return LogicResult(true, null);
        }


    }
}
