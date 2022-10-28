using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.TrabeColumna
{
    public class Tension
    {
        public double _a { get; set; }
        public double _espesor { get; set; }//De placa
        public double _fy { get; set; }
        public double _diametro { get; set; }
        public int _noTornillos { get; set; }
        public double _fu { get; set; }

        public double Ag { get => (_a * _espesor) / 100; }
        public double Fluencia { get => (0.9 * _fy * Ag) * 9.80665 / 1000; }
        public double Ae
        {
            get
            {

                double temp;
                if (_diametro < 28.575)
                {
                    temp = _diametro + 1.6;
                }
                else
                {
                    temp = _diametro + 3.2;
                }
                temp = _a - temp * _noTornillos;
                return (temp * _espesor) / 100;
            }
        }
        public double Ruptura { get => (0.75 * _fu * Ae) * 9.80665 / 1000; }
        public double Resistencia { get => Math.Min(Fluencia, Ruptura); }

    }
}
