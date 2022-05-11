using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //Para obtener ruta relativa
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            QuerysToBD _ListaConsultaBD = new QuerysToBD();
            List<SubEstacion> subEstaciones = _ListaConsultaBD.consultaBD_SEs2();
            return View(subEstaciones);
        }
        [HttpGet]
        public IActionResult CalculoTornilleria()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CalculoTornilleria(IFormCollection formCollection)
        {
            //Recibir los parametros del Form y agregarlos al ViewData
            string _subestacion = formCollection["subestacion"];
            ViewData["_subestacion"] = _subestacion;


            string _marco = formCollection["marco"];
            ViewData["_marco"] = _marco;

            string _noPieza = formCollection["noPieza"];
            ViewData["_noPieza"] = _noPieza;

            string _datoElemento = formCollection["datoElemento"];
            ViewData["_datoElemento"] = _datoElemento;

            string _datoTipo = formCollection["datoTipo"];
            ViewData["_datoTipo"] = _datoTipo;

            string _nombrePiezaMarco = formCollection["nombrePiezaMarco"];
            if(_nombrePiezaMarco.ElementAt(0)=='c')
            {
                _nombrePiezaMarco = "Columna " + _nombrePiezaMarco.ElementAt(1);
            }
            else
            {
                _nombrePiezaMarco = "Trabe " + _nombrePiezaMarco.ElementAt(1);
            }
            ViewData["_nombrePiezaMarco"] = _nombrePiezaMarco;


            //Declarar variables (?
            double[,] ListaScrews = new double[10, 3];
            string[,] ListaGpos = new string[35, 3];
            int NGpo;

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

            // Tabla Gpos
            ListaGpos[1, 1] = "_C_S1"; ListaGpos[1, 2] = "Cuerdas cuerpo piramidal, cara X y Z";
            ListaGpos[2, 1] = "_C_S2"; ListaGpos[2, 2] = "Cuerdas cuerpo recto, cara X y Z";
            ListaGpos[3, 1] = "_C_S3"; ListaGpos[3, 2] = "Cuerdas extensión, cara X y Z";
            ListaGpos[4, 1] = "_D_S1_X"; ListaGpos[4, 2] = "Diagonales cuerpo piramidal cara X";
            ListaGpos[5, 1] = "_D_S1_Z"; ListaGpos[5, 2] = "Diagonales cuerpo piramidal cara Z";
            ListaGpos[6, 1] = "_D_S2_X"; ListaGpos[6, 2] = "Diagonales soporte y cuerpo recto, cara X";
            ListaGpos[7, 1] = "_D_S2_Z"; ListaGpos[7, 2] = "Diagonales soporte y cuerpo recto, cara Z";
            ListaGpos[8, 1] = "_D_S3_X"; ListaGpos[8, 2] = "Diagonales unión trabe de remate, cara X";
            ListaGpos[9, 1] = "_D_S3_Z"; ListaGpos[9, 2] = "Diagonales unión trabe de remate, cara Z";
            ListaGpos[10, 1] = "_D_S4_X"; ListaGpos[10, 2] = "Diagonales unión trabe superior, cara X";
            ListaGpos[11, 1] = "_D_S4_Z"; ListaGpos[11, 2] = "Diagonales unión trabe de suspensión, cara Z";
            ListaGpos[12, 1] = "_D_S5_X"; ListaGpos[12, 2] = "Diagonales unión trabe superior, cara X";
            ListaGpos[13, 1] = "_D_S5_Z"; ListaGpos[13, 2] = "Diagonales unión trabe superior, cara Z";
            ListaGpos[14, 1] = "_D_S6"; ListaGpos[14, 2] = "Diagonales extensión, cara X y Z";
            ListaGpos[15, 1] = "_P_S1"; ListaGpos[15, 2] = "Panel (rombo) soporte y trabe de remate, cara X y Z";
            ListaGpos[16, 1] = "_P_S2"; ListaGpos[16, 2] = "Panel (rombo) trabe de suspensión, cara X y Z";
            ListaGpos[17, 1] = "_P_S3"; ListaGpos[17, 2] = "Panel (rombo) trabe superior, cara X y Z";
            ListaGpos[18, 1] = "_T_S1"; ListaGpos[18, 2] = "Transversales soporte y trabe de remate, cara X y Z";
            ListaGpos[19, 1] = "_T_S2"; ListaGpos[19, 2] = "Transversales trabe de suspensión, cara X y Z";
            ListaGpos[20, 1] = "_T_S3"; ListaGpos[20, 2] = "Transversales trabe superiror, cara X y Z";
            ListaGpos[21, 1] = "_T_S4"; ListaGpos[21, 2] = "Transversales capitel, cara X y Z";

            //Agregar al listBox los valores de ListaScrews
            //Enviar el ListaScrews a la vista
            ViewData["_ListaScrews"] = ListaScrews;//No se ocupa ya que se llenno a mano

            //Visualizar los valores
            //Subestacion
            //MArco
            //Pieza
            //Elemento (abajo)
            //Tipo

            NGpo = 0;
            if (_datoElemento == "trabe")
            {
                //Si el elemento es una trabe
                //Elegir la imagen
                ViewData["_imagen"] = "/Imagenes/Grupos_Trabes.PNG";
                //Dependiendo el tipo de dato, cambiar NGpo
                switch (_datoTipo)
                {
                    case "1":
                        NGpo = 4;
                        break;
                    case "2":
                        NGpo = 7;
                        break;
                    case "3":
                        NGpo = 7;
                        break;
                }
            }
            else
            {
                ViewData["_imagen"] = "/Imagenes/Grupos_Columnas_Tipo1_2y3.PNG";
                switch (_datoTipo)
                {
                    case "1":
                        NGpo = 20;
                        break;
                    case "2":
                        NGpo = 16;
                        break;
                    case "3":
                        NGpo = 12;
                        break;
                    case "4":
                        NGpo = 6;
                        break;
                }
            }

            //Enviar NGpo
            ViewData["_NGpo"] = NGpo;
            //Consultar los grupos y mandarlos a  la vista
            QuerysToBD _ConsultaGpos = new QuerysToBD();
            ViewData["_listaGrupos"] = _ConsultaGpos.ConsultaGrupo(_noPieza);
            //Poner los datos en la tabla

            //Poner los ListaGpos en la tabla (enviar el ListaGpos)
            ViewData["_ListaGpos"] = ListaGpos;


            return CalculoTornilleria();
        }


        public IActionResult PlacaBase(IFormCollection formCollection)
        {
            ViewData["grupo"] = formCollection["grupo"];
            ViewData["perfil"] = formCollection["perfil"];
            ViewData["nombrePiezaMarco"] = formCollection["nombrePiezaMarco"];
            //Obtener la lista de tamaños de Anclas
            //Para obtener ruta relativa
            List<Ancla> listaAnclas = LeerCSV.LeerListaAncla(_hostingEnvironment.WebRootPath);
            ViewData["listaAnclas"] = listaAnclas;
            //Obtener la lista de Perfiles con sus lados y centroides
            //Para obtener ruta relativa
            List<PerfilDelAISC> listaPerfiles = LeerCSV.LeerCatalogo(_hostingEnvironment.WebRootPath);
            ViewData["listaPerfiles"] = listaPerfiles;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult PlacaColumna(IFormCollection formCollection)
        {
            //Para obtener ruta relativa
            List<Ancla> listaAnclas = LeerCSV.LeerListaAncla(_hostingEnvironment.WebRootPath);
            ViewData["listaAnclas"] = listaAnclas;
            //Obtener la lista de Perfiles con sus lados y centroides
            //Para obtener ruta relativa
            List<PerfilDelAISC> listaPerfiles = LeerCSV.LeerCatalogo(_hostingEnvironment.WebRootPath);
            ViewData["listaPerfiles"] = listaPerfiles;
            return View();
        }
    }
}
    
