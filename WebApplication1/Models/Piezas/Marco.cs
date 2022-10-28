using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Piezas
{
    public class Marco
    {
        public int IdMarco { get; set; }
        public int IdSubestacion { get; set; }
        public string Nombre { get; set; }
        public float BaseX { get; set; }
        public float BaseZ { get; set; }
        public int Separacion { get; set; }
        public float AlturaGuarda { get; set; }
        public float AlturaSuperior { get; set; }
        public float AlturaSuspension { get; set; }
        public float AlturaRemate { get; set; }
        public int Tension { get; set; }
        public float FrecuenciaX { get; set; }
        public float FrecuenciaZ { get; set; }
        public float BaseZColumnas { get; set; }
        public float BaseXColumnas { get; set; }
        public float BaseSoporteColumnas { get; set; }
        public float Distanciab { get; set; }
        public int IdResponsable { get; set; }
        public DateTime Fecha { get; set; }
    }
}
