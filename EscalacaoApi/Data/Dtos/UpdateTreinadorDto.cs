﻿using EscalacaoApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EscalacaoApi.Data.Dtos;

public class UpdateTreinadorDto
{
    [Required]
    public string Nome { get; set; }

    public int TimeId { get; set; }

}
