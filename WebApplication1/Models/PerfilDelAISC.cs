using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PerfilDelAISC
    {
        public int Numero { set; get; }
        public double Lado { set; get; }
        public double Centroide { set; get; }
        public PerfilDelAISC(int _numero, double _lado, double _centroide) 
        {
            Numero = _numero;
            Lado = _lado;
            Centroide = _centroide;
        }
    }
}
