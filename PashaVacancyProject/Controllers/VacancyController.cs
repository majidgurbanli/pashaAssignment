using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaVacancyProject.Logic.FLogic;
using PashaVacancyProject.Logic.ViewModel;

namespace PashaVacancyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        public readonly VacancyBusinessLogic Logic;
        public VacancyController(VacancyBusinessLogic Logic)
        {
                this.Logic = Logic;
        }
        [HttpPost("get-vacancy")]
        public async Task<JsonResult> GetVacancy(VacancyFilterPagingVM vacancyFilterPagingVM)
        {
            return await Logic.GetAllVacancies(vacancyFilterPagingVM);
        }
    }
}
