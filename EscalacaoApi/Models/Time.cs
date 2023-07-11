using System.ComponentModel.DataAnnotations;

namespace EscalacaoApi.Models;

public class Time
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório!")]
    [MaxLength(50)]
    public string Nome { get; set; }

    [Required]
    [MaxLength(20)]
    public string Estado { get; set; }

    public int AnoFundacao { get; set; }

    public virtual Treinador Treinador { get; set; }

    public virtual ICollection<Jogador> Jogadores { get; set; }
}
