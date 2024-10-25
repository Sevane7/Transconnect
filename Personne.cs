using System;
using System.Security.Cryptography.X509Certificates;
using TransConnect;

public class Personne
{
    public string nom;
    public string prenom;
    public DateTime naissance;
    public string adresse;
    public string mail;
    public string telephone;
    public Personne(string nom, string prenom, DateTime naissance, string adresse, string mail, string telephone)
    {
        this.nom = nom;
        this.prenom = prenom;
        this.naissance = naissance;
        this.adresse = adresse;
        this.mail = mail;
        this.telephone = telephone;
    }
    public Personne(string nom)
    {
        try
        {
            foreach (string line in File.ReadLines("Clients.csv").Skip(1))
            {
                string[] lines = line.Split(",");
                if(nom == lines[0])
                {
                    this.nom = nom;
                    this.prenom = lines[1];
                    this.naissance = DateTime.ParseExact(lines[2], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    this.adresse = lines[3];
                    this.mail = lines[4];
                    this.telephone = lines[5];
                    break;
                }
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    #region Proprietes
    public string Nom { get { return nom; } set { nom = value; } }
    public string Prenom { get { return prenom; } }
    public DateTime Naissance { get { return naissance; } }
    public string Adresse { get { return adresse; } set { adresse = value; } }
    public string Mail { get { return mail; } set { mail = value; } }
    public string Telephone { get { return telephone; } set { telephone = value; } }
    #endregion
    public override string ToString()
    {
        return $"{this.nom}";
    }
    public virtual void To_csv()
    {
        string filename = GetFileName();

        try
        {
            // Créer ou ouvrir le fichier CSV en mode écriture
            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                string alwayswrite = $"{this.nom},{this.prenom},{this.naissance.ToString("dd/MM/yyyy")},{this.adresse},{this.mail},{this.telephone}";
                //string heritagewrite = "";

                //if (this is Client) heritagewrite += $",{(this as Client).Montant_cumule}";
                //if (this is Salarie) heritagewrite += $",{(this as Salarie).Num_ss},{(this as Salarie).Poste},{(this as Salarie).Salaire}";
                //if (this is Chauffeur) heritagewrite += $"{(this as Chauffeur).Derniere_course.ToString("dd/MM/yyy")}, {(this as Chauffeur).dispo.ToString()}";

                writer.Write(alwayswrite);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Une erreur est survenue lors de l'enregistrement des informations dans le fichier CSV.");
            Console.WriteLine(e.Message);
        }
        
    }
    protected virtual string GetFileName()
    {
        return "Personnes.csv";
    }
}
