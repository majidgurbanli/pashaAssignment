using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaVacancyProject.Logic.FLogic;

namespace PashaVacancyProject.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly AdminBusinessLogic Logic;

        public AdminController(AdminBusinessLogic logic)
        {
            Logic = logic;
        }

        [HttpPost("get-applications")]
        public async Task<JsonResult> GetApplication()
        {
          return await Logic.GetApplications();
        
            //return await Logic.GetAllVacancies(vacancyFilterPagingVM);
        }


        [HttpPost("get-detailed-applications")]
        public async Task<JsonResult> GetDetailedApplication(int applicantID, int vacancyID)
        {
            return await Logic.GetDetailedApplications(applicantID, vacancyID);

            //return await Logic.GetAllVacancies(vacancyFilterPagingVM);
        }
    }
}
