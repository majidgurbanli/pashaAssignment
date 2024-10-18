using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaVacancyProject.Logic.DTO;
using PashaVacancyProject.Logic.FLogic;

namespace PashaVacancyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoiceController : ControllerBase
    {

        public readonly ChoiceBusinessLogic Logic;

        public ChoiceController(ChoiceBusinessLogic logic)
        {
            Logic = logic;
        }
        [HttpPost]
        public async Task<JsonResult> AddChoice(ChoiceParentReM choiceParent)
        {
           return await Logic.AddChoice(choiceParent);
        }
    }
}
