using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Voiture : Vehicule
    {
        public int nb_places;
        public Voiture(int nb_places, double consommation, string immat)
            : base(consommation, immat)
        {
            this.nb_places = nb_places;
        }
    }
}
