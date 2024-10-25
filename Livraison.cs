using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Livraison
    {
        public Chauffeur chauffeur;
        public Vehicule vehicule;
        public List<string> chemin;
        public string depart;
        public string arrivee;
        public double prix_livraison;
        public GrapheDijkstra graphe;
        public Livraison(string depart, string arrivee)
        {
            this.chauffeur = Program.Chauffeur_dispo();
            this.vehicule = Program.Choix_vehicule();
            this.depart = depart;
            this.arrivee = arrivee;
            
            this.graphe = new GrapheDijkstra(Program.ReadFile("Distances.csv"));
            this.chemin = this.graphe.CourtChemin(this.depart, this.arrivee);

            this.prix_livraison = get_price();
        }
        public string Depart { get { return this.depart; } set { this.depart = value; } }
        public string Arrivee { get { return this.arrivee; } set { this.arrivee = value; } }
        public double Prix_livraison { get { return this.prix_livraison; } set { this.prix_livraison = value; } }
        public Chauffeur ChauffeurCommande { get { return this.chauffeur; } set { this.chauffeur = value; } }
        public Vehicule VehiculeLiv { get { return this.vehicule; } set { this.vehicule = value; } }
        public List<string> Chemin { get {  return this.chemin; } }
        public double get_price()
        {
            double price = 0;
            double distance = this.graphe.DistanceChemin(this.chemin);
            price += distance * this.vehicule.Consommation;
            price *= 1 + this.chauffeur.Anciennete / 5;
            return price;
        }
        public override string ToString()
        {
            string writechemin = "";
            for(int i = 0; i < this.chemin.Count();i++) writechemin += this.chemin[i] + " " ;
            return $"{this.chauffeur.ToString()}, {this.vehicule.ToString()}, {writechemin}, {this.prix_livraison}";
        }
    }
}
