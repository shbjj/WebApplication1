using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LeerCSV
    {
        public static List<Ancla> LeerListaAncla(String path)
        {
            //Variable contadora
            int cont = 0;


            //Guardar en una lista de Anclas
            List<Ancla> listA = new List<Ancla>();

            //Obtener todo el texto del archivo CSV
            string csvData = System.IO.File.ReadAllText(path + "/csv/Varillas_y_placas.csv");

            //Recorrer la informacion, por cada renglon
            foreach (string row in csvData.Split("\n"))
            {
                //Si el renglon no esta vacio
                if (!string.IsNullOrEmpty(row))
                {
                    if(cont>2)
                    {
                        listA.Add(new Ancla(row.Split(",")[1], Convert.ToDouble(row.Split(",")[2])));
                    }
                    //Si el contador es igual a 11, terminar
                    if(cont==8)
                    {
                        break;
                    }
                }

                //Aumentar contador
                cont++;
            }
            return listA;
        }
        public static List<PerfilDelAISC> LeerCatalogo(String path)
        {
            //Variable contadora
            int cont = 0;


            //Guardar en una lista de PerfilDelAISC
            List<PerfilDelAISC> listA = new List<PerfilDelAISC>();

            //Obtener todo el texto del archivo CSV
            string csvData = System.IO.File.ReadAllText(path + "/csv/Catalogo.csv");

            //Recorrer la informacion, por cada renglon
            foreach (string row in csvData.Split("\n"))
            {
                //Si el renglon no esta vacio
                if (!string.IsNullOrEmpty(row))
                {
                    if (cont > 4)//Empezar a leer apartir del 5to renglon
                    {
                        listA.Add(
                                new PerfilDelAISC(
                                    Convert.ToInt32(row.Split(",")[0]),//Agregar ID
                                    Convert.ToDouble(row.Split(",")[3]),//Agregar Lado
                                    Convert.ToDouble(row.Split(",")[17])//Agregar Centroide
                                )
                            );
                    }
                    //Si el contador es igual a 11, terminar
                    if (cont == 72)
                    {
                        break;
                    }
                }

                //Aumentar contador
                cont++;
            }
            return listA;
        }
    }
}
