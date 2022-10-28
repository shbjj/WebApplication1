using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tornilleria
{
    public class RevisionResistenciaDesgarre
    {
        public double _diametroTornillo { get; set; }
        public double _espesorPerfil { get; set; }
        public double _fu { get; set; }
        public double _tension { get; set; }
        public string _tipoConexion { get; set; }


        public double Da
        {
            get
            {
                if (_diametroTornillo < 25.4)
                {
                    return _diametroTornillo + 1.6;
                }
                else
                { return _diametroTornillo + 3.2; }
            }
        }
        public double Dmin { get; set; }//Dmin del tornillo
        public double Lc1 { get => Dmin - (Da / 2); }
        public double Rnd1 { get => 2.1 * Lc1 * _espesorPerfil * _fu / 1000; }
        public double Rund1 { get => 0.75 * Rnd1; }
        public double S { get => 3 * _diametroTornillo; }
        public double Lc2 { get => S - Da; }
        public double Rnd2 { get => 2.1 * Lc2 * _espesorPerfil * _fu / 1000; }
        public double Rund2 { get => 0.75 * Rnd2; }
        public int Nd
        {
            get
            {
                if (_tipoConexion == "Simple")
                {
                    if (_tension > Rund1)
                    {
                        return Convert.ToInt32(Math.Ceiling(((_tension - Rund1) / Rnd2) + 1));
                    }
                    else
                    {
                        return 1;

                    }
                }
                else
                {
                    if (_tension > 2 * Rund1)
                    {
                        return Convert.ToInt32(multiploSuperior(((_tension - 2 * Rund1) / Rnd2) + 2, 2));
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
        }
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
