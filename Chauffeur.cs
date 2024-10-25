using System;
using TransConnect;

namespace TransConnect
{
    public class Chauffeur : Salarie
    {
        public DateTime derniere_course;
        public bool dispo;
        public int anciennete;
        public Chauffeur(string nom, string prenom, DateTime naissance, string adresse, string mail, string telephone, int num_ss, string poste, string salaire, DateTime derniere_course, bool dispo) :
            base(nom, prenom, naissance, adresse, mail, telephone, num_ss, poste, salaire)
        {
            this.derniere_course = derniere_course;
            this.dispo = dispo;

            Random random = new Random();
            this.anciennete = random.Next(0, 5);
            //Promotion();
        }
        public DateTime Derniere_course { get { return this.derniere_course; } set { this.derniere_course = value; } }
        public bool Dispo { get { return this.dispo; } set { this.dispo = value; } }
        public int Anciennete { get { return this.anciennete; } }
        public override string ToString()
        {
            if (this == null) return "Le chauffeur est null";
            return base.ToString() + $", {this.derniere_course.ToString("dd/MM/yyyy")}, {this.dispo.ToString()}";
        }
        public override void To_csv()
        {
            base.To_csv();
            string filename = GetFileName();
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    writer.WriteLine($",{this.derniere_course.ToString("dd/MM/yyyy")},{this.dispo.ToString()}");
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
            return "Chauffeurs.csv";
        }
        public void Promotion()
        {
            if(this.anciennete > 0)
            {
                double promo = Anciennete / 10;
                double old_salary = Convert.ToDouble(this.Salaire);
                double new_salaire = old_salary + promo * old_salary;
                Console.WriteLine($"M. {this.Nom}, vous êtes parmis nous depuis {this.Anciennete}. " +
                    $"Nous vous attribut une promotion de {Anciennete / 10} %." +
                    $"\nVotre salaire passe à {new_salaire}");
            } 
        }
    }
}