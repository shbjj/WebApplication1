using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tornilleria
{
    public class RevisionResistenciaCortante
    {
        public string _grupoTornillo { get; set; }
        public string _roscaTornillo { get; set; }
        public string _tipoConexion { get; set; }
        public double _mmTornillo { get; set; }
        public double _tension { get; set; }
        public double _compresion { get; set; }
        public RevisionResistenciaCortante()
        {

        }

        public double Fnv
        {
            get
            {
                if (_grupoTornillo == "A307" && _roscaTornillo == "N/A")
                {
                    return 188;
                }
                else
                {
                    if (_grupoTornillo == "A" && _roscaTornillo == "No")
                    {
                        return 372;
                    }
                    else
                    {
                        if (_grupoTornillo == "A" && _roscaTornillo == "Sí")
                        {
                            return 469;
                        }
                        else
                        {
                            if (_grupoTornillo == "B" && _roscaTornillo == "No")
                            {
                                return 579;
                            }
                            else
                            {
                                if (_grupoTornillo == "B" && _roscaTornillo == "Sí")
                                {
                                    return 579;
                                }
                                else
                                {
                                    if (_grupoTornillo == "C" && _roscaTornillo == "No")
                                    {
                                        return 620;
                                    }
                                    else
                                    {
                                        if (_grupoTornillo == "C" && _roscaTornillo == "Sí")
                                        {
                                            return 779;
                                        }
                                        else
                                        {
                                            return 0;

                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        public double Ab
        {
            get
            {
                return Math.PI * (Math.Pow(_mmTornillo / 2, 2));
            }
        }
        public double Rnv
        {
            get
            {
                return Fnv * Ab / 1000;
            }
        }
        public double Ruv
        {
            get
            {
                return 0.75 * Rnv;
            }
        }
        public double RuvTotal
        {
            get
            {
                return Math.Max(_tension, _compresion);
            }
        }
        public int Nv
        {
            get
            {
                if (_tipoConexion == "Simple")
                {
                    return Convert.ToInt32(Math.Ceiling(RuvTotal / Ruv));
                }
                else
                {
                    return Convert.ToInt32(Math.Ceiling(RuvTotal / (2 * Ruv)));
                }
            }
        }
    }

}
