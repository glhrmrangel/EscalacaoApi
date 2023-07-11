using System.ComponentModel.DataAnnotations;

namespace EscalacaoApi.Data.Dtos;

public class CreateJogadorDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório!")]
    [MaxLength(60)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo NumeroCamisa é obrigatório!")]
    [Range(1, 100)]
    public int NumeroCamisa { get; set; }

    public int TimeId { get; set; }
}
