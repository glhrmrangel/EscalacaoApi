using AutoMapper;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;

namespace EscalacaoApi.Profiles;

public class JogadorProfile : Profile
{
    public JogadorProfile() 
    {
        CreateMap<CreateJogadorDto, Jogador>();
        CreateMap<UpdateJogadorDto, Jogador>();
        CreateMap<Jogador, UpdateJogadorDto>();
        CreateMap<Jogador, ReadJogadorDto>().ForMember(jogadorDto => jogadorDto.ReadTimeDto, opt => opt.MapFrom(jogador => jogador.Time));
    }
}
