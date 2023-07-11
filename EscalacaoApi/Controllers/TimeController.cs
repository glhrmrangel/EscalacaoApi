using AutoMapper;
using EscalacaoApi.Data;
using EscalacaoApi.Data.Dtos;
using EscalacaoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscalacaoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeController : ControllerBase
{
    private JogadorContext _context;
    private IMapper _mapper;

    public TimeController(JogadorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Realiza a inserção de um Time no banco de dados.
    /// </summary>
    /// <param name="timeDto">Recebe os campos Nome, Estado e AnoFundacao</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public IActionResult AdicionaTime(CreateTimeDto timeDto)
    {
        Time time = _mapper.Map<Time>(timeDto);
        _context.Times.Add(time);
        _context.SaveChanges();
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
        return _mapper.Map<List<ReadTimeDto>>(_context.Times.Skip(skip).Take(take).ToList());
    }

    /// <summary>
    /// Recupera um time individual por meio de seu ID
    /// </summary>
    /// <param name="id">Parâmetro para identificação do time</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]  
    public IActionResult RecuperaTimePorId(int id) 
    {
        var time = _context.Times.FirstOrDefault(time => time.Id == id);
        if (time == null) return NotFound();

        ReadTimeDto timeDto = _mapper.Map<ReadTimeDto>(time);
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
        var time = _context.Times.FirstOrDefault(time => time.Id == id);
        if (time == null) return NotFound();
        _mapper.Map(timeDto, time);
        _context.SaveChanges();
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
        var time = _context.Times.FirstOrDefault(time => time.Id == id);
        if (time == null) return NotFound();
        _context.Remove(time);
        _context.SaveChanges();
        return NoContent();
    }
}
