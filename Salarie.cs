using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class Salarie : Personne
    {
        public int num_ss;
        public string poste;
        public string salaire;
        public List<Salarie> subordonnes;
        public Salarie(string nom, string prenom, DateTime naissance, string adresse, string mail, string telephone, int num_ss, string poste, string salaire)
            : base(nom, prenom, naissance, adresse, mail, telephone)
        {
            this.num_ss = num_ss;
            this.poste = poste;
            this.salaire = salaire;
            this.subordonnes = new List<Salarie>();
        }
        public int Num_ss { get { return num_ss; } set { num_ss = value; } }
        public string Poste { get { return poste; } set { poste = value; } }
        public string Salaire { get { return salaire; } set { salaire = value; } }
        public List<Salarie> Subordonnes { get { return subordonnes; } set { subordonnes = value; } }
        public override void To_csv()
        {
            base.To_csv();
            string filename = GetFileName();            
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    if(this is Chauffeur chauffeur) writer.Write($",{Num_ss},{Poste},{Salaire}");
                    else { writer.WriteLine($",{Num_ss},{Poste},{Salaire}"); }
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
            return "Salaries.csv";
        }
        public void AddSubordonnes(Salarie new_sub)
        {
            this.subordonnes.Add(new_sub);
        }
        public static void AfficheSubbordonnes(Salarie salarie, string prefixe = "")
        {
            Console.WriteLine(prefixe + salarie.Nom);
            foreach (Salarie sub in salarie.Subordonnes)
            {
                AfficheSubbordonnes(sub, prefixe + "-/");
            }
        }
        public static void FireSalarie(Salarie boss, Salarie salarie)
        {
            if (boss.Subordonnes.Contains(salarie))
            {
                boss.Subordonnes.Remove(salarie);
                Console.WriteLine($"Le salarie {salarie.Nom} travaillAIT pour {boss.Nom}");
                AfficheSubbordonnes(boss);
            }
            else
            {
                foreach (Salarie sub in boss.Subordonnes)
                {
                    FireSalarie(sub, salarie);
                }
            }
        }
        public static void HireSalarie(Salarie boss, Salarie salarie)
        {
            boss.AddSubordonnes(salarie);
            Console.WriteLine($"{salarie.Nom} travaille dorénavant pour {boss.Nom}.");
            AfficheSubbordonnes(boss);
        }
        public override string ToString()
        {
            return base.ToString() + $", {this.num_ss}, {this.poste}, {this.salaire}";
        }
    }
}
