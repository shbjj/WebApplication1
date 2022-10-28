using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.TrabeColumna
{
    public class TrabeColumna
    {
        public Perfil Perfil { get; set; }
        public Fuerzas Fuerzas { get; set; }
        public Tornillo Tornillo { get; set; }
        public DimensionesPlaca DimensionesPlaca { get; set; }
        public int NoTornillos { get; set; }//Dato de entrada
        public Tension Tension { get; set; }
        public Cortante Cortante { get; set; }
        public BloqueDeCortante BloqueDeCortante { get; set; }//LLeva un dato de entrada
        public Compresion Compresion { get; set; }
    }
}
