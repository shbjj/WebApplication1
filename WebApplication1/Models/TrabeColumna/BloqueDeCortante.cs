using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.TrabeColumna
{
    public class BloqueDeCortante
    {
        public double _Anv { get; set; }//Otra oagina
        public double _espesorPerfil { get; set; }
        public double _espesorPlaca { get; set; }
        public double _Ant { get; set; }//Otra pagina
        public double _dimensionPerfil { get; set; }
        public int _noTornillos { get; set; }
        public double _Agv { get; set; }//Otra pagina
        public double _gramil { get; set; }
        public double _Ag { get; set; }//De tension
        public double _fu { get; set; }
        public double _fy { get; set; }
        public double Anv { get => (_Anv / _espesorPerfil) * _espesorPlaca / 100; }
        public double Ant { get => (_Ant / _dimensionPerfil) * _noTornillos / 100; }
        public double Agv { get => (_Agv / _gramil) * _Ag / 100; }
        public double Ubs { get; set; }//Dato de entrada
        public double Rna { get => (0.75 * ((0.6 * _fu * Anv) + (Ubs * _fu * Ant))) * 9.80665 / 1000; }
        public double Rnb { get => (0.75 * ((0.6 * _fy * Agv) + (Ubs * _fu * Ant))) * 9.80665 / 1000; }
        public string Rnc { get { if (Rna <= Rnb) { return "OK"; } else { return "NO"; } } }
    }
}
