using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Vehicule
    {
        public double consommation;
        public string immat;
        public Vehicule(double consommation, string immat)
        {
            this.consommation = consommation;
            this.immat = immat;
        }
        public double Consommation { get { return this.consommation; } set { this.consommation = value; } }
        public string Immat {  get { return this.immat; } set { this.immat = value; } }
        public override string ToString()
        {
            return $"{this.immat}, {this.consommation}";
        }
    }
}
