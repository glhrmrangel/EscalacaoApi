using System.ComponentModel.DataAnnotations;

namespace EscalacaoApi.Models;

public class Jogador
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required (ErrorMessage = "O campo Nome é obrigatório!")]
    [MaxLength (60)]
    public string Nome { get; set; }

    [Required (ErrorMessage = "O campo NumeroCamisa é obrigatório!")]
    [Range (1, 100)]
    public int NumeroCamisa { get; set; }

    [Required]
    public int TimeId { get; set; }

    public virtual Time Time { get; set; }
}
