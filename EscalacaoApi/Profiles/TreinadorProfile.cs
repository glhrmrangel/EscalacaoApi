using AutoMapper;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;

namespace EscalacaoApi.Profiles;

public class TreinadorProfile : Profile
{
    public TreinadorProfile() 
    {
        CreateMap<CreateTreinadorDto, Treinador>();
        CreateMap<UpdateTreinadorDto, Treinador>();
        CreateMap<Treinador, UpdateTreinadorDto>();
        CreateMap<Treinador, ReadTreinadorDto>().ForMember(treinadorDto => treinadorDto.ReadTimeDto, opt => opt.MapFrom(treinador => treinador.Time));
    }
}
