using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class GrupoPerfil
    {
        public int IdGrupo { get; set; }
        public int IdPerfil { get; set; }
        public String NombreGrupo { get; set; }
        public String NombrePerfil { get; set; }
        public Double DistanciaPerfil { get; set; }
        public Double DimensionPerfil { get; set; }
        public Double EspesorPerfil { get; set; }
        public String Ubicacion { get; set; }
    }
}
