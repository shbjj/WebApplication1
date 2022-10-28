using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tornilleria
{
    public class RevisionResistenciaBloqueCortante
    {
        public double _lc1 { get; set; }
        public double _lc2 { get; set; }
        public double _espesorPerfil { get; set; }
        public double _dimPerfil { get; set; }
        public double _gramilPerfil { get; set; }
        public string _tipoConexion { get; set; }
        public double _tension { get; set; }
        public double _compresion { get; set; }
        public double _daRevisionResistenciaDesgarre { get; set; }
        public double _dminRevisionResistenciaDesgarre { get; set; }
        public double _sRevisionResistenciaDesgarre { get; set; }
        public double _ubs { get; set; }
        public double _fu { get; set; }
        public double _fy { get; set; }



        public double Anv1 { get => _lc1 * _espesorPerfil; }
        public double Anv2 { get => _lc2 * _espesorPerfil; }
        //public double B {get; }
        //public double Gr {get; }
        //public double Da {get; }
        //public double Dmin {get; }
        //public double S {get; }
        public double Ant { get => (_dimPerfil - _gramilPerfil - (_daRevisionResistenciaDesgarre / 2)) * _espesorPerfil; }
        public double Agv1 { get => _dminRevisionResistenciaDesgarre * _espesorPerfil; }
        public double Agv2 { get => _sRevisionResistenciaDesgarre * _espesorPerfil; }
        public double Rnt { get => _ubs * _fu * Ant / 1000; }
        public double Rut { get => 0.75 * Rnt; }
        public double Rnv1 { get => 0.6 * _fu * Anv1 / 1000; }
        public double Rnv2 { get => 0.6 * _fu * Anv2 / 1000; }
        public double Runv1 { get => 0.75 * Rnv1; }
        public double Runv2 { get => 0.75 * Rnv2; }
        public double Rgv1 { get => 0.6 * _fy * Agv1 / 1000; }
        public double Rgv2
        {
            get => 0.6 * _fy * Agv2 / 1000;
        }
        public double Rugv1 { get => 0.75 * Rgv1; }
        public double Rugv2 { get => 0.75 * Rgv2; }
        public int Nbnv
        {
            get
            {
                if (_tipoConexion == "Simple")
                {
                    if (_tension > (Rut + Runv1))
                    {
                        return Convert.ToInt32(Math.Ceiling(((_tension - Rut - Runv1) / Runv2) + 1));
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {

                    if (_tension > 2 * (Rut + Runv1))
                    {
                        return Convert.ToInt32(multiploSuperior(((_tension - 2 * (Rut - Runv1)) / Runv2) + 2, 2));
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
        }
        public int Nbgv
        {
            get
            {
                if (_tipoConexion == "Simple")
                {
                    if (_tension > (Rut + Rugv1))
                    {
                        return Convert.ToInt32(Math.Ceiling(((_tension - Rut - Rugv1) / Runv2) + 1));
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (_tension > 2 * (Rut + Rugv2))
                    {
                        return Convert.ToInt32(multiploSuperior(((_tension - 2 * (Rut - Rugv2)) / Runv2) + 2, 2));
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
        }
        public int Nd { get => Math.Max(Nbgv, Nbnv); }

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
