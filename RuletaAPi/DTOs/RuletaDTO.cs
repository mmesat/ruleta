using System;

namespace RuletaAPi.DTOs
{
    public class RuletaDTO
    {
        public Guid Id { get; set; }
        public bool Abierta { get; set; }
        public int NumGanador { get; set; }
        public string ColorGanador { get; set; }
    }
}
