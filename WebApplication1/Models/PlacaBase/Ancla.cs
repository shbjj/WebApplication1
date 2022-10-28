using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.PlacaBase
{
    public class Ancla
    {
        public Ancla()
        {

        }
        public Ancla(double _anchoPropuesto, double _centroide, double _diametroAncla)
        {
            DiametroAncla = _diametroAncla;
            DistanciaBordeRecortadoMax = _anchoPropuesto * 10 / 2 - _centroide * 10 - 0.95 * DiametroAncla;
            AreaAncla = Math.Round((Math.PI * (Math.Pow(DiametroAncla / 2, 2))), 1);
        }
        public double DiametroAncla { get; set; }//USuario
        public int CantidadDeAnclas { get; set; }//Usuario
        public double IdAncla { get => DiametroAncla / 3.175 - 3; }
        public int DistanciaBordeRecortadoMin
        {
            get
            {
                int[] temp = { 22, 29, 32, 38, 45, 51, 57, 61, 67 };

                return temp[Convert.ToInt32(IdAncla) - 1];
            }
        }
        public int DistanciaBordeRecortadoDet { get; set; }//Usuario
        public double DiametroTuerca
        {
            get
            {
                double[] temp = { 25.7, 31.2, 36.7, 42.2, 47.7, 53.2, 58.7, 64.2, 69.7 };
                return temp[Convert.ToInt32(IdAncla) - 1];//
            }
        }
        public double RadioCortoTuerca { get => DiametroTuerca * 0.433; }
        public double DistanciaBordeRecortadoMax { get; set; }
        public double AreaAncla { get; set; }

    }
}
