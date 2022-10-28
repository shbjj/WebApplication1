using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.PlacaBase.Compresion
{
    public class ZonaRevision2
    {
        public ZonaRevision2()
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
        public double LargoDeLaFranja { get => (_AnchoPropuesto - (_Lado - 2 * _Centroide)) / (2 * Math.Cos(Math.PI / 4)); }
        public double Momento { get => (_CompresionMaxima / _AreaPlaca) * Math.Pow(LargoDeLaFranja, 2) * LargoDeLaFranja / 3; }
        public double EspesorPlaca { get => multiploSuperior(Math.Sqrt(2 * Momento / (0.9 * _AceroPlacaFy * LargoDeLaFranja)), 0.3175); }
        public double Mn { get => 0.9 * _AceroPlacaFy * LargoDeLaFranja * Math.Pow(_EspPlacaPorTensión, 2) / 2; }
        public double _1_6My { get => 1.6 * (0.9 * _AceroPlacaFy * LargoDeLaFranja * Math.Pow(_EspPlacaPorTensión, 2) / 3); }

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
