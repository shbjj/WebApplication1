using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tornilleria
{
    public class Perfil
    {
        public string NombrePerfil { get; set; }//Recibir desde la Interfaz
        public double Espesor { get; set; }//Recibir desde la Interfaz
        public double Dimension { get; set; }//Recibir desde la Interfaz
        public int Gramil
        {
            get
            {
                Dictionary<string, int> gramil = new Dictionary<string, int>();
                gramil.Add("L15152.5", 20);
                gramil.Add("L17172.5", 25);
                gramil.Add("L15153", 20);
                gramil.Add("L20202.5", 30);
                gramil.Add("L17173", 25);
                gramil.Add("L20203", 30);
                gramil.Add("L25252.5", 35);
                gramil.Add("L25253", 35);
                gramil.Add("L20204", 30);
                gramil.Add("L30303", 45);
                gramil.Add("L25254", 35);
                gramil.Add("L30304", 45);
                gramil.Add("L35354", 50);
                gramil.Add("L25256", 35);
                gramil.Add("L30305", 45);
                gramil.Add("L40404", 60);
                gramil.Add("L35355", 50);
                gramil.Add("L30306", 45);
                gramil.Add("L40405", 60);
                gramil.Add("L30308", 45);
                gramil.Add("L40406", 60);
                gramil.Add("L50505", 70);
                gramil.Add("L40407", 60);
                gramil.Add("L50506", 70);
                gramil.Add("L40408", 60);
                gramil.Add("L50507", 70);
                gramil.Add("L60606", 90);
                gramil.Add("L404010", 60);
                gramil.Add("L50508", 70);
                gramil.Add("L60607", 90);
                gramil.Add("L60608", 90);
                gramil.Add("L606010", 90);
                gramil.Add("L606012", 90);
                gramil.Add("L808012", 120);
                gramil.Add("L808014", 120);
                gramil.Add("LA15152.5", 20);
                gramil.Add("LA17172.5", 25);
                gramil.Add("LA15153", 20);
                gramil.Add("LA20202.5", 30);
                gramil.Add("LA17173", 25);
                gramil.Add("LA20203", 30);
                gramil.Add("LA25252.5", 35);
                gramil.Add("LA25253", 35);
                gramil.Add("LA20204", 30);
                gramil.Add("LA30303", 45);
                gramil.Add("LA25254", 35);
                gramil.Add("LA30304", 45);
                gramil.Add("LA35354", 50);
                gramil.Add("LA25256", 35);
                gramil.Add("LA30305", 45);
                gramil.Add("LA40404", 60);
                gramil.Add("LA35355", 50);
                gramil.Add("LA30306", 45);
                gramil.Add("LA40405", 60);
                gramil.Add("LA30308", 45);
                gramil.Add("LA40406", 60);
                gramil.Add("LA50505", 70);
                gramil.Add("LA40407", 60);
                gramil.Add("LA50506", 70);
                gramil.Add("LA40408", 60);
                gramil.Add("LA50507", 70);
                gramil.Add("LA60606", 90);
                gramil.Add("LA404010", 60);
                gramil.Add("LA50508", 70);
                gramil.Add("LA60607", 90);
                gramil.Add("LA60608", 90);
                gramil.Add("LA606010", 90);
                gramil.Add("LA606012", 90);
                gramil.Add("LA808012", 120);
                gramil.Add("LA808014", 120);

                return gramil[NombrePerfil];
            }
        }
        public double Peso
        {
            get
            {
                Dictionary<string, double> pesos = new Dictionary<string, double>();
                pesos.Add("L15152.5"    ,22.07);
pesos.Add("L17172.5"  ,25.95);
pesos.Add("L15153"  ,26.19);
pesos.Add("L20202.5"  ,29.83);
pesos.Add("L17173"  ,30.85);
pesos.Add("L20203"  ,35.50);
pesos.Add("L25252.5"  ,37.59);
pesos.Add("L25253"  ,44.82);
pesos.Add("L20204"  ,46.56);
pesos.Add("L30303"  ,54.13);
pesos.Add("L25254"  ,58.98);
pesos.Add("L30304"  ,71.39);
pesos.Add("L35354"  ,83.81);
pesos.Add("L25256"  ,86.13);
pesos.Add("L30305"  ,88.27);
pesos.Add("L40404"  ,96.23);
pesos.Add("L35355"  ,103.79);
pesos.Add("L30306"  ,104.76);
pesos.Add("L40405"  ,119.31);
pesos.Add("L30308"  ,136.58);
pesos.Add("L40406"  ,142.01);
pesos.Add("L50505"  ,150.35);
pesos.Add("L40407"  ,164.32);
pesos.Add("L50506"  ,179.26);
pesos.Add("L40408"  ,186.25);
pesos.Add("L50507"  ,207.78);
pesos.Add("L60606"  ,216.51);
pesos.Add("L404010" ,228.93);
pesos.Add("L50508"  ,235.91);
pesos.Add("L60607"  ,251.24);
pesos.Add("L60608"  ,285.58);
pesos.Add("L606010" ,353.09);
pesos.Add("L606012" ,419.05);
pesos.Add("L808012" ,568.05);
pesos.Add("L808014" ,657.29);
pesos.Add("LA15152.5" ,22.07);
pesos.Add("LA17172.5" ,25.95);
pesos.Add("LA15153" ,26.19);
pesos.Add("LA20202.5" ,29.83);
pesos.Add("LA17173" ,30.85);
pesos.Add("LA20203" ,35.50);
pesos.Add("LA25252.5" ,37.59);
pesos.Add("LA25253" ,44.82);
pesos.Add("LA20204" ,46.56);
pesos.Add("LA30303" ,54.13);
pesos.Add("LA25254" ,58.98);
pesos.Add("LA30304" ,71.39);
pesos.Add("LA35354" ,83.81);
pesos.Add("LA25256" ,0);
pesos.Add("LA30305" ,88.27);
pesos.Add("LA40404" ,96.23);
pesos.Add("LA35355" ,103.79);
pesos.Add("LA30306" ,104.76);
pesos.Add("LA40405" ,119.31);
pesos.Add("LA30308" ,136.58);
pesos.Add("LA40406" ,142.01);
pesos.Add("LA50505" ,150.35);
pesos.Add("LA40407" ,164.32);
pesos.Add("LA50506" ,179.26);
pesos.Add("LA40408" ,186.25);
pesos.Add("LA50507" ,207.78);
pesos.Add("LA60606" ,216.51);
pesos.Add("LA404010" ,228.93);
pesos.Add("LA50508" ,235.91);
pesos.Add("LA60607" ,251.24);
pesos.Add("LA60608" ,285.58);
pesos.Add("LA606010" ,353.09);
pesos.Add("LA606012" ,419.05);
pesos.Add("LA808012" ,568.05);
pesos.Add("LA808014" ,657.29);
                return pesos[NombrePerfil];
            }
        }
    }
}
