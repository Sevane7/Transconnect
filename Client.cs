using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Client : Personne
    {
        public double montant_cumule;
        public Client(string nom, string prenom, DateTime naissance, string adresse, string mail, string telephone, double montant_cumule = 0)
            : base(nom, prenom, naissance, adresse, mail, telephone)
        {
            this.montant_cumule = montant_cumule;
            //EnvoyerEmailAnniversaire();
        }
        public Client(string nom)
            :base(nom)
        {
            try
            {
                foreach (string line in File.ReadLines("Clients.csv").Skip(1))
                {
                    string[] lines = line.Split(",");
                    if (nom == lines[0])
                    {
                        this.montant_cumule = Convert.ToDouble(lines[7]);
                        break;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public double Montant_cumule { get { return montant_cumule; } set { montant_cumule = value; } }
        public void add_montant(double new_montant)
        {
            this.montant_cumule += new_montant;
        }
        public override string ToString()
        {
            return base.ToString() + $", {this.montant_cumule}";
        }
        public override void To_csv()
        {
            base.To_csv();
            string filename = GetFileName();
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    writer.WriteLine($",{Montant_cumule}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur est survenue lors de l'enregistrement des informations dans le fichier CSV.");
                Console.WriteLine(e.Message);
            }
        }
        protected override string GetFileName()
        {
            return "Clients.csv";
        }
        public void EnvoyerEmailAnniversaire()
        {
            DateTime today = DateTime.Today;
            if (this.Naissance.Month == today.Month && this.Naissance.Day == today.Day)
            {
                string fromAddress = "anniversaire@gmail.com"; 
                string subject = "Joyeux Anniversaire !";
                string body = $"Bonjour {this.Prenom},\n\nNous vous souhaitons un très joyeux anniversaire ! " +
                              $"Pour célébrer, nous vous offrons un bon de réduction de 20 euros sur votre prochaine commande.\n\n" +
                              $"Cordialement,\nL'équipe";
                Console.WriteLine($"To : {this.Mail}" +
                    $"\nObject : {subject}" +
                    $"\nContent : \n{body}");
            }
        }
    }
}
