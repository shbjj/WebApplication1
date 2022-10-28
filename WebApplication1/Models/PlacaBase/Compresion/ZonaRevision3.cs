using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.PlacaBase.Compresion
{
    public class ZonaRevision3
    {
        public ZonaRevision3()
        {

        }
        //Campos a llenar para obtener los calculos
        public double _AnchoPropuesto { get; set; }
        public double _Lado { get; set; }
        public double _Centroide { get; set; }
        public double _CompresionMaxima { get; set; }
        public double _AreaPlaca { get; set; }
        public double _EspPlacaPorTensión { get; set; }
        public double _AceroPlacaFy { get; set; }
        public double LargoDeLaFranja { get => (_AnchoPropuesto / 2) - (_Lado - _Centroide); }
        public double Momento { get => (_CompresionMaxima / _AreaPlaca) * _AnchoPropuesto * Math.Pow(LargoDeLaFranja, 2) / 2; }
        public double EspesorPlaca { get => multiploSuperior(Math.Sqrt(4 * Momento / (0.9 * _AceroPlacaFy * _AnchoPropuesto)), 0.3175); }
        public double Mn { get => 0.9 * _AceroPlacaFy * _AnchoPropuesto * Math.Pow(_EspPlacaPorTensión, 2) / 4; }
        public double _1_6My { get => 1.6 * (0.9 * _AceroPlacaFy * _AnchoPropuesto * Math.Pow(_EspPlacaPorTensión, 2) / 6); }

        private double multiploSuperior(double numero, double multiplo)
        {
            if (multiplo == 0)
            {
                return numero;
            }
            double residuo = numero % multiplo;
            if (residuo == 0)
            {
                return numero;
            }
            else
            {
                return numero + multiplo - residuo;
            }
        }
    }
}
