using AutoMapper;
using EscalacaoApi.Data;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;

namespace EscalacaoApi.Services;

/// <summary>
/// Camada de Serviço criada para centralizar a lógica referente às chamadas de API de Treinador
/// </summary>
public class TreinadorService
{
    private JogadorContext _context;
    private IMapper _mapper;

    public TreinadorService(JogadorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Treinador InsereTreinador(CreateTreinadorDto dto)
    {
        Treinador treinador = _mapper.Map<Treinador>(dto);
        _context.Treinadores.Add(treinador);
        _context.SaveChanges();

        return treinador;
    }

    public IEnumerable<ReadTreinadorDto> RecuperaTreinadores()
    {
        return _mapper.Map<List<ReadTreinadorDto>>(_context.Treinadores.ToList());
    }

    public Treinador? BuscaTreinadorPorId(int id) 
    {
        return _context.Treinadores.FirstOrDefault(treinador => treinador.Id == id);
    }

    public ReadTreinadorDto RecuperaTreinador(Treinador treinador)
    {
        return _mapper.Map<ReadTreinadorDto>(treinador);
    }

    public void AtualizaTreinador(UpdateTreinadorDto treinadorDto, Treinador treinador)
    {
        _mapper.Map(treinadorDto, treinador);
        _context.SaveChanges();
    }

    public void DeletaTreinador(Treinador treinador)
    {
                _context.Remove(treinador);
        _context.SaveChanges();
    }
}
