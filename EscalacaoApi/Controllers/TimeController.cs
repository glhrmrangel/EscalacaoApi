using AutoMapper;
using EscalacaoApi.Data;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;
using EscalacaoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EscalacaoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeController : ControllerBase
{
    private TimeService _timeService;

    public TimeController(TimeService timeService)
    {
        _timeService = timeService;
    }

    /// <summary>
    /// Realiza a inserção de um Time no banco de dados.
    /// </summary>
    /// <param name="timeDto">Recebe os campos Nome, Estado e AnoFundacao</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public IActionResult AdicionaTime(CreateTimeDto timeDto)
    {
        var time = _timeService.InsereTime(timeDto);
        return CreatedAtAction(nameof(RecuperaTimePorId), new { id = time.Id }, time);
    }

    /// <summary>
    /// Retorna a lista de times inseridos na base de dados.
    /// </summary>
    /// <param name="skip">Informa a partir de qual inserção retornar</param>
    /// <param name="take">Informa quantas inserções serão retornadas a partir do ponto de início.</param>
    /// <returns>IEnumerable</returns>
    [HttpGet]
    public IEnumerable<ReadTimeDto> RecuperaTimes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _timeService.RecuperaTimes(skip, take);
    }

    /// <summary>
    /// Recupera um time individual por meio de seu ID
    /// </summary>
    /// <param name="id">Parâmetro para identificação do time</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]  
    public IActionResult RecuperaTimePorId(int id) 
    {
        var time = _timeService.BuscaTimePorId(id);
        if (time == null) return NotFound();

        ReadTimeDto timeDto = _timeService.RecuperaTime(time);
        return Ok(timeDto);
    }

    /// <summary>
    /// Atualiza os campos de um time identificado pelo ID.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do time</param>
    /// <param name="timeDto">Recebe os campos Nome, Estado e AnoFundacao</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{id}")]
    public IActionResult AtualizaTime(int id, [FromBody] UpdateTimeDto timeDto) 
    {
        var time = _timeService.BuscaTimePorId(id);
        if (time == null) return NotFound();
        _timeService.AtualizaTime(timeDto, time);
        return NoContent();
    }

    /// <summary>
    /// Deleta um time da base de dados identificado pelo ID
    /// </summary>
    /// <param name="id">Parâmetro para identificação do time</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletaTime(int id) 
    {
        var time = _timeService.BuscaTimePorId(id);
        if (time == null) return NotFound();
        _timeService.DeletaTime(time);
        return NoContent();
    }
}
