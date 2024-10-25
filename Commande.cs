using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Commande
    {
        public int num_commande;
        public Livraison livraison;
        public Client client;
        public double prix_commande;
        public DateTime date_commande;
        public Commande(int commande, Livraison livraison, Client client, double prix_commande, bool random_date = true)
        {
            this.num_commande = commande;
            this.livraison = livraison;
            this.client = client;
            this.prix_commande = prix_commande;
            if(random_date) this.date_commande = Program.GetRandomDate(new DateTime(2023, 1, 1));
            else
            {
                Console.WriteLine("Jour:");
                int jour1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Mois:");
                int mois1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Annee:");
                int annee1 = Convert.ToInt32(Console.ReadLine());
                this.date_commande = new DateTime(jour1, mois1, annee1);

            }

            this.client.add_montant(prix_commande);
        }
        public int Num_commande { get { return this.num_commande; } }
        public Livraison Livraison { get { return this.livraison; } }
        public Client Client { get { return this.client; } }
        public DateTime DateCommanande { get { return this.date_commande; } }
        public double Prix_commande { get { return this.prix_commande; } }
        public override string ToString()
        {
            return $"{this.num_commande},{this.livraison.ChauffeurCommande.Num_ss},{this.date_commande.ToString("dd/MM/yyyy")},{this.livraison.Depart},{this.livraison.Arrivee},{this.prix_commande},{this.client.Nom}";
        }
        public void To_csv()
        {
            try
            {
                // Créer ou ouvrir le fichier CSV en mode écriture
                using (StreamWriter writer = new StreamWriter("Commandes.csv", true))
                {
                    // Écrire les informations de la personne dans le fichier CSV
                    writer.WriteLine(this.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur est survenue lors de l'enregistrement des informations dans le fichier CSV.");
                Console.WriteLine(e.Message);
            }
        }

    }
}
