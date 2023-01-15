using System;

namespace RuletaAPi.Models
{
    public class Ruleta
    {
        public Guid Id { get; set; }
        public bool Abierta { get; set; }
        public int NumGanador { get; set; }
        public string ColorGanador { get; set; }
    }
}
