using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using WebApplication1.Models;

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
    }
}

