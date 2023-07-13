using EscalacaoApi.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using EscalacaoApi.Services;

namespace EscalacaoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JogadorController : ControllerBase
{
    private JogadorService _jogadorService;

    public JogadorController(JogadorService jogadorService)
    {
        _jogadorService = jogadorService;
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
        var jogador = _jogadorService.InsereJogador(jogadorDto);
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
    public IEnumerable<ReadJogadorDto> RecuperaJogadores([FromQuery] int skip = 0, [FromQuery] int take = 50, [FromQuery] int? timeId = null)
    {
        return _jogadorService.RecuperaJogadores(skip, take, timeId);
    }

    /// <summary>
    /// Retorna jogadores individuais por meio do ID.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do jogador</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]
    public IActionResult RecuperaJogadorPorId(int id)
    {
        var jogador = _jogadorService.BuscaJogadorPorId(id);
        if (jogador != null)
        {
            var jogadorDto = _jogadorService.RecuperaJogador(jogador);
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
        var jogador = _jogadorService.BuscaJogadorPorId(id);
        if (jogador != null)
        {
            _jogadorService.AtualizaJogador(jogadorDto, jogador);
            return NoContent();
        }
        return NotFound();
    }

    /// <summary>
    /// Deleta um jogador da base de dados.
    /// </summary>
    /// <param name="id">Parâmetro para identificação do jogador</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletaJogador(int id)
    {
        var jogador = _jogadorService.BuscaJogadorPorId(id);
        if (jogador == null) return NotFound();
        _jogadorService.DeletaJogador(jogador);
        return NoContent();
    }
}
