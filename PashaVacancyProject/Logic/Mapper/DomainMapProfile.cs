using AutoMapper;
using PashaVacancyProject.Domain.Entities;
using PashaVacancyProject.Logic.DTO;

namespace PashaVacancyProject.Logic.Mapper
{
    public class DomainMapProfile : Profile
    {
        public DomainMapProfile()
        {
            this.CreateMap<ApplicantRM, Applicant>();
        }
      }
}
