using System;
using System.ComponentModel.DataAnnotations;

namespace RuletaAPi.Models
{
    public class Apuesta
    {
        public Guid Id { get; set; }
        [Required]
        [Range(1, 36)]
        public int Numero { get; set; }
        [Required]
        [RegularExpression("Rojo|Negro")]
        public string Color { get; set; }
        [Required]
        [Range(1, 10000)]
        public double DineroApostado { get; set; }
        public double DineroGanado { get; set; }
        public bool Active { get; set; }
        public Guid IdRuleta { get; set; }

    }
}
