using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.TrabeColumna;

namespace WebApplication1.Controllers.Calculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabeColumnaController : ControllerBase
    {
        public IActionResult CalcularTrabeColumna(TrabeColumna datosEntrada)
        {
            //Crear el objeto de tipo TrabeColumna
            TrabeColumna trabeColumna = datosEntrada;
            //Asignarle los valores a Tension
            trabeColumna.Tension = new Tension();
            trabeColumna.Tension._a = datosEntrada.DimensionesPlaca.A;
            trabeColumna.Tension._espesor = datosEntrada.DimensionesPlaca.Espesor;
            trabeColumna.Tension._fy = datosEntrada.Perfil.Fy;
            trabeColumna.Tension._fu = datosEntrada.Perfil.Fu;
            trabeColumna.Tension._diametro = datosEntrada.Tornillo.Diametro;
            trabeColumna.Tension._noTornillos = datosEntrada.NoTornillos;
            //Asignarle los valores a Cortante
            trabeColumna.Cortante = new Cortante();
            trabeColumna.Cortante._ag = trabeColumna.Tension.Ag;
            trabeColumna.Cortante._ae = trabeColumna.Tension.Ae;
            trabeColumna.Cortante._fy = trabeColumna.Perfil.Fy;
            trabeColumna.Cortante._fu = trabeColumna.Perfil.Fu;
            //Asignarle los valores a BloqueDeCortante
            trabeColumna.BloqueDeCortante._espesorPerfil = trabeColumna.Perfil.Espesor;
            trabeColumna.BloqueDeCortante._espesorPlaca = trabeColumna.DimensionesPlaca.Espesor;
            trabeColumna.BloqueDeCortante._dimensionPerfil = trabeColumna.Perfil.Dimension;
            trabeColumna.BloqueDeCortante._noTornillos = trabeColumna.NoTornillos;
            trabeColumna.BloqueDeCortante._gramil = trabeColumna.Perfil.Gramil;
            trabeColumna.BloqueDeCortante._fy = trabeColumna.Perfil.Fy;
            trabeColumna.BloqueDeCortante._fu = trabeColumna.Perfil.Fu;
            trabeColumna.BloqueDeCortante._Ag = trabeColumna.Tension.Ag;
            //Asignarle los valores a Compresion
            trabeColumna.Compresion = new Compresion();
            trabeColumna.Compresion._bPlaca = trabeColumna.DimensionesPlaca.B;
            trabeColumna.Compresion._espesorPlaca = trabeColumna.DimensionesPlaca.Espesor;
            trabeColumna.Compresion._fy = trabeColumna.Perfil.Fy;

            return Ok(trabeColumna);
        }
    }
}
