using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Camion_citerne : Poids_Lourd
    {
        public string[] materiaux_ok = ["liquide", "gaz"];
        public string cuve;
        public Camion_citerne(int volume, string matiere, double consommation, string immat, string cuve)
            : base(volume, matiere, consommation, immat)
        {
            this.cuve = cuve;
        }
    }
}
