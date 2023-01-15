using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuletaAPi.Data;
using RuletaAPi.DTOs;
using RuletaAPi.Models;


namespace RuletaAPi.Services
{
    public class RuletaService : IRuletaService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public RuletaService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> CreateRulet()
        {
            Ruleta modelRuleta = new Ruleta();
            modelRuleta.Id = Guid.NewGuid();
            modelRuleta.Abierta = false;
            _context.Add(modelRuleta);
            await _context.SaveChangesAsync();
            return modelRuleta.Id;
        }

        public async Task<bool> Apertura(Guid IdRuleta)
        {
            var ruleta = await _context.Ruletas.FirstOrDefaultAsync(x => x.Id == IdRuleta);
            if (ruleta == null)
            {
                return false;
            }
            ruleta.Abierta = true;
            await _context.SaveChangesAsync();
            return ruleta.Abierta;
        }

        public async Task<bool> Apuesta(ApuestaDTO apuesta)
        {
            var ruleta = await _context.Ruletas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == apuesta.IdRuleta);
            if (ruleta == null)
            {
                return false;
            }
            if (!ruleta.Abierta)
            {
                return false;
            }
            var apuestaRes = _mapper.Map<Apuesta>(apuesta);
            apuestaRes.Id = Guid.NewGuid();
            apuestaRes.DineroGanado = 0;
            apuesta.Active = true;
            _context.Add(apuestaRes);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ApuestaDTO>> Cierre(Guid id)
        {
            var ruleta = await _context.Ruletas.FirstOrDefaultAsync(x => x.Id == id);
            if (!ruleta.Abierta)
            {
                return null;
            }
            Random r = new Random();
            ruleta.Abierta = false;
            int rInt = r.Next(1, 36);
            ruleta.NumGanador = rInt;
            if ((rInt % 2)==0)
            {
                ruleta.ColorGanador = "Rojo";
            }
            else
            {
                ruleta.ColorGanador = "Negro";
            }
            var apuestasNumero = await _context.Apuesta.Where(x => x.Numero == ruleta.NumGanador && x.IdRuleta == id && x.Active).ToListAsync();
            apuestasNumero = ActualizarApuestaNumero(apuestasNumero);
            var apuestasColor = await _context.Apuesta.Where(x => x.Color == ruleta.ColorGanador && x.IdRuleta == id && x.Active).ToListAsync();
            apuestasColor = ActualizarApuestaColor(apuestasColor);
            var apuestas = await _context.Apuesta.Where(x => x.Active && x.IdRuleta == id).ToListAsync();
            apuestas.ForEach(p =>{ p.DineroApostado = 0;  p.Active = false;});
            await _context.SaveChangesAsync();

            return _mapper.Map<List<ApuestaDTO>>(apuestas);
        }

        public async Task<List<RuletaDTO>> ObtenerTodos()
        {
            return _mapper.Map<List<RuletaDTO>>(await _context.Ruletas.ToListAsync());
        }

        public async Task<List<ApuestaDTO>> ObtenerHistoricoApuestas()
        {
            return _mapper.Map<List<ApuestaDTO>>(await _context.Apuesta.ToListAsync());
        }


        private List<Apuesta> ActualizarApuestaNumero(List<Apuesta> apuesta)
        {
            List<Apuesta> apuestaList = new List<Apuesta>();
            foreach (Apuesta res in apuesta)
            {
                res.DineroGanado = (res.DineroApostado * 5); 
                apuestaList.Add(res);
            }
            return apuestaList;
        }

        private List<Apuesta> ActualizarApuestaColor(List<Apuesta> apuesta)
        {
            List<Apuesta> apuestaList = new List<Apuesta>();
            foreach (Apuesta res in apuesta)
            {
                res.DineroGanado = (res.DineroApostado * 1.8);
                apuestaList.Add(res);
            }
            return apuestaList;
        }
    }
}

