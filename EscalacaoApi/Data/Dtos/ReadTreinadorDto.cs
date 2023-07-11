using EscalacaoApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EscalacaoApi.Data.Dtos;

public class ReadTreinadorDto
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public ReadTimeDto ReadTimeDto { get; set; }
}
