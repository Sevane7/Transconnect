using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Poids_Lourd : Vehicule
    {
        public int volume;
        public string matiere;

        public Poids_Lourd(int volume, string matiere, double consommation, string immat)
            : base(consommation, immat)
        {
            this.volume = volume;
            this.matiere = matiere;
        }
    }
}
