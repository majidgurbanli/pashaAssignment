using AutoMapper;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Logic.Infrastucture.Concrete;
using PashaVacancyProject.Logic.ViewModel;

namespace PashaVacancyProject.Logic.FLogic
{
    public class VacancyBusinessLogic : BaseApplicationLogic
    {
        public VacancyBusinessLogic(IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor, mapper)
        {
        }

        public async Task<ApplicationLogicResult<List<VacancyVM>>> GetAllVacancies(VacancyFilterPagingVM vacancyFilterPagingVM)
        {
            vacancyFilterPagingVM.IQueryableSource = UnitOfWork.Repository<Vacancy>().Find(vacancyFilterPagingVM.FilterExpression);

            var vacancyList = vacancyFilterPagingVM.IQueryableSource
                .Select(s => new VacancyVM
                {
                  ID = s.Id,
                  Title = s.Title,
                  Description = s.Description,

                })
                .ToList();
            return LogicResult(true, vacancyList);
        }
    }
}
