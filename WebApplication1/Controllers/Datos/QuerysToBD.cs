using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using WebApplication1.Models;
using WebApplication1.Models.Piezas;

namespace WebApplication1.Controllers
{
    public class QuerysToBD
    {
        public List<Object> consultaBD_SEs()
        {
            MySqlDataReader reader;
            List<Object> lista = new List<Object>();
            string sql;
            String StringSEs;

            sql = "SELECT idSubestacion, nombre FROM subestacion";

            MySqlConnection connectionDB = Conexion.conexion();
            connectionDB.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, connectionDB);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StringSEs = reader.GetString(0) + "   " + reader.GetString(1);
                        lista.Add(StringSEs);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connectionDB.Close();
            }
            return lista;
        }

        public List<SubEstacion> consultaBD_SEs2()
        {
            MySqlDataReader reader;
            List<SubEstacion> lista = new List<SubEstacion>();
            string sql;
            SubEstacion subEstacion = null;

            sql = "SELECT idSubestacion, nombre FROM subestacion";

            MySqlConnection connectionDB = Conexion.conexion();
            connectionDB.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, connectionDB);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subEstacion = new SubEstacion(reader.GetString(0));
                        subEstacion.Nombre = reader.GetString(1);
                        lista.Add(subEstacion);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connectionDB.Close();
            }
            return lista;
        }

        public List<Object> ConsultaMarco(string dato)
        {
            List<Object> listaMco = new List<Object>();
            string sql;
            MySqlDataReader reader = null;

            // Muestra el marco y nombre del marco para la SEs
            sql = "SELECT idMarco, nombre FROM marco WHERE idSubestacion=" + dato;
            MySqlConnection connectionDB = Conexion.conexion();
            connectionDB.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, connectionDB);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listaMco.Add(reader.GetString(0));
                        listaMco.Add(reader.GetString(1));
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connectionDB.Close();
            }
            return listaMco;
        }
        public static Marco GetMarcoDetails(int id)
        {
            Marco temp = new Marco();
            string sql;
            MySqlDataReader reader = null;

            sql = "SELECT * FROM marco WHERE idMarco=" + id;
            MySqlConnection connectionDB = Conexion.conexion();
            connectionDB.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, connectionDB);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        temp.IdMarco = reader.GetInt32(0);
                        temp.IdSubestacion = reader.GetInt32(1);
                        temp.Nombre = reader.GetString(2);
                        temp.BaseX = reader.GetFloat(3);
                        temp.BaseZ = reader.GetFloat(4);
                        temp.Separacion = reader.GetInt32(5);
                        temp.AlturaGuarda = reader.GetFloat(6);
                        temp.AlturaSuperior = reader.GetFloat(7);
                        temp.AlturaSuspension = reader.GetFloat(8);
                        temp.AlturaRemate = reader.GetFloat(9);
                        temp.Tension = reader.GetInt32(10);
                        temp.FrecuenciaX = reader.GetFloat(11);
                        temp.FrecuenciaZ = reader.GetFloat(12);
                        temp.BaseZColumnas = reader.GetFloat(13);
                        temp.BaseXColumnas = reader.GetFloat(14);
                        temp.BaseSoporteColumnas = reader.GetFloat(15);
                        temp.Distanciab = reader.GetFloat(16);
                        temp.IdResponsable = reader.GetInt32(17);
                        temp.Fecha = reader.GetDateTime(18);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connectionDB.Close();
            }
            return temp;
        }
        public static List<Pieza> GetPiezasOfMarco(int id)
        {
            //Lista de piezas que se retornara
            List<Pieza> piezas = new List<Pieza>();
            
            MySqlDataReader reader = null;
            //Query de conexion a la BD, obtener todas las piezas que pertenezcan al marco del ID
            string sql = "SELECT * FROM pieza WHERE idMarco = " + id;
            //Obtener la conexion y abrirla
            MySqlConnection connectionDB = Conexion.conexion();
            connectionDB.Open();
            try
            {
                //Ejecutar consulta
                MySqlCommand comando = new MySqlCommand(sql, connectionDB);
                reader = comando.ExecuteReader();
                //Si hay resultados...
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Guardar los datos de la pieza en un objeto temporal
                        Pieza temp = new Pieza();
                        temp.IdPieza = reader.GetInt32(0);
                        temp.IdMarco = reader.GetInt32(1);
                        temp.Nombre = reader.GetString(2);
                        temp.Tipo = reader.GetString(3);
                        temp.Eje = reader.GetString(4);
                        temp.TipoLlegadas = reader.GetString(5);
                        temp.Numero = reader.GetInt32(6);
                        temp.TipoElemento = reader.GetInt32(7);
                        piezas.Add(temp);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {//Finalmente, cerrar conexion
                connectionDB.Close();
            }
            //Retornar lista de piezas
            return piezas;
        }
        public List<Object> ConsultaPiezas(string datoMco)
        {
            string StringPiezas;
            string sql2;
            List<Object> listaPiezas = new List<Object>();

            // Muestra la lista de piezas para el marco
            MySqlDataReader reader = null;
            MySqlConnection connectionDB = Conexion.conexion();
            sql2 = "SELECT idPieza, eje, nombre, tipoElemento, tipo FROM pieza WHERE idMarco =" + datoMco;
            connectionDB.Open();
            try
            {
                MySqlCommand comando2 = new MySqlCommand(sql2, connectionDB);
                reader = comando2.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StringPiezas = reader.GetString("idPieza") + "       ";
                        StringPiezas = StringPiezas + reader.GetString("eje") + "       ";
                        StringPiezas = StringPiezas + reader.GetString("nombre") + "       ";
                        StringPiezas = StringPiezas + reader.GetString("tipoElemento") + "       ";
                        StringPiezas = StringPiezas + reader.GetString("tipo");
                        listaPiezas.Add(StringPiezas);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connectionDB.Close();
            }
            return listaPiezas;
        }
        public List<GrupoPerfil> ConsultaGrupoPerfil(string idPieza, string WebRootPath)
        {
            string sql= "select g.idGrupo as IdGrupo, g.idPerfil as IdPerfil, g.nombre as NombreGrupo, " +
                "p.nombre as NombrePerfil, p.dimension as Dimension, p.espesor as Espesor " +
                "from grupo g, perfilcelosia p " +
                "where g.idPieza = "+idPieza+" and(p.idPerfilcelosia = g.idPerfil); ";
            List<GrupoPerfil> gruposPerfil = new List<GrupoPerfil>();
            Dictionary<string, string> ubicaciones = LeerCSV.LeerUbicacion(WebRootPath);

            // Muestra la lista de piezas para el marco
            MySqlDataReader reader = null;
            MySqlConnection connectionDB = Conexion.conexion();
            connectionDB.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, connectionDB);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GrupoPerfil temp = new GrupoPerfil();
                        temp.IdGrupo = reader.GetInt32("IdGrupo");
                        temp.IdPerfil = reader.GetInt32("IdPerfil");
                        temp.NombreGrupo = reader.GetString("NombreGrupo");
                        temp.NombrePerfil = reader.GetString("NombrePerfil");
                        temp.DimensionPerfil = reader.GetDouble("Dimension");
                        temp.EspesorPerfil = reader.GetDouble("Espesor");
                        string nombGrupo = reader.GetString("NombreGrupo");
                        temp.Ubicacion = ubicaciones[nombGrupo.Substring(2,nombGrupo.Length-2)];

                        gruposPerfil.Add(temp);
                    }
                }
            }
            catch (MySqlException ex)
            {
                return null;
            }
            finally
            {
                connectionDB.Close();
            }
            return gruposPerfil;
        }

        public List<Object> ConsultaGrupo(string dato)
        {
            string sql1, sql2;
            int contador = 0;
            int jPerfil;
            string[,] ArrayPerfiles = new string[71, 7];
            //Lee la lista de perfiles
            List<Object> listaPerfiles = new List<Object>();
            MySqlDataReader reader = null;
            MySqlConnection connectionDB = Conexion.conexion();

            sql1 = "SELECT idPerfilcelosia, nombre, dimension, espesor, rxy, rz FROM perfilcelosia;";

            connectionDB.Open();
            try
            {
                MySqlCommand comando2 = new MySqlCommand(sql1, connectionDB);
                reader = comando2.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        contador++;
                        ArrayPerfiles[contador, 1] = reader.GetString("idPerfilcelosia");
                        ArrayPerfiles[contador, 2] = reader.GetString("nombre");
                        ArrayPerfiles[contador, 3] = reader.GetString("dimension");
                        ArrayPerfiles[contador, 4] = reader.GetString("espesor");
                        ArrayPerfiles[contador, 5] = reader.GetString("rxy");
                        ArrayPerfiles[contador, 6] = reader.GetString("rz");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connectionDB.Close();
            }

            // Muestra la lista de grupos para la pieza
            List<Object> listaGrupos = new List<Object>();
            //MySqlDataReader reader = null;
            //MySqlConnection connectionDB = Conexion.conexion();
            sql2 = "SELECT idPieza, idGrupo, nombre, idPerfil FROM grupo WHERE idPieza =" + dato;

            connectionDB.Open();
            try
            {
                MySqlCommand comando2 = new MySqlCommand(sql2, connectionDB);
                reader = comando2.ExecuteReader();
                contador = 0;
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        contador++;
                        Models.Grupos _grupos = new Grupos();
                        _grupos.NumeracionGpo1 = contador;
                        _grupos.IdPieza = reader.GetString("idPieza");
                        _grupos.IdGrupo = reader.GetString("idGrupo");
                        _grupos.NombreGpo1 = reader.GetString("nombre");
                        _grupos.UbicacionGpo1 = "XXX ";
                        _grupos.IdPerfil = reader.GetString("idPerfil");
                        jPerfil = int.Parse(reader.GetString("idPerfil"));
                        _grupos.NombrePerfil1 = ArrayPerfiles[jPerfil, 2];
                        _grupos.EspesorPF1 = double.Parse(ArrayPerfiles[jPerfil, 4]);
                        _grupos.DimensionPF1 = double.Parse(ArrayPerfiles[jPerfil, 3]);
                        // ArrayPerfiles[contador, 5]
                        // ArrayPerfiles[contador, 6]
                        listaGrupos.Add(_grupos);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connectionDB.Close();
            }
            return listaGrupos;
        }
        public static double ConsultaEspesorPerfil(int IdGrupo)
        {
            double espesor = 0;
            MySqlDataReader reader = null;

            MySqlConnection connectionDB = Conexion.conexion();
            connectionDB.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand("SELECT espesor FROM perfilcelosia WHERE idPerfilcelosia=" + IdGrupo,
                    connectionDB);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        espesor = reader.GetFloat(0);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connectionDB.Close();
            }
            return espesor;
        }
    }
}

