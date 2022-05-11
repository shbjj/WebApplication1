using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class CalculosTornilleria
    {
        internal static void CalculosTornillos()
        {
            int iGpo, jRosca, iDiam_tor;
            string Perfil, Nombre_tor, Gpo, Rosca;
            double Fza_T, Fza_C, Fu, Fy;
            double Diam_tor;
            double Fn, Fnv;
            double t_Perfil, B_Perfil, gr_Perfil;
            double ListaFnv_NA;
            double[] ListaFnv1 = new double[5];
            double[] ListaFnv2 = new double[5];
            double[,] ListaScrews = new double[10, 10];
            string Conexion;
            double Ab, Rnv, Ruv, RuvTot, Nv_C, Dummy; //  Por cortante
            double Rna, Rua, Rua_tot, Nv_AP;          //  Por aplastamiento
            double Diam_a_tor, Lc1, Rnd1, Rud1;       //  Por desgarre
            double s, Lc2, Rnd2, Rud2, Nd;
            Double Dmin;
            double Anv1, Anv2, Ant, Agv1, Agv2;       // Por bloque de cortante
            double Rnt, Rut, Rnv1, Rnv2, Rgv1, Rgv2;
            double Runv1, Runv2, Rugv1, Rugv2, Ubs;
            double Nbnv, Nbgv, Nbv;

            // Valores de las celdas de Excel
            Perfil = "L404010";//Tabla 
            Nombre_tor = "1/2";//Usuario
            Gpo = "A";//Usuario
            Rosca = "No";//Usuario
            Fza_T = 81.15;//Usuario
            Fza_C = -80.839;//Usuario
            Fu = 345;//Usuario
            Fy = 250;//Usuario
            Conexion = "Empalme";//Usuario

            // Estos valores se tienen que obtener de acuerdo al perfil seleccionado de la lista de perfiles
            //Estos valores si vienen del EXCEL
            t_Perfil = 15.875;//Ya aparecen el la BD
            B_Perfil = 101.6;//Ya aparecen el la BD
            gr_Perfil = 60;

            //Se definen
            jRosca = 0;//Tambien es variable temporal, se puede optimizar
            iDiam_tor = 0;//Variable temporal, se peude quitar

            // Tabla de tornillos
            ListaScrews[1, 1] = 12.7; ListaScrews[1, 2] = 19.05;          // 1/2
            ListaScrews[2, 1] = 15.875; ListaScrews[2, 2] = 22.225;       // 5 /8
            ListaScrews[3, 1] = 19.05; ListaScrews[3, 2] = 25.4;          // 3/4
            ListaScrews[4, 1] = 22.225; ListaScrews[4, 2] = 28.575;       // 7/8
            ListaScrews[5, 1] = 25.4; ListaScrews[5, 2] = 31.75;          // 1"
            ListaScrews[6, 1] = 28.575; ListaScrews[6, 2] = 38.1;         // 1" 1/8
            ListaScrews[7, 1] = 31.75; ListaScrews[7, 2] = 41.275;        // 1" 1/4
            ListaScrews[8, 1] = 34.925; ListaScrews[8, 2] = 43.65625;     // 1" 3/8
            ListaScrews[9, 1] = 38.1; ListaScrews[9, 2] = 47.625;        // 1" 1/2

            // Tablas Fnv grupo A, B, C o 347, con rosca y sin rosca
            ListaFnv_NA = 188;
            ListaFnv1[1] = 372; ListaFnv2[1] = 469;
            ListaFnv1[2] = 469; ListaFnv2[2] = 579;
            ListaFnv1[3] = 620; ListaFnv2[3] = 779;
            ListaFnv1[4] = 155.25; ListaFnv2[4] = 194.235;

            if (Gpo == "A307")
            {
                Rosca = "N/A";
                Fn = ListaFnv_NA;
            }
            else
            {
                if (Rosca == "No")
                { jRosca = 1; }
                else
                { jRosca = 2; }
            }
            switch (Gpo)
            {
                case "A":
                    iGpo = 1;
                    break;
                case "B":
                    iGpo = 2;
                    break;
                case "C":
                    iGpo = 3;
                    break;
                default:
                    iGpo = 4;
                    break;
            }
            if (jRosca == 1)
            {
                Fnv = ListaFnv1[iGpo];
            }
            else
            {
                Fnv = ListaFnv2[iGpo];
            }

            switch (Nombre_tor)
            {
                case "1/2":
                    iDiam_tor = 1;
                    break;
                case "5/8":
                    iDiam_tor = 2;
                    break;
                case "3/4":
                    iDiam_tor = 3;
                    break;
                case "7/8":
                    iDiam_tor = 4;
                    break;
                case "1":
                    iDiam_tor = 5;
                    break;
                case "1 1/8":
                    iDiam_tor = 6;
                    break;
                case "1 1/4":
                    iDiam_tor = 7;
                    break;
                case "1 3/8":
                    iDiam_tor = 8;
                    break;
                case "1 1/2":
                    iDiam_tor = 9;
                    break;
            }
            Diam_tor = ListaScrews[iDiam_tor, 1];


            // 'Revisión por cortante
            Ab = Math.PI * Diam_tor * Diam_tor / 4;
            Rnv = Fnv * Ab;
            Ruv = 0.75 * Rnv;
            if (Math.Abs(Fza_T) > Math.Abs(Fza_C))
            {
                RuvTot = Fza_T;
            }
            else
            {
                RuvTot = Fza_C;
            }

            if (Conexion == "Simple")
            {
                Nv_C = Math.Round(RuvTot / Ruv + 1, 0);
            }
            else
            {
                Dummy = Math.Round(RuvTot / (Ruv * 2) + 1, 0);
                if (Dummy == 1 || Dummy == 3 || Dummy == 5 || Dummy == 7 || Dummy == 9 || Dummy == 11)
                {
                    Nv_C = Dummy + 1;
                }
                else
                {
                    Nv_C = Math.Round(RuvTot / (Ruv * 2) + 1, 0);
                }
            }
            // Imprime resultados:
            //  Fnv, Ab, Rnv, Ruv, RuvTot, Nv_C

            //'Revisión por aplastamiento

            Rna = 2.4 * Diam_tor * t_Perfil * Fu;
            Rua = 0.75 * Rna;
            if (Math.Abs(Fza_T) > Math.Abs(Fza_C))
            {
                Rua_tot = Fza_T;
            }
            else
            {
                Rua_tot = Fza_C;
            }
            if (Conexion == "Simple")
            {

                Nv_AP = Math.Round(Rua_tot / Rua + 1, 0);
            }
            else
            {
                Dummy = Math.Round(Rua_tot / Rua + 1, 0);
                if (Dummy == 1 || Dummy == 3 || Dummy == 5 || Dummy == 7 || Dummy == 9 || Dummy == 11)
                {
                    Nv_AP = Dummy + 1;
                }
                else
                {
                    Nv_AP = Math.Round(RuvTot / (Ruv * 2) + 1, 0);
                }
            }

            // Imprime resultados
            // t_Perfil, Rna, Rua, Rua_tot, Nv_AP

            // Revisión por desgarre
            if (Diam_tor < 25.4)
            {
                Diam_a_tor = ListaScrews[iDiam_tor, 1] + 1.6;
            }
            else
            {
                Diam_a_tor = ListaScrews[iDiam_tor, 1] + 3.2;
            }
            Dmin = ListaScrews[iDiam_tor, 2];
            Lc1 = Dmin - Diam_a_tor / 2;
            Rnd1 = 2.1 * Lc1 * t_Perfil * Fu;
            Rud1 = 0.75 * Rnd1;
            s = 3 * Diam_tor;
            Lc2 = s - Diam_a_tor;
            Rnd2 = 2.1 * Lc2 * t_Perfil * Fu;
            Rud2 = 0.75 * Rnd2;
            if (Conexion == "Simple")
            {
                if (Fza_T > Rud1)
                {
                    Nd = (Fza_T - Rud1) / Rud2 + 1;
                }
                else
                {
                    Nd = 1;
                }
            }
            else
            {
                if (Fza_T > (2 * Rud1))
                {
                    Nd = (Fza_T - 2 * Rud1) / Rud2 + 2;
                }
                else
                {
                    Nd = 2;
                }
            }

            // Imprime resultados
            // Diam_a_tor, Dmin, s, Lc1, Rnd1, Rud1, Lc2, Rnd2, Rud2, Nd

            // Revisión por bloque de cortante
            Anv1 = Lc1 * t_Perfil;
            Anv2 = Lc2 * t_Perfil;
            Ant = (B_Perfil - gr_Perfil - Diam_a_tor / 2) * t_Perfil;
            Agv1 = Dmin * t_Perfil;
            Agv2 = s * t_Perfil;
            Ubs = 1;
            Rnt = Ubs * Fu * Ant;
            Rut = 0.75 * Rnt;
            Rnv1 = 0.6 * Fu * Anv1;
            Rnv2 = 0.6 * Fu * Anv2;
            Runv1 = 0.75 * Rnv1;
            Runv2 = 0.75 * Rnv2;
            Rgv1 = 0.6 * Fy * Agv1;
            Rgv2 = 0.6 * Fy * Agv2;
            Rugv1 = 0.75 * Rgv1;
            Rugv2 = 0.75 * Rgv2;

            if (Conexion == "Simple")
            {
                if (Fza_T > (Rut + Runv1))
                {
                    Nbnv = (Fza_T - Rut - Runv1) / Runv2 + 1;
                }
                else
                {
                    Nbnv = 1;
                }
            }
            else
            {
                if (Fza_T > (2 * (Rut + Runv1)))
                {
                    Nbnv = (Fza_T - 2 * (Rut + Runv1)) / Runv2 + 2;
                }
                else
                {
                    Nbnv = (Fza_T - 2 * (Rut + Runv1)) / Runv2 + 2;
                }
            }
            if (Conexion == "Simple")
            {
                if (Fza_T > (Rut + Rugv1))
                {
                    Nbgv = (Fza_T - Rut - Rugv1) / Rugv2 + 1;
                }
                else
                {
                    Nbgv = 1;
                }
            }
            else
            {
                if (Fza_T > (2 * (Rut + Rugv1)))
                {
                    Nbgv = (Fza_T - 2 * (Rut + Rugv1)) / Rugv2 + 2;
                }
                else
                {
                    Nbgv = 2;
                }
            }
            if (Nbnv > Nbgv)
            {
                Nbv = Nbnv;
            }
            else
            {
                Nbv = Nbgv;
            }
            // Imprime resultados
            // Anv1, Anv2, Ant, Agv1, Agv2, Rnt, Rut, Rnv1, Rnv2
            // Runv1, Runv2, Rgv1, Rgv2, Rugv1, Rugv2, Nbnv, Nbgv, Nbv

            return;
        }
    }
}

