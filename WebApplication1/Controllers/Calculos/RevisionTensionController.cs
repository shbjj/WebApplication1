using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.PlacaBase.Tension;

namespace WebApplication1.Controllers.Calculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevisionTensionController : ControllerBase
    {
        public IActionResult CalcularTension(PlacaBase datosEntrada)
        {
            //Crear el Ancla
            Models.PlacaBase.Ancla ancla = new Models.PlacaBase.Ancla(datosEntrada.AnchoPropuesto, datosEntrada.Perfil.Centroide, datosEntrada.Ancla.DiametroAncla);
            ancla.CantidadDeAnclas = datosEntrada.Ancla.CantidadDeAnclas;
            ancla.DiametroAncla = datosEntrada.Ancla.DiametroAncla;
            ancla.DistanciaBordeRecortadoDet = datosEntrada.Ancla.DistanciaBordeRecortadoDet;
            //Crear el perfil
            Perfil perfil = new Perfil();
            perfil = datosEntrada.Perfil;
            //Crear ZonaRevision1
            ZonaRevision1 zonaRevision1 = new ZonaRevision1();
            zonaRevision1._anchoPropuesto = datosEntrada.AnchoPropuesto;
            zonaRevision1._centroide = datosEntrada.Perfil.Centroide;
            zonaRevision1._cantAnclas = datosEntrada.Ancla.CantidadDeAnclas;
            zonaRevision1._distanciaBordeRecortadoDet = datosEntrada.Ancla.DistanciaBordeRecortadoDet;
            zonaRevision1._diametroAncla = ancla.DiametroAncla;
            zonaRevision1._tensionMaxima = datosEntrada.TensionMax;
            zonaRevision1._Fy = datosEntrada.AceroPlaca_Fy;
            zonaRevision1.EspesorPropuestoDePlaca = datosEntrada.ZonaRevision1.EspesorPropuestoDePlaca;

            //Crear ZonaRevision2 =>por revisar
            ZonaRevision2 zonaRevision2 = new ZonaRevision2();
            zonaRevision2._anchoPropuesto = datosEntrada.AnchoPropuesto;
            zonaRevision2._lado = datosEntrada.Perfil.Lado;
            zonaRevision2._centroide = datosEntrada.Perfil.Centroide;
            zonaRevision2._cantidadAnclas = datosEntrada.Ancla.CantidadDeAnclas;
            zonaRevision2._distBordeRecortadoDetallado = datosEntrada.Ancla.DistanciaBordeRecortadoDet;
            zonaRevision2._diametroAncla = ancla.DiametroAncla;
            zonaRevision2._tensionMaxima = datosEntrada.TensionMax;
            zonaRevision2._Fy = datosEntrada.AceroPlaca_Fy;
            zonaRevision2.EspesorPropuestoDePlaca = zonaRevision1.EspesorPropuestoDePlaca;
            double t = zonaRevision2.Q;
            //Crear placa Base
            PlacaBase temp = new PlacaBase();
            temp.Concreto = datosEntrada.Concreto;
            temp.AceroPlaca_Fy = datosEntrada.AceroPlaca_Fy;
            temp.AceroPlaca_Fu = datosEntrada.AceroPlaca_Fu;
            temp.AceroAnclas_Fy = datosEntrada.AceroAnclas_Fy;
            temp.AceroAnclas_Fu = datosEntrada.AceroAnclas_Fu;
            temp.PiezaElemento = datosEntrada.PiezaElemento;
            temp.Grupo = datosEntrada.Grupo;
            temp.NombrePerfil = datosEntrada.NombrePerfil;
            temp.TensionMax = datosEntrada.TensionMax;
            temp.Cortante = datosEntrada.Cortante;
            temp.Ancla = ancla;
            temp.Perfil = perfil;
            temp.ZonaRevision1 = zonaRevision1;
            temp.ZonaRevision2 = zonaRevision2;

            //Agregar a las zonas el espesor de placa definitivo
            temp.ZonaRevision1._espesorPlacaDefinitivo = temp.EspesorPlacaDefinitivo;
            temp.ZonaRevision2._espesorPlacaDefinitivo = temp.EspesorPlacaDefinitivo;
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(temp, options);
            return Ok(jsonString);

        }
    }
}
