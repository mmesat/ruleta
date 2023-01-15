using RuletaAPi.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace RuletaAPi.DTOs
{
    public class ApuestaDTO
    {
        public Guid Id { get; set; }
        [Required]
        [Range(1, 36)]
        public int Numero { get; set; }
        [Required]
        [RegularExpression("Rojo|Negro")]
        public string  Color { get; set; }
        [Required]
        [Range(1, 10000)]
        public double DineroApostado { get; set; }
        public double DineroGanado { get; set; }
        public bool Active { get; set; }
        public Guid IdRuleta { get; set; }
    }
}
