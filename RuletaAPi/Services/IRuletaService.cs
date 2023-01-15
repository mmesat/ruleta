using System.Threading.Tasks;
using System;
using RuletaAPi.DTOs;
using System.Collections.Generic;

namespace RuletaAPi.Services
{
    public interface IRuletaService
    {
        Task<Guid> CreateRulet();
        Task<bool> Apertura(Guid IdRuleta);
        Task<bool> Apuesta(ApuestaDTO apuesta);
        Task<List<ApuestaDTO>> Cierre(Guid id);
        Task<List<RuletaDTO>> ObtenerTodos();
        Task<List<ApuestaDTO>> ObtenerHistoricoApuestas();
    }
}
