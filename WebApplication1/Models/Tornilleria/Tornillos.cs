using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tornilleria
{
    public class Tornillos
    {
        public Dictionary<string, Models.Tornilleria.Tornillo> Dictionary { get; set; }
        public Tornillos()
        {
            Dictionary = new Dictionary<string, Models.Tornilleria.Tornillo>();
            Dictionary.Add("1/2",   new Models.Tornilleria.Tornillo("1 / 2", 12.7, 19.05));
            Dictionary.Add("5/8",   new Models.Tornilleria.Tornillo("5 / 8", 15.875, 22.225));
            Dictionary.Add("3/4",   new Models.Tornilleria.Tornillo("3 / 4", 19.05, 25.4));
            Dictionary.Add("7/8",   new Models.Tornilleria.Tornillo("7 / 8", 22.225, 28.575));
            Dictionary.Add("1",     new Models.Tornilleria.Tornillo("1", 25.4, 31.75));
            Dictionary.Add("1 1/8", new Models.Tornilleria.Tornillo("1 1 / 8", 28.575, 38.1));
            Dictionary.Add("1 1/4", new Models.Tornilleria.Tornillo("1 1 / 4", 31.75, 41.275));
            Dictionary.Add("1 3/8", new Models.Tornilleria.Tornillo("1 3 / 8", 34.925, 43.65625));
            Dictionary.Add("1 1/2", new Models.Tornilleria.Tornillo("1 1 / 2", 38.1, 47.625));

        }
        public Models.Tornilleria.Tornillo GetTornillo(string plg)
        {
            return Dictionary[plg];
        }
    }
}
