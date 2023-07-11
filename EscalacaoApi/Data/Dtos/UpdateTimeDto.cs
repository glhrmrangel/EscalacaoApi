using System.ComponentModel.DataAnnotations;

namespace EscalacaoApi.Data.Dtos;

public class UpdateTimeDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório!")]
    [MaxLength(50)]
    public string Nome { get; set; }

    [Required]
    [MaxLength(20)]
    public string Estado { get; set; }

    public int AnoFundacao { get; set; }
}
