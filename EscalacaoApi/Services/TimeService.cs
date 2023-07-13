using AutoMapper;
using EscalacaoApi.Data;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;

namespace EscalacaoApi.Services;

/// <summary>
/// Camada de Serviço criada para centralizar a lógica referente às chamadas de API de Time
/// </summary>
public class TimeService
{
    private JogadorContext _context;
    private IMapper _mapper;

    public TimeService(JogadorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Time InsereTime(CreateTimeDto dto)
    {
        Time time = _mapper.Map<Time>(dto);
        _context.Times.Add(time);
        _context.SaveChanges();

        return time;
    }

    public IEnumerable<ReadTimeDto> RecuperaTimes(int skip, int take)
    {
        return _mapper.Map<List<ReadTimeDto>>(_context.Times.Skip(skip).Take(take).ToList());
    }

    public Time? BuscaTimePorId(int id) 
    {
        return _context.Times.FirstOrDefault(time => time.Id == id);
    }

    public ReadTimeDto RecuperaTime(Time time)
    {
        return _mapper.Map<ReadTimeDto>(time);
    }

    public void AtualizaTime(UpdateTimeDto timeDto, Time time)
    {
        _mapper.Map(timeDto, time);
        _context.SaveChanges(); 
    }

    public void DeletaTime(Time time)
    {
        _context.Remove(time);
        _context.SaveChanges(); 
    }
}
