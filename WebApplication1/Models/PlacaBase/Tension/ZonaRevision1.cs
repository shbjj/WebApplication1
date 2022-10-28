using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.PlacaBase.Tension
{
    public class ZonaRevision1
    {
        public ZonaRevision1()
        {

        }

        public double _anchoPropuesto { get; set; }
        public double _centroide { get; set; }
        public int _cantAnclas { get; set; }
        public int _distanciaBordeRecortadoDet { get; set; }
        public double _diametroAncla { get; set; }
        public double _tensionMaxima { get; set; }
        public double _Fy { get; set; }
        public double _espesorPlacaDefinitivo { get; set; }

        public double EspesorPropuestoDePlaca { get; set; }//Usuario
        public double AnchoDeLaFranja { get => _anchoPropuesto / 2 - _centroide; }
        public double Q
        {
            get
            {

                if (_cantAnclas == 4)
                {
                    return (3 * ((_anchoPropuesto / 2) - (_distanciaBordeRecortadoDet / 10) - _centroide) / (8 * Math.Min(_distanciaBordeRecortadoDet / 10, 2 * _diametroAncla / 10)) - Math.Pow(EspesorPropuestoDePlaca * 10, 3) / (20 * Math.Pow(25.4, 3))) * _tensionMaxima / 4;
                }
                else
                {
                    return (3 * ((_anchoPropuesto / 2) - (_distanciaBordeRecortadoDet / 10) - _centroide) / (8 * Math.Min(_distanciaBordeRecortadoDet / 10, 2 * _diametroAncla / 10)) - Math.Pow(EspesorPropuestoDePlaca * 10, 3) / (20 * Math.Pow(25.4, 3))) * _tensionMaxima / 8;
                }
            }
        }
        public double Momento
        {
            get
            {
                if (_cantAnclas == 4)
                {
                    return 2 * (_tensionMaxima / 4 + Q) * ((_anchoPropuesto / 2) - _centroide - _distanciaBordeRecortadoDet / 10) - 2 * Q * ((_anchoPropuesto / 2) - _centroide - _distanciaBordeRecortadoDet / 10 + Math.Min(_distanciaBordeRecortadoDet / 10, 2 * _diametroAncla / 10));
                }
                else
                {
                    return 3 * (_tensionMaxima / 8 + Q) * ((_anchoPropuesto / 2) - _centroide - _distanciaBordeRecortadoDet / 10) - 3 * Q * ((_anchoPropuesto / 2) - _centroide - _distanciaBordeRecortadoDet / 10 + Math.Min(_distanciaBordeRecortadoDet / 10, 2 * _diametroAncla / 10));
                }

            }
        }
        public double EspesorPlaca
        {
            get
            {
                //Sacar EspesorPLaca
                double temp = Math.Sqrt(4 * Momento / (0.9 * _Fy * _anchoPropuesto));
                return multiploSuperior(temp, 0.3175);
            }
        }
        public double Mn { get => 0.9 * _Fy * _anchoPropuesto * Math.Pow(_espesorPlacaDefinitivo, 2) / 4; }
        public double _1_6My { get => 1.6 * (0.9 * _Fy * _anchoPropuesto * Math.Pow(_espesorPlacaDefinitivo, 2) / 6); }

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
