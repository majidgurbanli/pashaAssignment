using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PashaVacancyProject.Logic.DTO;
using PashaVacancyProject.Logic.FLogic;

namespace PashaVacancyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserBusinessLogic Logic;

        public AuthController(UserBusinessLogic logic)
        {
            Logic = logic;
        }

        [HttpPost("register")]
        public async Task<JsonResult> Register(UserReM userRM)
        {
            return Logic.RegisterUser(userRM.Username, userRM.Password);
             
        }

        [HttpPost("login")]
        public async Task<JsonResult> Login(UserReM userRM)
        {
            return Logic.ValidateUser(userRM.Username, userRM.Password);

        }
    }
}
