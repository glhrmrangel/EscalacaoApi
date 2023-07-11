using AutoMapper;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;

namespace EscalacaoApi.Profiles;

public class TimeProfile : Profile
{
    public TimeProfile() 
    {
        CreateMap<CreateTimeDto, Time>();
        CreateMap<UpdateTimeDto, Time>();
        CreateMap<Time, UpdateTimeDto>();
        CreateMap<Time, ReadTimeDto>();
    }
}
