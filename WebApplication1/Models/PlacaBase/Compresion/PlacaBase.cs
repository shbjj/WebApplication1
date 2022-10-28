using System;

namespace WebApplication1.Models.PlacaBase.Compresion
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
        public double CompresionMax { get; set; }//Dato usuario

        //Aun no se si se ocupan
        public string PiezaElemento { get; set; }//Dato usuario
        public string Grupo { get; set; }//Dato usuario
        public string NombrePerfil { get; set; }//Dato usuario

        public double EsfuerzoConcreto { get => 0.75 * 0.8 * Concreto; }//Igual a Tension
        public double AreaRequerida { get => Math.Round((CompresionMax / EsfuerzoConcreto), 1); }//Igual a tension
        public double Cortante { get; set; }//Dato usuario      //Igual a tension

        public Perfil Perfil { get; set; }//Dato usuario        //Igual a tension
        //
        public Ancla Ancla { get; set; }                        //Igual a tension
        public double AnchoPlaca
        {                                                       //Igual a tension
            get =>
                Math.Max((2 * (Perfil.Centroide +
                    (Ancla.DistanciaBordeRecortadoMin + 0.95 * Ancla.DiametroAncla) / 10)),
                    (Math.Pow(AreaRequerida, 0.5)));
        }
        public double AnchoPropuesto { get => AnchoPlaca + 5 - (AnchoPlaca % 5); }//Dato usuario//Calculado //Igual a tension
        public double AreaPlaca { get => AnchoPropuesto * AnchoPropuesto; } //Igual a tension

        public ZonaRevision1 ZonaRevision1 { get; set; }
        public ZonaRevision2 ZonaRevision2 { get; set; }
        public ZonaRevision3 ZonaRevision3 { get; set; }
        public double EspPlacaPorCompresión //Parecido, no igual
        {
            get => Math.Max(ZonaRevision1.EspesorPlaca, Math.Max(ZonaRevision2.EspesorPlaca, ZonaRevision3.EspesorPlaca));
        }
        public double EspPlacaPorTensión { get; set; }
        //Aplast. x el cortante, placa		
        public double RaAncla
        {
            get => Math.Min(1.2 * (Ancla.DistanciaBordeRecortadoDet - (Ancla.DiametroAncla + (25.4 / 16)) / (10 * 2)) * EspPlacaPorTensión * AceroPlaca_Fu, 2.4 * (Ancla.DiametroAncla / 10) * EspPlacaPorTensión * AceroPlaca_Fu);
            //MIN(1.2*(T22-(O22+(25.4/16))/(10*2))*AQ22*Fu , 2.4*(O22/10)*AQ22*Fu)
        }
        public double ResisAplastamiento { get => 0.75 * 2 * (RaAncla); }
        public double PorcentajeApla { get => 100 * Cortante / ResisAplastamiento / Ancla.CantidadDeAnclas; }
        //Cortante en anclas
        //Igual
        public double RV1Ancla { get => Math.Pow((Ancla.DiametroAncla / 10), 2) * Math.PI / 4 * 0.55 * 0.9 * AceroAnclas_Fy; }
        //Igual
        public double ResisCort { get => RV1Ancla * Ancla.CantidadDeAnclas; }
        //Igual
        public double PorcentajeCorAncla { get => 100 * Cortante / ResisCort; }

    }
}
