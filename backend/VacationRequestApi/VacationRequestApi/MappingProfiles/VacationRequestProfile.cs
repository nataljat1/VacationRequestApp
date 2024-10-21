using AutoMapper;
using VacationRequestApi.Data.Models;
using VacationRequestApi.VacationRequestsDto;

namespace VacationRequestApi.MappingProfiles
{
    public class VacationRequestProfile : Profile
    {
        public VacationRequestProfile()
        {
            // Map between VacationRequest and its DTOs
            CreateMap<VacationRequest, VacationRequestDto>();
            CreateMap<CreateVacationRequestDto, VacationRequest>();
        }
    }
}