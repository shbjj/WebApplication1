

using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using WebApplication1.Models;
using WebApplication1.Models.Tornilleria;

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
            ViewData["_datoElemento"] = _datoElemento;//Columna -ejemplo

            string _datoTipo = formCollection["datoTipo"];
            ViewData["_datoTipo"] = _datoTipo;//num de columna

            string _nombrePiezaMarco; 
            if (_datoElemento.ElementAt(0) == 'c')
            {
                _nombrePiezaMarco = "Columna " + _datoTipo;
            }
            else
            {
                _nombrePiezaMarco = "Trabe " + _datoTipo;
            }
            ViewData["_nombrePiezaMarco"] = _nombrePiezaMarco;


            //NGpo = 0;
            if (_datoElemento == "trabe")
            {
                //Si el elemento es una trabe
                //Elegir la imagen
                ViewData["_imagen"] = "/Imagenes/Grupos_Trabes.PNG";
                //Dependiendo el tipo de dato, cambiar NGpo
                /*switch (_datoTipo)
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
                }*/
            }
            else
            {
                ViewData["_imagen"] = "/Imagenes/Grupos_Columnas_Tipo1_2y3.PNG";
                /*switch (_datoTipo)
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
                }*/
            }

            //Enviar NGpo
            //ViewData["_NGpo"] = NGpo;
            //Consultar los grupos y mandarlos a  la vista
            QuerysToBD _ConsultaGpos = new QuerysToBD();
            ViewData["_listaGrupos"] = _ConsultaGpos.ConsultaGrupoPerfil(_noPieza, _hostingEnvironment.WebRootPath);
            //Poner los datos en la tabla

            //Poner los ListaGpos en la tabla (enviar el ListaGpos)
            //ViewData["_ListaGpos"] = ListaGpos;


            return CalculoTornilleria();
        }

        public IActionResult DescargarPlantilla(IFormCollection formCollection)
        {
            string noPieza = formCollection["noPieza"];
            if (noPieza==null | noPieza.Equals(""))
            {
                return NotFound();
            }
            //Obtener los datos de Grupo y Perfil
            QuerysToBD _ConsultaGpos = new QuerysToBD();
            List<GrupoPerfil> gruposPerfiles = _ConsultaGpos.ConsultaGrupoPerfil(noPieza, _hostingEnvironment.WebRootPath);

            //Leer archivo Excel
            var file = "DatosEntrada.xlsx";
            string filename = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\plantillas"}" + "\\" + file;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var libros = new XLWorkbook(filename))
            {

                //Obtener la hoja
                var hoja = libros.Worksheet(1);
                //Realizar los cambios
                int cont = 5;
                foreach (GrupoPerfil grupoPerfil in gruposPerfiles)
                {
                    //Perfil nombre
                    hoja.Cell("A" + cont).SetValue(grupoPerfil.NombrePerfil);
                    //Perfil Espesor
                    hoja.Cell("B" + cont).SetValue(grupoPerfil.EspesorPerfil);
                    //Perfil Dimension
                    hoja.Cell("C" + cont).SetValue(grupoPerfil.DimensionPerfil);
                    //Grupo Nombre
                    hoja.Cell("J" + cont).SetValue(grupoPerfil.NombreGrupo);
                    //Grupo Ubicación
                    hoja.Cell("K" + cont).SetValue(grupoPerfil.Ubicacion);

                    cont++;
                }

                using (var memori = new MemoryStream())
                {
                    libros.SaveAs(memori);
                    var nombreExcel = string.Concat("plantilla", ".xlsx");

                    return File(memori.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }
        }
        [HttpPost]
        public virtual ActionResult Test(IFormFile file2)
        {
            // to do :Look through productImg and do something  
            return Ok(1);
        }
        [HttpPost]
        public IActionResult loadFile(IFormFile file2, [FromServices] IWebHostEnvironment he)
        {
            List<ElementoTornilleria> lista = new List<ElementoTornilleria>();
            string filename = $"{he.WebRootPath}\\files\\{file2.FileName}";
            // lo que esta entre diagonales invertidas "files", es el nombre de la carpeta que esta por default o puedes
            // crear una nueva para que esa que crees la sustituyes por ese nombre.
            var name = file2.FileName;
            using (FileStream filestream = System.IO.File.Create(filename))
            {
                file2.CopyTo(filestream);
                filestream.Flush();
            }
            // en este using hace que se guarde el archivo
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var filename2 = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + name;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            SLDocument sl = new SLDocument(filename2);
            double Fy;
            double Fu;
            double Ubs;
            using (var stream = System.IO.File.Open(filename2, FileMode.Open, FileAccess.Read))
            {
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var result = reader.AsDataSet();
                DataTable table = result.Tables[0];
                DataRow row = table.Rows[0];
                var rt = table.Columns.Count;
                var ft = table.Rows.Count;
                var temp = sl.GetCellValueAsString(6, 4);
                //Variable para obtener valores de los tornillos
                Tornillos tornillos = new Tornillos();

                //Leer archivo
                //Obtener Fy, Fu, Ubs
                Fy = sl.GetCellValueAsDouble(2, 1);
                Fu = sl.GetCellValueAsDouble(2, 2);
                Ubs = sl.GetCellValueAsDouble(2, 3);
                //Leer los grupos y perfiles
                for (int cont = 5; cont <= ft; cont++)
                {
                    try
                    {
                        //Obtener datos
                        //Perfil
                        String nombrePerfil = sl.GetCellValueAsString(cont, 1);
                        double espesorPerfil = sl.GetCellValueAsDouble(cont, 2);
                        double dimensionPerfil = sl.GetCellValueAsDouble(cont, 3);
                        //Tornillo
                        String plgTornillo = sl.GetCellValueAsString(cont, 4);
                        String grupoTornillo = sl.GetCellValueAsString(cont, 5);
                        String roscaTornillo = sl.GetCellValueAsString(cont, 6);
                        String conexionTornillo = sl.GetCellValueAsString(cont, 7);
                        //Fuerzas
                        double tension = sl.GetCellValueAsDouble(cont, 8);
                        double compresion = sl.GetCellValueAsDouble(cont, 9);
                        //Grupo
                        string nombreGrupo = sl.GetCellValueAsString(cont, 10);
                        string ubicacion = sl.GetCellValueAsString(cont, 11);


                        ElementoTornilleria elementoTemp = new ElementoTornilleria();
                        //Llenar datos de elementoTemp
                        //Grupo
                        elementoTemp.Grupo = new Grupo();
                        elementoTemp.Grupo.Nombre = nombreGrupo;
                        elementoTemp.Grupo.Ubicacion = ubicacion;
                        //
                        elementoTemp.Perfil = new Models.Tornilleria.Perfil();
                        elementoTemp.Perfil.NombrePerfil = nombrePerfil;
                        elementoTemp.Perfil.Espesor = espesorPerfil;
                        elementoTemp.Perfil.Dimension = dimensionPerfil;
                        //Tornillo
                        elementoTemp.Tornillo = new Tornillo();
                        elementoTemp.Tornillo.Plg = plgTornillo;
                        elementoTemp.Tornillo.Mm = tornillos.Dictionary[plgTornillo].Mm; 
                        elementoTemp.Tornillo.Grupo = grupoTornillo;
                        elementoTemp.Tornillo.Rosca = roscaTornillo;
                        elementoTemp.Tornillo.TipoConexion = conexionTornillo;
                        elementoTemp.Tornillo.Dmin = tornillos.Dictionary[plgTornillo].Dmin;
                        //Fuerzas
                        elementoTemp.Fuerzas = new Fuerzas();
                        elementoTemp.Fuerzas.Tension = tension;
                        elementoTemp.Fuerzas.Compresion = compresion;
                        //Agregarle el numero "consecutivo de contador" Ejemplo 1,2,3,4...
                        elementoTemp.Numero = cont - 2;
                        //RevisionResistenciaCortante
                        elementoTemp.RevisionResistenciaCortante = new RevisionResistenciaCortante();
                        elementoTemp.RevisionResistenciaCortante._grupoTornillo = grupoTornillo;
                        elementoTemp.RevisionResistenciaCortante._roscaTornillo = roscaTornillo;
                        elementoTemp.RevisionResistenciaCortante._tipoConexion = conexionTornillo;
                        elementoTemp.RevisionResistenciaCortante._mmTornillo = elementoTemp.Tornillo.Mm;//datosEntrada.ElementoTornillerias[i].Tornillo.Mm;
                        elementoTemp.RevisionResistenciaCortante._tension = tension;//datosEntrada.ElementoTornillerias[i].Fuerzas.Tension;
                        elementoTemp.RevisionResistenciaCortante._compresion = compresion; //datosEntrada.ElementoTornillerias[i].Fuerzas.Compresion;
                                                                                           //RevisionAplastamiento
                        elementoTemp.RevisionAplastamiento = new RevisionAplastamiento();
                        elementoTemp.RevisionAplastamiento._diametroTornillo = elementoTemp.Tornillo.Mm;//datosEntrada.ElementoTornillerias[i].Tornillo.Mm;
                        elementoTemp.RevisionAplastamiento._tipoConexion = conexionTornillo;//datosEntrada.ElementoTornillerias[i].Tornillo.TipoConexion;
                        elementoTemp.RevisionAplastamiento._espesorPerfil = espesorPerfil;//datosEntrada.ElementoTornillerias[i].Perfil.Espesor;
                        elementoTemp.RevisionAplastamiento._fu = Fu;//datosEntrada.Fu;
                        elementoTemp.RevisionAplastamiento._compresion = compresion;//datosEntrada.ElementoTornillerias[i].Fuerzas.Compresion;
                        elementoTemp.RevisionAplastamiento._tension = tension;//datosEntrada.ElementoTornillerias[i].Fuerzas.Tension;
                                                                              //RevisionResistenciaDesgarre
                        elementoTemp.RevisionResistenciaDesgarre = new RevisionResistenciaDesgarre();
                        elementoTemp.RevisionResistenciaDesgarre._diametroTornillo = elementoTemp.Tornillo.Mm;//datosEntrada.ElementoTornillerias[i].Tornillo.Mm;
                        elementoTemp.RevisionResistenciaDesgarre.Dmin = elementoTemp.Tornillo.Dmin;
                        elementoTemp.RevisionResistenciaDesgarre._espesorPerfil = espesorPerfil;//datosEntrada.ElementoTornillerias[i].Perfil.Espesor;
                        elementoTemp.RevisionResistenciaDesgarre._fu = Fu;//datosEntrada.Fu;
                        elementoTemp.RevisionResistenciaDesgarre._tipoConexion = conexionTornillo;//datosEntrada.ElementoTornillerias[i].Tornillo.TipoConexion;
                        elementoTemp.RevisionResistenciaDesgarre._tension = tension;//datosEntrada.ElementoTornillerias[i].Fuerzas.Tension;
                                                                                    //RevisionResistenciaBloqueCortante
                        elementoTemp.RevisionResistenciaBloqueCortante = new RevisionResistenciaBloqueCortante();
                        elementoTemp.RevisionResistenciaBloqueCortante._lc1 = elementoTemp.RevisionResistenciaDesgarre.Lc1;
                        elementoTemp.RevisionResistenciaBloqueCortante._lc2 = elementoTemp.RevisionResistenciaDesgarre.Lc2;
                        elementoTemp.RevisionResistenciaBloqueCortante._espesorPerfil = espesorPerfil;//datosEntrada.ElementoTornillerias[i].Perfil.Espesor;
                        elementoTemp.RevisionResistenciaBloqueCortante._dimPerfil = dimensionPerfil;//datosEntrada.ElementoTornillerias[i].Perfil.Dimension;
                        elementoTemp.RevisionResistenciaBloqueCortante._gramilPerfil = elementoTemp.Perfil.Gramil;
                        elementoTemp.RevisionResistenciaBloqueCortante._tipoConexion = conexionTornillo;//datosEntrada.ElementoTornillerias[i].Tornillo.TipoConexion;
                        elementoTemp.RevisionResistenciaBloqueCortante._tension = tension;//datosEntrada.ElementoTornillerias[i].Fuerzas.Tension;
                        elementoTemp.RevisionResistenciaBloqueCortante._compresion = compresion;//datosEntrada.ElementoTornillerias[i].Fuerzas.Compresion;
                        elementoTemp.RevisionResistenciaBloqueCortante._daRevisionResistenciaDesgarre = elementoTemp.RevisionResistenciaDesgarre.Da;
                        elementoTemp.RevisionResistenciaBloqueCortante._dminRevisionResistenciaDesgarre = elementoTemp.RevisionResistenciaDesgarre.Dmin;
                        elementoTemp.RevisionResistenciaBloqueCortante._sRevisionResistenciaDesgarre = elementoTemp.RevisionResistenciaDesgarre.S;
                        elementoTemp.RevisionResistenciaBloqueCortante._ubs = Ubs;
                        elementoTemp.RevisionResistenciaBloqueCortante._fu = Fu;
                        elementoTemp.RevisionResistenciaBloqueCortante._fy = Fy;

                        //Agregar el elementoTemp a la lista
                        lista.Add(elementoTemp);
                    }
                    catch(OverflowException)
                    {
                        Console.WriteLine("Error");
                    }
                }
            }
            ElementosTornilleria elementosTornilleria = new ElementosTornilleria();
            elementosTornilleria.ElementoTornillerias = lista.ToArray();
            elementosTornilleria.Fu = Fu;
            elementosTornilleria.Fy = Fy;
            elementosTornilleria.Ubs = Ubs;
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(elementosTornilleria, options);
            int lent = jsonString.Length;
            //TempData["elementosTornilleria"] = jsonString;
            //return RedirectToAction("PlacaBase");
            return Ok(jsonString);
            /*var file = "Columna1.xlsx";
            filename = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\plantillas"}" + "\\" + file;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var libros = new XLWorkbook(filename))
            {

                //Obtener la hoja
                var hoja = libros.Worksheet(1);
                //Realizar los cambios
                int cont = 7;
                foreach (ElementoTornilleria elementoTornilleria in lista)
                {
                    //Nombre grupo
                    hoja.Cell("AA" + cont).SetValue(elementoTornilleria.Grupo.Nombre);
                    //Ubicacion
                    hoja.Cell("AB" + cont).SetValue(elementoTornilleria.Grupo.Ubicacion);
                    //Perfil nombre
                    hoja.Cell("AC" + cont).SetValue(elementoTornilleria.Perfil.NombrePerfil);
                    //Peso lineal
                    hoja.Cell("AD" + cont).SetValue(elementoTornilleria.Perfil.Peso);
                    //Gramil
                    hoja.Cell("AE" + cont).SetValue(elementoTornilleria.Perfil.Gramil);
                    //NO. tornillos
                    hoja.Cell("AF" + cont).SetValue(elementoTornilleria.NTot);
                    //Diametro
                    hoja.Cell("AG" + cont).SetValue(elementoTornilleria.Tornillo.Dmin);

                    cont++;
                }

                using (var memori = new MemoryStream())
                {
                    libros.SaveAs(memori);
                    var nombreExcel = string.Concat("prueba", ".xlsx");

                    return File(memori.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }*/
        }
        public IActionResult PlacaBase(IFormCollection formCollection)
        {
            ViewData["grupo"] = formCollection["grupo"];
            ViewData["perfil"] = formCollection["perfil"];
            ViewData["nombrePiezaMarco"] = formCollection["nombrePiezaMarco"];
            ViewData["_nombrePiezaMarco"] = formCollection["nombrePiezaMarco"];
            //ElementosTornilleria t = JsonConvert.DeserializeObject<ElementosTornilleria>((string)TempData["elementosTornilleria"]);
            //string t = TempData["elementosTornilleria"].ToString();

            ViewData["datosJSON"] = formCollection["datosJSON"];
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
        public IActionResult ExportarResultados(IFormCollection formCollection)
        {
            //Obtener los datos del JSON en String
            string datosString = formCollection["datosTornilleria"];
            ElementosTornilleria elementosTornilleria = System.Text.Json.JsonSerializer.Deserialize<ElementosTornilleria>(datosString);
            datosString = formCollection["datosTension"];
            Models.PlacaBase.Tension.PlacaBase placaBaseTension =
                System.Text.Json.JsonSerializer.Deserialize<Models.PlacaBase.Tension.PlacaBase>(datosString);
            datosString = formCollection["datosCompresion"];
            Models.PlacaBase.Compresion.PlacaBase placaBaseCompresion =
                System.Text.Json.JsonSerializer.Deserialize<Models.PlacaBase.Compresion.PlacaBase>(datosString);

            var numAnclas = placaBaseCompresion.Ancla.CantidadDeAnclas;
            var diaAnclas = placaBaseCompresion.Ancla.DiametroAncla;
            var anchoPlaca = placaBaseCompresion.AnchoPlaca;
            var espesorPlaca = placaBaseTension.EspesorPlacaDefinitivo;
            var distAlBordRecortMin = placaBaseCompresion.Ancla.DistanciaBordeRecortadoMin;
            var distAlBordRecortMax = placaBaseCompresion.Ancla.DistanciaBordeRecortadoMax;

            //Elegir que archivo leer en base a _nombrePiezaMarco
            string _nombrePiezaMarco = formCollection["_nombrePiezaMarco"];
            var file = "";
            switch (_nombrePiezaMarco)
            {
                case "Columna 1": file = "Columna1.xlsx"; break;
                case "Columna 2": file = "Columna2.xlsx"; break;
                case "Columna 3": file = "Columna3.xlsx"; break;
                case "Columna 4": file = "Columna4.xlsx"; break;
                case "Trabe 1": break;
                case "Trabe 2": break;
                case "Trabe 3": break;
            }
            //Leer el archivo
            string filename = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\plantillas"}" + "\\" + file;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var libros = new XLWorkbook(filename))
            {
                //Obtener la hoja
                var hoja = libros.Worksheet(1);
                //Realizar los cambios
                int cont = 7;
                foreach (ElementoTornilleria elementoTornilleria in elementosTornilleria.ElementoTornillerias)
                {
                    //Nombre grupo
                    hoja.Cell("AA" + cont).SetValue(elementoTornilleria.Grupo.Nombre);
                    //Ubicacion
                    hoja.Cell("AB" + cont).SetValue(elementoTornilleria.Grupo.Ubicacion);
                    //Perfil nombre
                    hoja.Cell("AC" + cont).SetValue(elementoTornilleria.Perfil.NombrePerfil);
                    //Peso lineal
                    hoja.Cell("AD" + cont).SetValue(elementoTornilleria.Perfil.Peso);
                    //Gramil
                    hoja.Cell("AE" + cont).SetValue(elementoTornilleria.Perfil.Gramil);
                    //NO. tornillos
                    hoja.Cell("AF" + cont).SetValue(elementoTornilleria.NTot);
                    //Diametro
                    hoja.Cell("AG" + cont).SetValue(elementoTornilleria.Tornillo.Dmin);

                    cont++;
                }
                hoja.Cell("AA3").SetValue(elementosTornilleria.ElementoTornillerias[0].Grupo.Nombre);
                hoja.Cell("AB3").SetValue(elementosTornilleria.ElementoTornillerias[0].Grupo.Ubicacion);
                hoja.Cell("AC3").SetValue(anchoPlaca);
                hoja.Cell("AD3").SetValue(espesorPlaca);
                hoja.Cell("AE3").SetValue(numAnclas);
                hoja.Cell("AF3").SetValue(diaAnclas);
                hoja.Cell("AG3").SetValue(distAlBordRecortMin);
                hoja.Cell("AH3").SetValue(distAlBordRecortMax);

                using (var memori = new MemoryStream())
                {
                    libros.SaveAs(memori);
                    var nombreExcel = string.Concat(_nombrePiezaMarco, ".xlsx");

                    return File(memori.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult PlacaColumna(IFormCollection formCollection)
        {
            //Para obtener ruta relativa
            List<Models.Ancla> listaAnclas = LeerCSV.LeerListaAncla(_hostingEnvironment.WebRootPath);
            ViewData["listaAnclas"] = listaAnclas;
            //Obtener la lista de Perfiles con sus lados y centroides
            //Para obtener ruta relativa
            List<PerfilDelAISC> listaPerfiles = LeerCSV.LeerCatalogo(_hostingEnvironment.WebRootPath);
            ViewData["listaPerfiles"] = listaPerfiles;
            return View();
        }
        public IActionResult Exportar(List<ElementoTornilleria> lista)
        {
            var file = "Columna1.xlsx";
            var filename = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\plantillas"}" + "\\" + file;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var libros = new XLWorkbook(filename))
            {

                //Obtener la hoja
                var hoja = libros.Worksheet(1);
                //Realizar los cambios
                int cont = 7;
                foreach (ElementoTornilleria elementoTornilleria in lista)
                {
                    //Nombre grupo
                    hoja.Cell("AA" + cont).SetValue("Nombre grupo");
                    //Ubicacion
                    hoja.Cell("AB" + cont).SetValue("Ubicación");
                    //Perfil nombre
                    hoja.Cell("AC" + cont).SetValue(elementoTornilleria.Perfil.NombrePerfil);
                    //Peso lineal
                    hoja.Cell("AD" + cont).SetValue("Peso lineal");
                    //Gramil
                    hoja.Cell("AE" + cont).SetValue(elementoTornilleria.Perfil.Gramil);
                    //NO. tornillos
                    hoja.Cell("AF" + cont).SetValue(elementoTornilleria.NTot);
                    //Diametro
                    hoja.Cell("AG" + cont).SetValue("Diametro");
                    cont++;
                }
                //librohoja.Cell("B3").SetValue(Mensaje);
                
                
                //librohoja.ColumnsUsed().AdjustToContents();
                

                using (var memori = new MemoryStream())
                {
                    libros.SaveAs(memori);
                    var nombreExcel = string.Concat("prueba", ".xlsx");

                    return File(memori.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }

        }
        public IActionResult llenarPlantilla(List<ElementoTornilleria> lista)
        {
            SLDocument sl = new SLDocument(
                System.IO.Path.Combine(_hostingEnvironment.WebRootPath + "/files/plantillas/",
                "Columna1.xlsx"));

            var dateAndTime = DateTime.Now;
            string mensaje = "Informe del " + dateAndTime.ToString("dd/MM/yyyy") + " de la Norma Oficial Mexicana NOM-010-ENER. Eficiencia energética del conjunto motor-bomba sumergible tipo pozo profundo.";

            //sl.SetCellValue("A2", mensaje);
            int cont = 7;
            foreach (ElementoTornilleria elementoTornilleria in lista)
            {
                //Nombre grupo
                sl.SetCellValue("AA" + cont, "Nombre grupo");
                //Ubicacion
                sl.SetCellValue("AB" + cont, "Ubicación");
                //Perfil nombre
                sl.SetCellValue("AC" + cont, elementoTornilleria.Perfil.NombrePerfil);
                //Peso lineal
                sl.SetCellValue("AD" + cont, "Peso lineal");
                //Gramil
                sl.SetCellValue("AE" + cont, elementoTornilleria.Perfil.Gramil);
                //NO. tornillos
                sl.SetCellValue("AF" + cont, elementoTornilleria.NTot);
                //Diametro
                sl.SetCellValue("AG" + cont, "Diametro");
                cont++;
            }



            using (var memori = new MemoryStream())
            {
                sl.SaveAs(memori);
                var nombreExcel = string.Concat("prueba", ".xlsx");

                return File(memori.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
            }
        }


    }
}
    
