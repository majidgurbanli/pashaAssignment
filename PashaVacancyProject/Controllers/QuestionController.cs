using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaVacancyProject.Logic.DTO;
using PashaVacancyProject.Logic.FLogic;

namespace PashaVacancyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public readonly QuestionBusinessLogic Logic;

        public QuestionController(QuestionBusinessLogic logic)
        {
            Logic = logic;
        }
        [HttpPost]
        public async Task<JsonResult> AddQuestion(QuestionRM question)
        {
            return await Logic.AddQuestion(question);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteQuestion(int questionID)
        {
            return await Logic.DeleteQuestion(questionID);
        }

        [HttpPut]
        public async Task<JsonResult> UpdateQuestion(QuestionRM question)
        {
            return await Logic.UpdateQuestion(question);
        }
        [HttpGet]
        public async Task<JsonResult> GetAllQuestion()
        {
            return await Logic.GetAllGuestion();
        }

        [HttpGet("add-question-to-vacancy")]
        public async Task<JsonResult> AddQuestionToVacancy(int questionID, int vacancyID)
        {
            return await Logic.AddQuestionToVacancy(questionID, vacancyID);
        }
    }
}
