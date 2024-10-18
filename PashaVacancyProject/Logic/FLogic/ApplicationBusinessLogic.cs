using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Logic.DTO;
using PashaVacancyProject.Logic.Infrastucture.Concrete;
using PashaVacancyProject.Logic.ViewModel;

namespace PashaVacancyProject.Logic.FLogic
{
    public class ApplicationBusinessLogic : BaseApplicationLogic
    {
        public ApplicationBusinessLogic(IHttpContextAccessor httpContextAccessor, IMapper Mapper) : base(httpContextAccessor, Mapper)
        {
        }

        public async Task<ApplicationLogicResult<int>> AddAplication(ApplicantRM applicantRM)
        {

            var vacancy = await UnitOfWork.Repository<Vacancy>().Find(x=>x.Id == applicantRM.VacancyID).FirstOrDefaultAsync();
            if(vacancy == null)
            {
                return LogicGenericResult<int>(false, 0, "Qeyd olunan vakansiya bazada yoxdur");
            }
            var applicant = GetMapperEntity<Applicant>(applicantRM) as Applicant;
            applicant.Vacancies.Add(vacancy);
            UnitOfWork.Repository<Applicant>().AddOrUpdate(applicant);
            string errorMessage = UnitOfWork.SaveChangesDetached();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return LogicGenericResult<int>(false, 0, "Müraciət qeydiyyatı zamanı xəta!");
            }
              
            return LogicResult(true, applicant.Id);
        }

        public async Task<ApplicationLogicResult<QuestionDTO>> GetGuestion(QuestionVM questionVM)
        {
            var vacancy = await UnitOfWork.Repository<Application>().Find(x => x.ApplicantID == questionVM.ApplicantID && x.VacancyID == questionVM.VacancyID).Select(t => t.Vacancy).FirstOrDefaultAsync();
            int? questionCount = vacancy?.QuestionCount;
            if(questionCount == null)
            {
                return LogicGenericResult<QuestionDTO>(false, null, "Müraciət qeydiyyatı zamanı xəta!");
            }
            var applicationAnswers = await UnitOfWork.Repository<ApplicationAnswer>().Find(x => x.ApplicantID == questionVM.ApplicantID && x.VacancyID == questionVM.VacancyID).ToListAsync();
            if (applicationAnswers.Count <= questionCount -1)
            {
                MakePreviousAnswerInActive(questionVM.ApplicantID, questionVM.VacancyID);
                QuestionDTO question = GenerateQuestion(questionVM.ApplicantID, questionVM.VacancyID);
                if (applicationAnswers.Count == questionCount-1)
                {
                    question.isLast = true;
                }
                return LogicGenericResult<QuestionDTO>(true, question);
            }
            
            return LogicGenericResult<QuestionDTO>(false, null,"Sual sayı bitdi");



        }
        public async Task<ApplicationLogicResult> AnswerQuestion(AnswerRM answerRM)
        {
            var answer = UnitOfWork.Repository<ApplicationAnswer>().Find(x => x.ApplicantID == answerRM.ApplicantID && x.QuestionID == answerRM.QuestionID && x.VacancyID == answerRM.VacancyID && x.IsActive).FirstOrDefault();
            if(answer == null)
            {
                return LogicResult(false, null, "Qeyd edilən sual deaktivdir!");
            }
            answer.AnswerTime = DateTime.Now;
            answer.IsActive = false;
            if (answer.StartTime.AddMinutes(1) <= DateTime.Now)
            {
                UnitOfWork.SaveChanges();
                return LogicResult(false, null, "Bu sual üçün vaxtınız bitdi!");
            }
            else
            {
                var isCorrectChoice = await UnitOfWork.Repository<Choice>().Find(x => x.ID == answerRM.ChoiceID && x.QuestionID == answerRM.QuestionID).FirstOrDefaultAsync();
                if(isCorrectChoice == null)
                {
                    return LogicResult(false, null, "Qeyd edilən variant bu sualda mövcüd deyil");
                }
                
                answer.ChoiceID = answerRM.ChoiceID;

            }
            UnitOfWork.Repository<ApplicationAnswer>().Update(answer);
            UnitOfWork.SaveChanges();
            return LogicResult(true, null);




        }

        public async Task<ApplicationLogicResult> SaveCV(FileRM fileRM)
        {


            string originalExtension = Path.GetExtension(fileRM.File.FileName);
            string randomFileName = $"{Guid.NewGuid()}{originalExtension}";
            string filePath = Path.Combine("fileServer", randomFileName);

            // Ensure the upload directory exists
            Directory.CreateDirectory("fileServer");

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileRM.File.CopyToAsync(stream);
            }
            UnitOfWork.Repository<FileEntity>().Add(new FileEntity()
            {
                FileName = randomFileName,
                VacancyID = fileRM.VacancyID,
                ApplicantID = fileRM.ApplicantID,


            });
            UnitOfWork.SaveChanges();

            return LogicResult(true, null);
        }
        public async Task<ApplicationLogicResult> DownloadCV(int vacancyID, int applicantID)
        {
            var fileName = UnitOfWork.Repository<FileEntity>().Find(x=>x.VacancyID == vacancyID && x.ApplicantID == applicantID).Select(t=>t.FileName).FirstOrDefault();
            if (fileName == null || fileName == string.Empty)
            {
                return LogicResult(false, null, "CV tapılmadı");
            }


           var file = await ReadFileAsByteArrayAsync(fileName);

            return new ApplicationLogicResult(true, file);
        }

        public static string GenerateRandomFileName(string extension)
        {
            // Generate a random GUID and combine it with the desired extension
            string randomFileName = $"{Guid.NewGuid()}{extension}";
            return randomFileName;
        }
        private void MakePreviousAnswerInActive(int applicantID, int vacancyID)
        {
            var answers = UnitOfWork.Repository<ApplicationAnswer>().Find(x => x.ApplicantID == applicantID && x.VacancyID == vacancyID && x.IsActive).ToList();
            for(int i= 0; i < answers.Count; i++)
            {
                answers[i].IsActive = false;
            }
            UnitOfWork.Repository<ApplicationAnswer>().AddOrUpdateRange(answers);
            UnitOfWork.SaveChanges();

        }

        private async Task<byte[]> ReadFileAsByteArrayAsync(string fileName)
        {
            string filePath = Path.Combine("fileServer", fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{fileName}' was not found.");
            }

            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);

            return fileBytes;
        }

        private QuestionDTO GenerateQuestion(int applicantID, int vacancyID)
        {
            var existedQuestions = UnitOfWork.Repository<ApplicationAnswer>().Find(x => x.ApplicantID == applicantID && x.VacancyID == vacancyID ).Select(z=>z.QuestionID).ToList();

            var question = UnitOfWork.Repository<Question>()
                 .Find(z => !existedQuestions.Contains(z.Id)) 
                 .OrderBy(z => Guid.NewGuid())
                 .Select(y=>new QuestionDTO()
                 {
                     ID = y.Id,
                     QuestionText = y.QuestionText,
                     Choices = y.Choices.Select(t=>new ChoiceDTO()
                     {
                         ID= t.ID,
                         ChoiceText = t.ChoiceText
                     }).ToList()
                 })
                 .FirstOrDefault();
            if(question == null)
            {
                return null;
            }

            ApplicationAnswer appAnswer = new ApplicationAnswer()
            {
                ApplicantID = applicantID,
                VacancyID = vacancyID,
                QuestionID = question.ID,
                StartTime = DateTime.Now,
                IsActive = true

            };
            UnitOfWork.Repository<ApplicationAnswer>().Add(appAnswer);
           string error =  UnitOfWork.SaveChangesDetached();
            return question;
            

        }



        public object GetMapperEntity<T>(object destination)
        {
            T mapper = Mapper.Map<T>(destination);
            return mapper;
        }

        public ApplicationLogicResult GetExceptionResponse(string message)
        {
            return new ApplicationLogicResult(false, null, message);
        }


    }
}
