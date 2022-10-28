using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.PlacaBase.Tension
{
    public class ZonaRevision2
    {
        public ZonaRevision2()
        {

        }

        public double _anchoPropuesto { get; set; }
        public double _lado { get; set; }
        public double _centroide { get; set; }
        public int _cantidadAnclas { get; set; }
        public double _distBordeRecortadoDetallado { get; set; }
        public double _diametroAncla { get; set; }
        public double _tensionMaxima { get; set; }
        public double _Fy { get; set; }
        public double _espesorPlacaDefinitivo { get; set; }
        public double LargoDeLaFranja { get => (_anchoPropuesto - (_lado - 2 * _centroide)) * Math.Cos(Math.PI / 4); }
        public double EspesorPropuestoDePlaca { get; set; }//Usuario
        public double Q
        {
            get
            {

                if (_cantidadAnclas == 4)
                {
                    return (
                        3
                        *
                        (
                            LargoDeLaFranja - _distBordeRecortadoDetallado
                            /
                                (10 *
                                    Math.Cos(Math.PI / 4)
                                )
                        )
                        /
                        (
                            8
                            *
                            Math.Min(
                                _distBordeRecortadoDetallado / (10 * Math.Cos(Math.PI / 4)),
                                2 * _diametroAncla / 10)
                         )
                         -
                         Math.Pow(EspesorPropuestoDePlaca * 10, 3)
                         /
                         (20 * Math.Pow(25.4, 3))) * _tensionMaxima / 4;
                }
                else
                {
                    return (3 * (_anchoPropuesto / 2 + _centroide - _lado) / Math.Cos(Math.PI / 4) / (8 * Math.Min(_distBordeRecortadoDetallado / (10 * Math.Cos(Math.PI / 4)), 2 * _diametroAncla / 10)) - Math.Pow(EspesorPropuestoDePlaca * 10, 3) / (20 * Math.Pow(25.4, 3))) * _tensionMaxima / 8;
                }

            }
        }
        public double Momento
        {
            get
            {
                if (_cantidadAnclas == 4)
                {
                    return (_tensionMaxima / 4 + Q) * (LargoDeLaFranja - _distBordeRecortadoDetallado / (10 * Math.Cos(Math.PI / 4))) - Q * (LargoDeLaFranja - _distBordeRecortadoDetallado / (10 * Math.Cos(Math.PI / 4)) + Math.Min(_distBordeRecortadoDetallado / (10 * Math.Cos(Math.PI / 4)), 2 * _diametroAncla / 10));
                }
                else
                {
                    return 2 * (_tensionMaxima / 8 + Q) * (_anchoPropuesto / 2 + _centroide - _lado) / Math.Cos(Math.PI / 4) + _tensionMaxima / 8 * (LargoDeLaFranja - _distBordeRecortadoDetallado / (10 * Math.Cos(Math.PI / 4))) - 2 * Q * ((_anchoPropuesto / 2 + _centroide - _lado) / Math.Cos(Math.PI / 4) + Math.Min(_distBordeRecortadoDetallado / (10 * Math.Cos(Math.PI / 4)), 2 * _diametroAncla / 10));
                }


            }
        }
        public double EspesorPlaca
        {
            get
            {
                double temp = Math.Sqrt(2 * Momento / (0.9 * _Fy * LargoDeLaFranja));
                return multiploSuperior(temp, 0.3175);

            }
        }//Duda
        public double Mn { get => 0.9 * _Fy * LargoDeLaFranja * Math.Pow(_espesorPlacaDefinitivo, 2) / 2; }
        public double _1_6My { get => 1.6 * (0.9 * _Fy * LargoDeLaFranja * Math.Pow(_espesorPlacaDefinitivo, 2) / 3); }

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
