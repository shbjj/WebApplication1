
using Newtonsoft.Json;

namespace WebApplication1.Models.Tornilleria
{
    public class ElementosTornilleria
    {
        public double Fy { get; set; }
        public double Fu { get; set; }
        public double Ubs { get; set; }
        public ElementoTornilleria[] ElementoTornillerias { get; set; }
    }
}
