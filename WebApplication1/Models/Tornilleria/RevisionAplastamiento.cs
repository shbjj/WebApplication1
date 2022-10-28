using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tornilleria
{
    public class RevisionAplastamiento
    {
        public double _diametroTornillo { get; set; }
        public string _tipoConexion { get; set; }
        public double _espesorPerfil { get; set; }
        public double _fu { get; set; }
        public double _tension { get; set; }
        public double _compresion { get; set; }
        public double Rna { get => 2.4 * _diametroTornillo * _espesorPerfil * _fu / 1000; }
        public double Rua { get => 0.75 * Rna; }
        public double RuaTotal { get => Math.Max(_tension, _compresion); }
        public int Na
        {
            get
            {
                if (_tipoConexion == "Simple") { return Convert.ToInt32(Math.Ceiling(RuaTotal / Rua)); } else { return Convert.ToInt32(multiploSuperior(RuaTotal / Rua, 2)); }
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
