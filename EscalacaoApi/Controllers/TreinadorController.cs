using AutoMapper;
using EscalacaoApi.Data;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace EscalacaoApi.Controllers;

[ApiController]
[Route("[controller]")]

public class TreinadorController : ControllerBase
{
    private JogadorContext _context;
    private IMapper _mapper;

    public TreinadorController(JogadorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Realiza a inserção de um treinador no banco de dados.
    /// </summary>
    /// <param name="treinadorDto">Recebe os campos Nome e TimeId</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public IActionResult AdicionaTreinador([FromBody] CreateTreinadorDto treinadorDto)
    {
        Treinador treinador = _mapper.Map<Treinador>(treinadorDto);
        _context.Treinadores.Add(treinador);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return UnprocessableEntity("Não foi possível processar a requisição. Verifique o ID do Time.");
        }
        return CreatedAtAction(nameof(RetornaTreinadorPorId), new { id = treinador.Id }, treinador);
    }

    /// <summary>
    /// Retorna uma lista completa dos treinadores inseridos.
    /// </summary>
    /// <returns>IEnumerable</returns>
    [HttpGet]
    public IEnumerable<ReadTreinadorDto> RetornaTreinadores()
    {
        return _mapper.Map<List<ReadTreinadorDto>>(_context.Treinadores.ToList());
    }

    /// <summary>
    /// Retorna um treinador individualmente por meio do Id.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do treinador</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult RetornaTreinadorPorId(int id)
    {
        var treinador = _context.Treinadores.FirstOrDefault(treinador => treinador.Id  == id);
        if (treinador == null) return NotFound();
        var treinadorDto = _mapper.Map<ReadTreinadorDto>(treinador);
        return Ok(treinadorDto);
    }

    /// <summary>
    /// Atualiza os campos do Treinador.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do treinador</param>
    /// <param name="treinadorDto">Recebe os campos Nome e TimeId</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{id}")]
    public IActionResult AtualizaTreinador(int id, [FromBody] UpdateTreinadorDto treinadorDto)
    {
        var treinador = _context.Treinadores.FirstOrDefault(treinador => treinador.Id == id);
        if (treinador == null) return NotFound();
        _mapper.Map(treinadorDto, treinador);
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
    /// Deleta um treinador da base de dados identificado pelo ID
    /// </summary>
    /// <param name="id">Parâmetro para identificação do treinador</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletaTreinador(int id)
    {
        var treinador = _context.Treinadores.FirstOrDefault(treinador => treinador.Id == id);
        if (treinador == null) return NotFound();
        _context.Remove(treinador);
        _context.SaveChanges();
        return NoContent();
    }
}
