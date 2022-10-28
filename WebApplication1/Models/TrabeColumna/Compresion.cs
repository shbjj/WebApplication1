using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.TrabeColumna
{
    public class Compresion
    {
        public double _bPlaca { set; get; }
        public double _espesorPlaca { set; get; }
        public double _fy { set; get; }
        public double Ag { get => (_bPlaca * _espesorPlaca) / 100; }
        public double Pn { get => 0.9 * _fy * Ag; }
    }
}
