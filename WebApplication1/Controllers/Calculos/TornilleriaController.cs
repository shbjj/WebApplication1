using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.Models.Tornilleria;

namespace WebApplication1.Controllers.Calculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class TornilleriaController : ControllerBase
    {

        public IActionResult CalcularTornillos(ElementosTornilleria datosEntrada)
        {
            Tornillos tornillos = new Tornillos();

            for (int i = 0; i < datosEntrada.ElementoTornillerias.Length; i++)
            {
                if (datosEntrada.ElementoTornillerias[i].DatosCompletos)
                {
                    datosEntrada.ElementoTornillerias[i].Tornillo.Dmin = tornillos.Dictionary[datosEntrada.ElementoTornillerias[i].Tornillo.Plg].Dmin;
                    datosEntrada.ElementoTornillerias[i].Tornillo.Mm = tornillos.Dictionary[datosEntrada.ElementoTornillerias[i].Tornillo.Plg].Mm;
                    //RevisionResistenciaCortante
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaCortante = new RevisionResistenciaCortante();
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaCortante._grupoTornillo = datosEntrada.ElementoTornillerias[i].Tornillo.Grupo;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaCortante._roscaTornillo = datosEntrada.ElementoTornillerias[i].Tornillo.Rosca;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaCortante._tipoConexion = datosEntrada.ElementoTornillerias[i].Tornillo.TipoConexion;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaCortante._mmTornillo = datosEntrada.ElementoTornillerias[i].Tornillo.Mm;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaCortante._tension = datosEntrada.ElementoTornillerias[i].Fuerzas.Tension;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaCortante._compresion = datosEntrada.ElementoTornillerias[i].Fuerzas.Compresion;
                    //RevisionAplastamiento
                    datosEntrada.ElementoTornillerias[i].RevisionAplastamiento = new RevisionAplastamiento();
                    datosEntrada.ElementoTornillerias[i].RevisionAplastamiento._diametroTornillo = datosEntrada.ElementoTornillerias[i].Tornillo.Mm;
                    datosEntrada.ElementoTornillerias[i].RevisionAplastamiento._tipoConexion = datosEntrada.ElementoTornillerias[i].Tornillo.TipoConexion;
                    datosEntrada.ElementoTornillerias[i].RevisionAplastamiento._espesorPerfil = datosEntrada.ElementoTornillerias[i].Perfil.Espesor;
                    datosEntrada.ElementoTornillerias[i].RevisionAplastamiento._fu = datosEntrada.Fu;
                    datosEntrada.ElementoTornillerias[i].RevisionAplastamiento._compresion = datosEntrada.ElementoTornillerias[i].Fuerzas.Compresion;
                    datosEntrada.ElementoTornillerias[i].RevisionAplastamiento._tension = datosEntrada.ElementoTornillerias[i].Fuerzas.Tension;
                    //RevisionResistenciaDesgarre
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre = new RevisionResistenciaDesgarre();
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre._diametroTornillo = datosEntrada.ElementoTornillerias[i].Tornillo.Mm;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre.Dmin = datosEntrada.ElementoTornillerias[i].Tornillo.Dmin;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre._espesorPerfil = datosEntrada.ElementoTornillerias[i].Perfil.Espesor;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre._fu = datosEntrada.Fu;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre._tipoConexion = datosEntrada.ElementoTornillerias[i].Tornillo.TipoConexion;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre._tension = datosEntrada.ElementoTornillerias[i].Fuerzas.Tension;
                    //RevisionResistenciaBloqueCortante
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante = new RevisionResistenciaBloqueCortante();
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._lc1 = datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre.Lc1;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._lc2 = datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre.Lc2;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._espesorPerfil = datosEntrada.ElementoTornillerias[i].Perfil.Espesor;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._dimPerfil = datosEntrada.ElementoTornillerias[i].Perfil.Dimension;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._gramilPerfil = datosEntrada.ElementoTornillerias[i].Perfil.Gramil;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._tipoConexion = datosEntrada.ElementoTornillerias[i].Tornillo.TipoConexion;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._tension = datosEntrada.ElementoTornillerias[i].Fuerzas.Tension;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._compresion = datosEntrada.ElementoTornillerias[i].Fuerzas.Compresion;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._daRevisionResistenciaDesgarre = datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre.Da;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._dminRevisionResistenciaDesgarre = datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre.Dmin;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._sRevisionResistenciaDesgarre = datosEntrada.ElementoTornillerias[i].RevisionResistenciaDesgarre.S;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._ubs = datosEntrada.Ubs;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._fu = datosEntrada.Fu;
                    datosEntrada.ElementoTornillerias[i].RevisionResistenciaBloqueCortante._fy = datosEntrada.Fy;
                }
                else
                {
                    datosEntrada.ElementoTornillerias[i] = null;
                }
            }
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(datosEntrada, options);
            return Ok(jsonString);
        }
    }
}
