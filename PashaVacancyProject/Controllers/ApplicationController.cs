using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaVacancyProject.Logic.DTO;
using PashaVacancyProject.Logic.FLogic;
using PashaVacancyProject.Logic.ViewModel;

namespace PashaVacancyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        public readonly ApplicationBusinessLogic Logic;

        public ApplicationController(ApplicationBusinessLogic logic)
        {
            Logic = logic;
        }

      


        [HttpPost("apply")]
        public async Task<JsonResult> Apply(ApplicantRM applicantRM)
        {
            return await Logic.AddAplication(applicantRM);
        }

        [HttpPost("get-question")]
        public async Task<JsonResult> GetQuestion(QuestionVM question)
        {
            return await Logic.GetGuestion(question);
        }
        [HttpPost("answer")]
        public async Task<JsonResult> Answer(AnswerRM answer)
        {
            return await Logic.AnswerQuestion(answer);
        }

        [HttpPost("sendcv")]
        public async Task<JsonResult> SendCV( [FromForm] FileRM file)
        {
            return await Logic.SaveCV(file);
        }

        [HttpPost("DownloadCV")]
        public async Task<JsonResult> DownloadCV(int vacancyID, int applicantID)
        {
            return await Logic.DownloadCV(vacancyID,applicantID);
        }
    }
}
