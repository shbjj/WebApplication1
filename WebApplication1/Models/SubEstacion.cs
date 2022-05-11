using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class SubEstacion
    {
        private string id;
        private string nombre;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Id { get => id; set =>  id= value; }

        public SubEstacion(string _id)
        {
            id=_id;
        }
    }
}
