using AutoMapper;
using EscalacaoApi.Data;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace EscalacaoApi.Services;

/// <summary>
/// Camada de Serviço criada para centralizar a lógica referente às chamadas de API de Jogador
/// </summary>
public class JogadorService
{
    private JogadorContext _context;
    private IMapper _mapper;

    public JogadorService(JogadorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Jogador InsereJogador(CreateJogadorDto dto)
    {
        Jogador jogador = _mapper.Map<Jogador>(dto);
        _context.Jogadores.Add(jogador);
        _context.SaveChanges();

        return jogador;
    }

    public IEnumerable<ReadJogadorDto> RecuperaJogadores(int skip, int take, int? timeId)
    {
        if (timeId == null)
            return _mapper.Map<List<ReadJogadorDto>>(_context.Jogadores.Skip(skip).Take(take).ToList());

            return _mapper.Map<List<ReadJogadorDto>>(_context.Jogadores.Skip(skip).Take(take).Where(jogador => jogador.TimeId == timeId).ToList());
    }

    /// <summary>
    /// Método para a busca de todos os Jogadores.
    /// </summary>
    /// <param name="jogador">Recebe um objeto do tipo Jogador</param>
    /// <returns></returns>
    public ReadJogadorDto RecuperaJogador(Jogador jogador)
    {
        return _mapper.Map<ReadJogadorDto>(jogador);
    }

    public Jogador? BuscaJogadorPorId(int id)
    {
        Jogador jogador = _context.Jogadores.FirstOrDefault(jogador => jogador.Id == id);

        return jogador;
    }

    /// <summary>
    /// Método que efetua a deleção do Jogador.
    /// </summary>
    /// <param name="jogador"></param>
    public void DeletaJogador(Jogador jogador)
    {
        _context.Remove(jogador);
        _context.SaveChanges();
    }

    /// <summary>
    /// Método para a atualização dos campos de Jogador
    /// </summary>
    /// <param name="jogadorDto"></param>
    /// <param name="jogador"></param>
    internal void AtualizaJogador(UpdateJogadorDto jogadorDto, Jogador jogador)
    {
        _mapper.Map(jogadorDto, jogador);
        _context.SaveChanges();
    }
}
