using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.TrabeColumna
{
    public class Cortante
    {
        public double _ag { get; set; }
        public double _ae { get; set; }
        public double _fy { get; set; }
        public double _fu { get; set; }
        public double Agv { get => _ag; }
        public double Fluencia { get => (1 * 0.6 * _fy * Agv) * 9.80665 / 1000; }
        public double Anv { get => _ae; }
        public double Ruptura { get => 0.75 * 0.6 * _fu * Anv * 9.80665 / 1000; }
        public double ResistenciaFinal { get => Math.Min(Fluencia, Ruptura); }
    }
}
