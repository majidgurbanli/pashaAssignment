using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class BaseApplicationLogic
    {
        public readonly IHttpContextAccessor HttpContextAccessor;
        public readonly IUnitOfWork UnitOfWork;
        public readonly IMapper Mapper;
        //public readonly IWorkPortalService WorkPortalService;
        public BaseApplicationLogic(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            HttpContextAccessor = httpContextAccessor;
            Mapper = mapper;
            //WorkPortalService = centralizedPortalService;
            UnitOfWork = new UnitOfWork(1);
        }



        public ApplicationLogicResult<T> LogicResult<T>(bool Success)
        {
            return new ApplicationLogicResult<T>(Success);
        }
        public ApplicationLogicResult<T> LogicResult<T>(bool Success, T Data)
        {
            return new ApplicationLogicResult<T>(Success, Data, null);
        }
        public ApplicationLogicResult<T> LogicGenericResult<T>(bool Success, T Data, string Message = null)
        {
            return new ApplicationLogicResult<T>(Success, Data, Message);
        }



        public ApplicationLogicResult LogicResult(bool Success = false, object Data = null, string Message = null)
        {
            return new ApplicationLogicResult(Success, Data, Message);
        }
        public ApplicationLogicResult LogicResult(bool Success = false, object Data = null)
        {
            return new ApplicationLogicResult(Success, Data, null);
        }

        public ApplicationLogicResult LogicResult(bool Success = false, string Message = null)
        {
            return new ApplicationLogicResult(Success, null, Message);
        }
        public ApplicationLogicResult LogicResult(FileContentResult ContentResult)
        {
            return new ApplicationLogicResult(ContentResult);
        }
        public ApplicationLogicResult LogicSuccsesResult(object Data = null)
        {
            return new ApplicationLogicResult(true, Data, null);
        }
        public string UserID
        {
            get
            {

                var authorization = this.HttpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
                var token = authorization?.Split(" ")[1];
                var tokenHandler = new JwtSecurityTokenHandler();
                var decryptedToken = tokenHandler.ReadJwtToken(token);
                var userID = decryptedToken.Claims.Where(z => z.Type == "userID").FirstOrDefault().Value;
                return userID;
            }
        }
    }
}
