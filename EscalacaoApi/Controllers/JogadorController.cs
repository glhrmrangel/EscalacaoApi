using AutoMapper;
using EscalacaoApi.Data;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace EscalacaoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JogadorController : ControllerBase
{
    private JogadorContext _context;
    private IMapper _mapper;

    public JogadorController(JogadorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Realiza a inserção de um Jogador no banco de dados.
    /// </summary>
    /// <param name="jogadorDto">Recebe os campos Nome, NumeroCamisa e TimeId (recebido na criação de um time via AdicionaTime)</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Em caso de sucesso</response>
    [HttpPost]
    public IActionResult AdicionaJogador([FromBody] CreateJogadorDto jogadorDto)
    {
        Jogador jogador = _mapper.Map<Jogador>(jogadorDto);
        _context.Jogadores.Add(jogador);
        try
        {
            _context.SaveChanges();
        }
        catch(DbUpdateException ex)
        {
            return UnprocessableEntity("Não foi possível processar a requisição. Verifique o ID do Time.");
        }
        
        return CreatedAtAction(nameof(RecuperaJogadorPorId), new { id = jogador.Id }, jogadorDto);
    }
    /// <summary>
    /// Retorna uma lista de jogadores em ordem de inserção OU pertencentes a um mesmo time.
    /// </summary>
    /// <param name="skip">Informa a partir de qual inserção retornar</param>
    /// <param name="take">Informa quantas inserções serão retornadas a partir do ponto de início.</param>
    /// <param name="timeId">Inserido caso desejar buscar todos os jogadores pertencentes a um time em comum. Valor padrão NULO.</param>
    /// <returns>IEnumerable</returns>
    [HttpGet]
    public IEnumerable<ReadJogadorDto> RecuperaJogadores([FromQuery] int skip = 0, [FromQuery] int take = 50,[FromQuery] int? timeId = null)
    {
        if (timeId == null) 
            return _mapper.Map<List<ReadJogadorDto>>(_context.Jogadores.Skip(skip).Take(take).ToList());

        return _mapper.Map<List<ReadJogadorDto>>(_context.Jogadores.Skip(skip).Take(take).Where(jogador => jogador.TimeId == timeId).ToList());
    }

    /// <summary>
    /// Retorna jogadores individuais por meio do ID.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do jogador</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]
    public IActionResult RecuperaJogadorPorId(int id)
    {
        var jogador = _context.Jogadores.FirstOrDefault(jogador => jogador.Id == id);
        if (jogador != null)
        {
            ReadJogadorDto jogadorDto = _mapper.Map<ReadJogadorDto>(jogador);
            return Ok(jogadorDto);
        }
        return NotFound();
    }

    /// <summary>
    /// Atualiza por completo o registro de um jogador identificado por seu ID.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do jogador</param>
    /// <param name="jogadorDto">Recebe os campos Nome, NumeroCamisa e TimeId (recebido na criação de um time via AdicionaTime)</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{id}")]
    public IActionResult AtualizaJogador(int id, [FromBody] UpdateJogadorDto jogadorDto) 
    {
        var jogador = _context.Jogadores.FirstOrDefault(jogador => jogador.Id == id);
        if (jogador != null)
        {
            _mapper.Map(jogadorDto, jogador);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return UnprocessableEntity("Não foi possível processar a requisição. Verifique o ID do Time.");
            }
            return NoContent();
        }
        return NotFound();
    }

    /// <summary>
    /// Atualiza uma única informação de um jogador.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do jogador</param>
    /// <param name="campoJogadorDto">Recebe individualmente os campos Nome, NumeroCamisa e TimeId (recebido na criação de um time via AdicionaTime)</param>
    /// <returns>IActionResult</returns>
    [HttpPatch("{id}")]
    public IActionResult AtualizaCampoJogador(int id, JsonPatchDocument<UpdateJogadorDto> campoJogadorDto)
    {
        var jogador = _context.Jogadores.FirstOrDefault(jogador => jogador.Id == id);
        if (jogador == null) return NotFound();
                
        var jogadorAtualizavel = _mapper.Map<UpdateJogadorDto>(jogador);
        campoJogadorDto.ApplyTo(jogadorAtualizavel, ModelState);

        if (!TryValidateModel(jogadorAtualizavel)) return ValidationProblem(ModelState);

        _mapper.Map(jogadorAtualizavel, jogador);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return UnprocessableEntity("Não foi possível processar a requisição. Verifique o ID do Time.");
        }
        return NoContent();
    }

    /// <summary>
    /// Deleta um jogador da base de dados.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do jogador</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletaJogador (int id)
    {
        var jogador = _context.Jogadores.FirstOrDefault(jogador => jogador.Id == id);
        if (jogador == null) return NotFound();
        _context.Remove(jogador);
        _context.SaveChanges();
        return NoContent();
    }
}
