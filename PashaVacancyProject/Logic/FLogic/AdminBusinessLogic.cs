using AutoMapper;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Logic.Infrastucture.Concrete;

namespace PashaVacancyProject.Logic.FLogic
{
    public class AdminBusinessLogic : BaseApplicationLogic
    {
        public AdminBusinessLogic(IHttpContextAccessor httpContextAccessor, IMapper Mapper) : base(httpContextAccessor, Mapper)
        {
        }


        public async Task<ApplicationLogicResult> GetApplications()
        {
            var correctPercentage = UnitOfWork.Repository<ApplicationAnswer>()
     .GetAll()
     .GroupJoin(
         UnitOfWork.Repository<Choice>().GetAll(),
         answer => answer.ChoiceID,
         choice => choice.ID,
         (answer, choices) => new { answer, choices }
     )
     .SelectMany(
         x => x.choices.DefaultIfEmpty(), // Left join to include null choices
         (x, choice) => new { x.answer, choice }
     )
     .Join(
         UnitOfWork.Repository<Vacancy>().GetAll(),
         a => a.answer.VacancyID,
         v => v.Id,
         (a, vacancy) => new { a.answer, a.choice, vacancy.QuestionCount, vacancy.Title }
     )
     .Join(
         UnitOfWork.Repository<Applicant>().GetAll(),
         a => a.answer.ApplicantID,
         applicant => applicant.Id,
         (a, applicant) => new { a.answer, a.choice, a.QuestionCount, a.Title, applicant.Name, applicant.Surname, applicant.Email, applicant.Phone }
     )
     .Join(
         UnitOfWork.Repository<Application>().GetAll(), // Join with the Application table
         a => new { a.answer.ApplicantID, a.answer.VacancyID }, // Composite key join
         application => new { application.ApplicantID, application.VacancyID },
         (a, application) => new { a.answer, a.choice, a.QuestionCount, a.Title, a.Name, a.Surname, a.Email, a.Phone, application.RegDate } // Include RegDate
     )
     .GroupBy(a => new { a.answer.ApplicantID, a.answer.VacancyID, a.QuestionCount, a.Name, a.Surname, a.Title, a.Email, a.Phone, a.RegDate }) // Group by RegDate as well
     .Select(g => new
     {
         ApplicantId = g.Key.ApplicantID,
         VacancyId = g.Key.VacancyID,
         TotalQuestions = g.Key.QuestionCount,  // Use QuestionCount from Vacancy
         CorrectAnswers = g.Count(a => a.choice != null && a.choice.IsCorrect),
         InCorrectAnswers = g.Count(a => a.choice != null && !a.choice.IsCorrect),
         NotAnswered = (g.Key.QuestionCount - (g.Count(a => a.choice != null && a.choice.IsCorrect) + g.Count(a => a.choice != null && !a.choice.IsCorrect))),
         Name = g.Key.Name,
         Surname = g.Key.Surname,
         Email = g.Key.Email,
         Phone = g.Key.Phone,
         VacancyTitle = g.Key.Title,
         RegDate = g.Key.RegDate // Add RegDate here
     })
     .Select(result => new
     {
         result.ApplicantId,
         result.VacancyId,
         result.Name,
         result.Surname,
         result.Email,
         result.Phone,
         result.VacancyTitle,
         result.CorrectAnswers,
         result.InCorrectAnswers,
         result.NotAnswered,
         result.RegDate, // Include RegDate in the final result
         CorrectPercentage = result.TotalQuestions > 0
                             ? (double)result.CorrectAnswers / result.TotalQuestions * 100
                             : 0
     })
     .ToList();






            return new ApplicationLogicResult(true, correctPercentage);
        }

        public async Task<ApplicationLogicResult> GetDetailedApplications(int applicantID, int vacancyID)
        {
            //        var detailedInformation = UnitOfWork.Repository<VacancyQuestion>() // Assuming you have a VacancyQuestion table for the many-to-many relationship
            //.GetAll()
            //.Where(vq => vq.VacancyId == vacancyID) // Filter by VacancyID to get all related questions
            //.Join(
            //    UnitOfWork.Repository<Question>().GetAll(), // Join with the Question table
            //    vq => vq.QuestionId,
            //    question => question.Id,
            //    (vq, question) => new { vq, question } // Select both VacancyQuestion and Question
            //)
            //.GroupJoin(
            //    UnitOfWork.Repository<ApplicationAnswer>().GetAll().Where(a => a.ApplicantID == applicantID), // Get only answers from the specified applicant
            //    q => q.question.Id,
            //    answer => answer.QuestionID,
            //    (q, answers) => new { q.question, answers } // Group question with answers
            //)
            //.SelectMany(
            //    qa => qa.answers.DefaultIfEmpty(), // Left join: include questions without answers
            //    (qa, answer) => new { qa.question, answer }
            //)
            //.GroupJoin(
            //    UnitOfWork.Repository<Choice>().GetAll(),
            //    qa => qa.answer.ChoiceID,
            //    choice => choice.ID,
            //    (qa, choices) => new { qa.question, qa.answer, choices } // Keep all choices for the left join
            //)
            //.SelectMany(
            //    qa => qa.choices.DefaultIfEmpty(), // Include choices, allowing for nulls
            //    (qa, applicantChoice) => new { qa.question, qa.answer, applicantChoice }
            //)
            //.GroupJoin(
            //    UnitOfWork.Repository<Choice>().GetAll().Where(c => c.IsCorrect), // Get correct choices for each question
            //    qa => qa.question.Id,
            //    correctChoice => correctChoice.QuestionID,
            //    (qa, correctChoices) => new { qa.question, qa.answer, qa.applicantChoice, correctChoices }
            //)
            //.SelectMany(
            //    qa => qa.correctChoices.DefaultIfEmpty(), // Include correct choices
            //    (qa, correctChoice) => new { qa.question, qa.answer, qa.applicantChoice, correctChoice }
            //)
            //.Join(
            //    UnitOfWork.Repository<Vacancy>().GetAll(),
            //    qa => qa.answer.VacancyID, // Assuming the answer includes the VacancyID
            //    vacancy => vacancy.Id,
            //    (qa, vacancy) => new { qa.question, qa.answer, qa.applicantChoice, qa.correctChoice, vacancy.Title }
            //)
            //.Join(
            //    UnitOfWork.Repository<Applicant>().GetAll(),
            //    qa => qa.answer.ApplicantID,
            //    applicant => applicant.Id,
            //    (qa, applicant) => new
            //    {
            //        ApplicantId = qa.answer.ApplicantID, // Nullable to allow for unanswered questions
            //        ApplicantName = applicant.Name,
            //        ApplicantSurname = applicant.Surname,
            //        VacancyTitle = qa.Title
            //        Question = qa.question.QuestionText,           // Question text
            //        ApplicantAnswer = qa.applicantChoice != null ? qa.applicantChoice.ChoiceText : "No answer", // Applicant's answer or "No answer"
            //        CorrectAnswer = qa.correctChoice != null ? qa.correctChoice.ChoiceText : "No correct answer", // Correct answer text or indication
            //        IsAnswerCorrect = qa.applicantChoice != null && qa.applicantChoice.ID == qa.correctChoice.ID // Whether the answer is correct
            //    }
            //)
            //.ToList();
            var detailedInformation = UnitOfWork.Repository<VacancyQuestion>() // Assuming you have a VacancyQuestion table for the many-to-many relationship
            .GetAll()
            .Where(vq => vq.VacancyId == vacancyID) // Filter by VacancyID to get all related questions
            .Join(
                UnitOfWork.Repository<Question>().GetAll(), // Join with the Question table
                vq => vq.QuestionId,
                question => question.Id,
                (vq, question) => new { vq, question } // Select both VacancyQuestion and Question
            )
            .GroupJoin(
                UnitOfWork.Repository<ApplicationAnswer>().GetAll().Where(a => a.ApplicantID == applicantID), // Get only answers from the specified applicant
                q => q.question.Id,
                answer => answer.QuestionID,
                (q, answers) => new { q.question, answers } // Group question with answers
            )
            .SelectMany(
                qa => qa.answers.DefaultIfEmpty(), // Left join: include questions without answers
                (qa, answer) => new { qa.question, answer }
            )
            .GroupJoin(
                UnitOfWork.Repository<Choice>().GetAll(),
                qa => qa.answer.ChoiceID,
                choice => choice.ID,
                (qa, choices) => new { qa.question, qa.answer, choices } // Keep all choices for the left join
            )
            .SelectMany(
                qa => qa.choices.DefaultIfEmpty(), // Include choices, allowing for nulls
                (qa, applicantChoice) => new { qa.question, qa.answer, applicantChoice }
            )
            .GroupJoin(
                UnitOfWork.Repository<Choice>().GetAll().Where(c => c.IsCorrect), // Get correct choices for each question
                qa => qa.question.Id,
                correctChoice => correctChoice.QuestionID,
                (qa, correctChoices) => new { qa.question, qa.answer, qa.applicantChoice, correctChoices }
            )
            .SelectMany(
                qa => qa.correctChoices.DefaultIfEmpty(), // Include correct choices
                (qa, correctChoice) => new { qa.question, qa.answer, qa.applicantChoice, correctChoice }
            )
            .Join(
                UnitOfWork.Repository<Application>().GetAll(), // Join with the Application table to get regDate
                qa => new { qa.answer.ApplicantID, qa.answer.VacancyID }, // Composite key join
                application => new { application.ApplicantID, application.VacancyID },
                (qa, application) => new { qa.question, qa.answer, qa.applicantChoice, qa.correctChoice, application.RegDate } // Include RegDate
            )
            .Join(
                UnitOfWork.Repository<Vacancy>().GetAll(),
                qa => qa.answer.VacancyID, // Assuming the answer includes the VacancyID
                vacancy => vacancy.Id,
                (qa, vacancy) => new { qa.question, qa.answer, qa.applicantChoice, qa.correctChoice, vacancy.Title, qa.RegDate } // Include RegDate
            )
            .Join(
                UnitOfWork.Repository<Applicant>().GetAll(),
                qa => qa.answer.ApplicantID,
                applicant => applicant.Id,
                (qa, applicant) => new
                {
                    ApplicantId = qa.answer.ApplicantID, // Nullable to allow for unanswered questions
                    ApplicantName = applicant.Name,
                    ApplicantSurname = applicant.Surname,
                    VacancyTitle = qa.Title,
                    Question = qa.question.QuestionText,           // Question text
                    ApplicantAnswer = qa.applicantChoice != null ? qa.applicantChoice.ChoiceText : "No answer", // Applicant's answer or "No answer"
                    CorrectAnswer = qa.correctChoice != null ? qa.correctChoice.ChoiceText : "No correct answer", // Correct answer text or indication
                    IsAnswerCorrect = qa.applicantChoice != null && qa.applicantChoice.ID == qa.correctChoice.ID, // Whether the answer is correct
                    RegDate = qa.RegDate // Registration date from the Application table
                }
            )
            .ToList();




            return new ApplicationLogicResult(true, detailedInformation);

        }


    }
}
