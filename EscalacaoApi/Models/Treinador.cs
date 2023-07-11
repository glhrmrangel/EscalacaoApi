using System.ComponentModel.DataAnnotations;

namespace EscalacaoApi.Models;

public class Treinador
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    public int TimeId { get; set; }

    public virtual Time Time { get; set; }
}
