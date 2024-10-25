using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Camion_frigo : Poids_Lourd
    {
        public int nb_electrogene;
        public Camion_frigo(int volume, string matiere, double consommation, string immat, int nb_electrogene)
            : base(volume, matiere, consommation, immat)
        {
            this.nb_electrogene = nb_electrogene;
        }
    }
}
