using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PashaVacancyProject.Domain.DInfrastucture;
using PashaVacancyProject.Domain.DInfrastucture.DAbstract;
using PashaVacancyProject.Logic.Infrastucture.Concrete;

namespace PashaVacancyProject.Logic.FLogic
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
       
    }
}
