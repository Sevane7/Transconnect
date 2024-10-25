using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Camion_benne : Poids_Lourd
    {
        public int nb_bennes;
        public bool grue;
        public string[] materiaux_ok = ["sable", "terre", "gravier"];
        public Camion_benne(int volume, string matiere, double consommation, string immat, int nb_bennes, bool grue)
            : base(volume, matiere, consommation, immat)
        {
            this.nb_bennes = nb_bennes;
            this.grue = grue;
        }
    }
}
