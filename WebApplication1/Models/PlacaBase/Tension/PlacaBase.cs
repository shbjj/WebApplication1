using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.PlacaBase.Tension
{
    public class PlacaBase
    {
        public PlacaBase()
        {

        }
        //Variables
        public double Concreto { get; set; }//Dato usuario
        public double AceroPlaca_Fy { get; set; }//Dato usuario
        public double AceroPlaca_Fu { get; set; }//Dato usuario
        public double AceroAnclas_Fy { get; set; }//Dato usuario
        public double AceroAnclas_Fu { get; set; }//Dato usuario

        //Aun no se si se ocupan
        public string PiezaElemento { get; set; }//Dato usuario
        public string Grupo { get; set; }//Dato usuario
        public string NombrePerfil { get; set; }//Dato usuario

        public double TensionMax { get; set; }//Dato usuario
        public double EsfuerzoConcreto { get => 0.75 * 0.8 * Concreto; }
        public double AreaRequerida { get => Math.Round((TensionMax / EsfuerzoConcreto), 1); }
        public double Cortante { get; set; }//Dato usuario

        public Perfil Perfil { get; set; }//Dato usuario
        //
        public Ancla Ancla { get; set; }
        public double AnchoPlaca { get => Math.Max((2 * (Perfil.Centroide + (Ancla.DistanciaBordeRecortadoMin + 0.95 * Ancla.DiametroAncla) / 10)), (Math.Pow(AreaRequerida, 0.5))); }
        public double AnchoPropuesto { get => AnchoPlaca + 5 - (AnchoPlaca % 5); }//Dato usuario//Calculado
        public double AreaPlaca { get => AnchoPropuesto * AnchoPropuesto; }
        public double DistanciaAnclaAlPeril { get => (AnchoPropuesto * 10 / 2 - Ancla.DistanciaBordeRecortadoDet - Perfil.Centroide * 10) / Ancla.DiametroAncla; }
        public double SepDeAnclas
        {
            get
            {
                if (Ancla.CantidadDeAnclas == 8)
                {
                    return (AnchoPropuesto * 10 - 2 * Ancla.DistanciaBordeRecortadoDet) / (2 * Ancla.DiametroAncla);
                }
                else
                {
                    return (AnchoPropuesto * 10 - 2 * Ancla.DistanciaBordeRecortadoDet) / (Ancla.DiametroAncla);
                }
                //=SI(P22=8,(Y22*10-2*T22)/(2*O22),(Y22*10-2*T22)/(O22))
            }
        }
        public ZonaRevision1 ZonaRevision1 { get; set; }
        public ZonaRevision2 ZonaRevision2 { get; set; }
        public double EspesorPlacaDefinitivo
        {
            get => Math.Max(
               Math.Max(
                         ZonaRevision1.EspesorPropuestoDePlaca, ZonaRevision1.EspesorPlaca
                        )
               , ZonaRevision2.EspesorPlaca);
        }
        //Aplast. x el cortante, placa		
        public double RaAncla
        {
            get => Math.Min(1.2 * (
                (Ancla.DistanciaBordeRecortadoDet - (Ancla.DiametroAncla / 2) - (25.4 / 16)) / 10) * ZonaRevision1.EspesorPropuestoDePlaca * AceroPlaca_Fu,
                2.4 * (Ancla.DiametroAncla / 10) * ZonaRevision1.EspesorPropuestoDePlaca * AceroPlaca_Fu);
        }
        public double ResisAplastamiento { get => 0.75 * (RaAncla); }
        public double PorcentajeApla { get => 100 * (Cortante / Ancla.CantidadDeAnclas / ResisAplastamiento); }
        //Cortante en anclas
        public double RV1Ancla { get => Math.Pow((Ancla.DiametroAncla / 10), 2) * Math.PI / 4 * 0.55 * 0.9 * AceroAnclas_Fy; }
        public double ResisCort { get => RV1Ancla * Ancla.CantidadDeAnclas; }
        public double PorcentajeCorAncla { get => 100 * Cortante / ResisCort; }
        //Tensión anclas
        public double ResisTension { get => Math.Pow((Ancla.DiametroAncla / 10), 2) * Math.PI / 4 * 0.9 * AceroAnclas_Fy; }
        public double ResisRup
        {
            get//=> 0.75 * ELEGIR(Q22, 0.81, 1.3, 2.01, 2.71, 3.55, 4.46, 5.73, 6.8, 8.35) * Fuanc; 
            {
                double[] temp = { 0.81, 1.3, 2.01, 2.71, 3.55, 4.46, 5.73, 6.8, 8.35 };
                return 0.75 * temp[Convert.ToInt32(Ancla.IdAncla) - 1] * AceroAnclas_Fu;
                //temp[Convert.ToInt32(Ancla.IdAncla)-1]
            }
        }
        public double Q { get => Math.Max(ZonaRevision1.Q, ZonaRevision2.Q); }
        public double PorcentajeTenAncla { get => 100 * (TensionMax / Ancla.CantidadDeAnclas + Q) / ResisTension; }
        public double PorcentajeRupAncla { get => 100 * (TensionMax / Ancla.CantidadDeAnclas + Q) / ResisRup; }
        //100*(H22/P22+AZ22)/AY22
        //Interacción
        public double PorcentajeTen_V { get => PorcentajeCorAncla + PorcentajeTenAncla; }

    }
}
