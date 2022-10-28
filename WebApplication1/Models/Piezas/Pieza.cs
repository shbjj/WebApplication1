using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Piezas
{
    public class Pieza
    {
        public int IdPieza { get; set; }
        public int IdMarco { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Eje { get; set; }
        public string TipoLlegadas { get; set; }
        public int Numero { get; set; }
        public int TipoElemento { get; set; }
    }
}
