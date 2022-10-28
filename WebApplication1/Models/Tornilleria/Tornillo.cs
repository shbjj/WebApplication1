using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tornilleria
{
    public class Tornillo : IEquatable<Tornillo>
    {
        
        public Tornillo(string plg, double mm, double dmin)
        {
            Plg = plg;
            Mm = mm;
            Dmin = dmin;
        }
        public Tornillo(double mm)
        {
            Mm = mm;
        }
        public Tornillo()
        {

        }
        public string Plg { set; get; }
        public double Mm { set; get; }
        public string Grupo { get; set; }
        public string Rosca { get; set; }
        public string TipoConexion { get; set; }
        public double Dmin { set; get; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Tornillo objAsPart = obj as Tornillo;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(Mm);
        }
        public bool Equals(Tornillo other)
        {
            if (other == null) return false;
            return (this.Mm.Equals(other.Mm));
        }

    }

}
