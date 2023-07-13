using AutoMapper;
using EscalacaoApi.Data;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;
using EscalacaoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace EscalacaoApi.Controllers;

[ApiController]
[Route("[controller]")]

public class TreinadorController : ControllerBase
{
    private TreinadorService _treinadorService;

    public TreinadorController(TreinadorService treinadorService)
    {
        _treinadorService = treinadorService;
    }

    /// <summary>
    /// Realiza a inserção de um treinador no banco de dados.
    /// </summary>
    /// <param name="treinadorDto">Recebe os campos Nome e TimeId</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public IActionResult AdicionaTreinador([FromBody] CreateTreinadorDto treinadorDto)
    {
        var treinador = _treinadorService.InsereTreinador(treinadorDto);
        return CreatedAtAction(nameof(RetornaTreinadorPorId), new { id = treinador.Id }, treinador);
    }

    /// <summary>
    /// Retorna uma lista completa dos treinadores inseridos.
    /// </summary>
    /// <returns>IEnumerable</returns>
    [HttpGet]
    public IEnumerable<ReadTreinadorDto> RetornaTreinadores()
    {
        return _treinadorService.RecuperaTreinadores();
    }

    /// <summary>
    /// Retorna um treinador individualmente por meio do Id.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do treinador</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult RetornaTreinadorPorId(int id)
    {
        var treinador = _treinadorService.BuscaTreinadorPorId(id);
        if (treinador == null) return NotFound();
        var treinadorDto = _treinadorService.RecuperaTreinador(treinador);
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
        var treinador = _treinadorService.BuscaTreinadorPorId(id);
        if (treinador == null) return NotFound();
        _treinadorService.AtualizaTreinador(treinadorDto, treinador);
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
        var treinador = _treinadorService.BuscaTreinadorPorId(id);
        if (treinador == null) return NotFound();
        _treinadorService.DeletaTreinador(treinador);
        return NoContent();
    }
}
