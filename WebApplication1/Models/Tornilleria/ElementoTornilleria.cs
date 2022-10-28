using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tornilleria
{
    public class ElementoTornilleria
    {
        public bool DatosCompletos { get; set; }
        public int Numero { get; set; }
        public Grupo Grupo { get; set; }
        public Perfil Perfil { get; set; }
        public Tornillo Tornillo { get; set; }
        public Fuerzas Fuerzas { get; set; }
        public RevisionResistenciaCortante RevisionResistenciaCortante { get; set; }
        public RevisionAplastamiento RevisionAplastamiento { get; set; }
        public RevisionResistenciaDesgarre RevisionResistenciaDesgarre { get; set; }
        public RevisionResistenciaBloqueCortante RevisionResistenciaBloqueCortante { get; set; }
        public int NTot
        {
            get
            {
                //Si los valores de los sig objetos no son nulos...
                if (RevisionResistenciaCortante != null && RevisionAplastamiento != null && RevisionResistenciaDesgarre != null && RevisionResistenciaBloqueCortante != null)
                {
                    return Math.Max(RevisionResistenciaBloqueCortante.Nd, Math.Max(RevisionResistenciaDesgarre.Nd, Math.Max(RevisionResistenciaCortante.Nv, RevisionAplastamiento.Na)));
                }
                else//Si son nulos...   
                {
                    return 0;
                }
            }
        }
        public double Anv
        {
            get
            {
                int n = Math.Max(RevisionResistenciaCortante.Nv, Math.Max(RevisionAplastamiento.Na, RevisionResistenciaDesgarre.Nd));
                return (RevisionResistenciaDesgarre.Lc1 + (n - 1) * RevisionResistenciaDesgarre.Lc2) * Perfil.Espesor;
            }
        }
        public double Ant
        {
            get
            {
                return (Perfil.Dimension - Perfil.Gramil - (RevisionResistenciaDesgarre.Da / 2)) * Perfil.Espesor;
            }
        }
        public double Agv
        {
            get
            {
                int n = Math.Max(RevisionResistenciaCortante.Nv, Math.Max(RevisionAplastamiento.Na, RevisionResistenciaDesgarre.Nd));
                return (RevisionResistenciaDesgarre.Dmin + (n - 1) * RevisionResistenciaDesgarre.S) * Perfil.Espesor;
            }
        }
    }
}
