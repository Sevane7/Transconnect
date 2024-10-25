using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Camionnette : Vehicule
    {
        public string usage;
        public Camionnette(string usage, double consommation, string immat)
            : base(consommation, immat)
        {
            this.usage = usage;
        }
    }
}
