using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Ancla
    {
        public string Pulgada { set; get; }
        public double Mm { set; get; }
        public Ancla (string _pulgada, double _mm)
        {
            Pulgada = _pulgada;
            Mm = _mm;
        }
    }
}
