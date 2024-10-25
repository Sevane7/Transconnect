using System.ComponentModel.Design;
using System.IO;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Specialized;
using System.Globalization;

namespace TransConnect
{
    public class FichierClient
    {
        List<Client> FClient { get; set; }
        public FichierClient()
        {
            this.FClient = new List<Client>();
        }
        public void AjouterClient(Client c)
        {
            FClient.Add(c);
        }
        public void SuprimerClient(Client client)
        {
            if(this.FClient.Contains(client)) this.FClient.Remove(client);
        }
        //public void ModifierClient(Client ancienclient)
        //{
        //    if (FClient.Contains(ancienclient))
        //    {
        //        Client nouveau_client = new Client(ancienclient.Nom);
        //        Console.WriteLine("Quel nouveau mail ?");
        //        nouveau_client.Mail = Console.ReadLine();
        //        Console.WriteLine("Quel nouvelle adresse ?");
        //        nouveau_client.Adresse = Console.ReadLine();
        //        Console.WriteLine("Quel nouveau telephone ?");
        //        nouveau_client.Telephone = Console.ReadLine();
        //    }
        //    else { Console.WriteLine($"{ancienclient.Nom} n'est pas client chez nous"); }
        //}
        private void AfficherFClient()
        {
            foreach (Client client in this.FClient) { client.ToString(); } // ou ToString qu'on def dans client pour afficher toutes les chars
        }
        public void ClientsOrdreAlphabetique()
        {
            this.FClient.OrderBy(c => c.Nom).ToList();
            AfficherFClient();
        }
        public void ClientsParVille()
        {
            this.FClient.OrderBy(c => c.Adresse).ToList();
            AfficherFClient();
        }
        public void MeilleursClients()
        {
            this.FClient.OrderByDescending(c => c.Montant_cumule).ToList();
            AfficherFClient();
        }
        public void SauvegardeCDansFichier(string nomFichier)
        {
            List<string> lignesClients = new List<string>();
            foreach (Client client in FClient)
            {
                string ligneClient = $"{client.Nom},{client.Prenom},{client.Naissance},{client.Adresse},{client.Mail},{client.Telephone},{client.Montant_cumule}";
                lignesClients.Add(ligneClient);
            }

            File.WriteAllLines(nomFichier, lignesClients);
        }
        public void ChargerFichier(string nomFichier)
        {
            if (File.Exists(nomFichier))
            {
                this.FClient.Clear();
                using (StreamReader reader = new StreamReader(nomFichier))
                {
                    string ligne;
                    while ((ligne = reader.ReadLine()) != null)
                    {
                        string[] informationsClient = ligne.Split(',');
                        string nom = informationsClient[0];
                        string prenom = informationsClient[1];
                        DateTime naissance = DateTime.Parse(informationsClient[2]);
                        string adresse = informationsClient[3];
                        string mail = informationsClient[4];
                        string telephone = informationsClient[5];
                        int Montant_cumule = int.Parse(informationsClient[6]);
                        FClient.Add(new Client(nom, prenom, naissance, adresse, mail, telephone, Montant_cumule));
                    }
                }
            }
            else
            {
                Console.WriteLine("Le fichier est inexistant.");
            }
        }
    }  
    
    internal class Program
    {
        #region Initialisation tuples
        /// <summary>
        /// Retourne une date aléatoire pour la création des objets
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static DateTime GetRandomDate(DateTime startDate)
        {
            Random random = new Random();

            // Calculer la différence entre les dates de début et de fin en jours
            TimeSpan timeSpan = DateTime.Today - startDate;

            // Générer un nombre aléatoire de jours entre 0 et le nombre total de jours
            int randomDays = random.Next(0, (int)timeSpan.TotalDays);

            // Ajouter ce nombre de jours à la date de début pour obtenir une date aléatoire
            return startDate.AddDays(randomDays);
        }
        /// <summary>
        /// Initie les tuples Client et les range dans une liste
        /// </summary>
        /// <returns></returns>
        ///
        static List<Client> InitClient()
        {
            List<Client> clients = new List<Client>
            {
                new Client("Dupont", "Jean", new DateTime(1980, 5, 1), "1 rue de Paris, Paris", "jean.dupont@mail.com", "0123456789"),
                new Client("Martin", "Marie", new DateTime(1990, 7, 12), "2 rue de Lyon, Lyon", "marie.martin@mail.com", "0123456790"),
                new Client("Durand", "Pierre", new DateTime(1985, 8, 23), "3 rue de Marseille, Marseille", "pierre.durand@mail.com", "0123456791"),
                new Client("Petit", "Laura", new DateTime(1992, 2, 14), "4 rue de Bordeaux, Bordeaux", "laura.petit@mail.com", "0123456792"),
                new Client("Moreau", "Sophie", new DateTime(1978, 11, 30), "5 rue de Lille, Lille", "sophie.moreau@mail.com", "0123456793"),
                new Client("Lefevre", "Luc", new DateTime(1983, 6, 19), "6 rue de Nantes, Nantes", "luc.lefevre@mail.com", "0123456794"),
                new Client("Roux", "Julien", new DateTime(1988, 10, 25), "7 rue de Toulouse, Toulouse", "julien.roux@mail.com", "0123456795"),
                new Client("David", "Emma", new DateTime(1995, 4, 9), "8 rue de Nice, Nice", "emma.david@mail.com", "0123456796"),
                new Client("Bertrand", "Lucas", new DateTime(1982, 3, 15), "9 rue de Montpellier, Montpellier", "lucas.bertrand@mail.com", "0123456797"),
                new Client("Fournier", "Julie", new DateTime(1989, 12, 1), "10 rue de Strasbourg, Strasbourg", "julie.fournier@mail.com", "0123456798")
            };
            foreach (Client client in clients) client.To_csv();
            return clients;
        }
        /// <summary>
        /// Initie les tuples Chauffeurs
        /// </summary>
        /// <returns></returns>
        static List<Chauffeur> InitChauffeur()
        {
            List<Chauffeur> all_salarie = new List<Chauffeur>();
            Chauffeur chauffeur1 = new Chauffeur("Romu", "Nathan", new DateTime(1996, 1, 1), "16 rue de Rennes, Rennes", "nathan.romu@mail.com", "0123456704", 778901234, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur1);
            Chauffeur chauffeur2 = new Chauffeur("Rome", "Mia", new DateTime(1997, 3, 3), "17 rue de Dijon, Dijon", "mia.rome@mail.com", "0123456705", 889012345, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur2);
            Chauffeur chauffeur3 = new Chauffeur("Romi", "Leo", new DateTime(1995, 4, 4), "18 rue de Brest, Brest", "leo.romi@mail.com", "0123456706", 990123456, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur3);
            Chauffeur chauffeur4 = new Chauffeur("Rimou", "Zoe", new DateTime(1993, 5, 5), "19 rue de Limoges, Limoges", "zoe.rimou@mail.com", "0123456707", 101234567, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur4);
            Chauffeur chauffeur5 = new Chauffeur("Roma", "Ella", new DateTime(1992, 6, 6), "20 rue de Bayonne, Bayonne", "ella.roma@mail.com", "0123456708", 112345678, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur5);
            return all_salarie;
        }
        /// <summary>
        /// Initie les tuples Salarie et les range dans une liste
        /// </summary>
        /// <returns></returns>
        static List<Salarie> InitSalarie()
        {
            // Initialisation des Salaries
            List<Salarie> all_salarie = new List<Salarie>();

            #region Tuples 
            // n
            Salarie PDG = new Salarie("Dupond", "Jean", new DateTime(1975, 3, 15), "1 rue de Paris, Paris", "jean.dupond@mail.com", "0123456789", 123456789, "Directeur General", "10000");
            all_salarie.Add(PDG);

            // n-1
            Salarie DC = new Salarie("Fiesta", "Marie", new DateTime(1980, 6, 10), "2 rue de Lyon, Lyon", "marie.fiesta@mail.com", "0123456790", 234567890, "Directeur Commerciale", "5000");
            all_salarie.Add(DC);
            Salarie DO = new Salarie("Fetard", "Pierre", new DateTime(1978, 7, 22), "3 rue de Marseille, Marseille", "pierre.fetard@mail.com", "0123456791", 345678901, "Directeur des opérations", "5000");
            all_salarie.Add(DO);
            Salarie DRH = new Salarie("Joyeuse", "Sophie", new DateTime(1985, 9, 25), "4 rue de Bordeaux, Bordeaux", "sophie.joyeuse@mail.com", "0123456792", 456789012, "Directeur des RH", "5000");
            all_salarie.Add(DRH);
            Salarie DF = new Salarie("GripSous", "Luc", new DateTime(1982, 11, 5), "5 rue de Lille, Lille", "luc.gripsous@mail.com", "0123456793", 567890123, "Directeur Financier", "5000");
            all_salarie.Add(DF);

            // n-2
            Salarie chefequipe1 = new Salarie("Royal", "Laura", new DateTime(1990, 2, 18), "6 rue de Nantes, Nantes", "laura.royal@mail.com", "0123456794", 678901234, "Chef Equipe", "2000");
            all_salarie.Add(chefequipe1);
            Salarie chefequipe2 = new Salarie("Prince", "Julien", new DateTime(1992, 4, 12), "7 rue de Toulouse, Toulouse", "julien.prince@mail.com", "0123456795", 789012345, "Chef Equipe", "2000");
            all_salarie.Add(chefequipe2);
            Salarie directioncomptable = new Salarie("Picsou", "Emma", new DateTime(1988, 8, 28), "8 rue de Nice, Nice", "emma.picsou@mail.com", "0123456796", 890123456, "Direction Comptable", "2000");
            all_salarie.Add(directioncomptable);
            Salarie controleurgestion = new Salarie("GrosSous", "Lucas", new DateTime(1995, 1, 9), "9 rue de Montpellier, Montpellier", "lucas.grossous@mail.com", "0123456797", 901234567, "Controleur de Gestion", "2000");
            all_salarie.Add(controleurgestion);
            Salarie commercial1 = new Salarie("Forge", "Paul", new DateTime(1993, 3, 14), "10 rue de Strasbourg, Strasbourg", "paul.forge@mail.com", "0123456798", 112345678, "Commercial", "2000");
            all_salarie.Add(commercial1);
            Salarie commercial2 = new Salarie("Fermi", "Alice", new DateTime(1991, 5, 11), "11 rue de Nancy, Nancy", "alice.fermi@mail.com", "0123456799", 223456789, "Commercial", "2000");
            all_salarie.Add(commercial2);
            Salarie contrats1 = new Salarie("Couleur", "Eva", new DateTime(1994, 10, 10), "12 rue de Metz, Metz", "eva.couleur@mail.com", "0123456700", 334567890, "Contrats", "2000");
            all_salarie.Add(contrats1);
            Salarie contrats2 = new Salarie("TouteMonde", "Leo", new DateTime(1987, 12, 5), "13 rue de Rouen, Rouen", "leo.toutemonde@mail.com", "0123456701", 445678901, "Contrats", "2000");
            all_salarie.Add(contrats2);

            // n-3
            Salarie comptable1 = new Salarie("Fourmier", "Claire", new DateTime(1998, 11, 3), "14 rue de Reims, Reims", "claire.fourmier@mail.com", "0123456702", 556789012, "Comptable", "1000");
            all_salarie.Add(comptable1);
            Salarie comptable2 = new Salarie("Gautier", "Thomas", new DateTime(2000, 2, 2), "15 rue de Caen, Caen", "thomas.gautier@mail.com", "0123456703", 667890123, "Comptable", "1000");
            all_salarie.Add(comptable2);
            Salarie chauffeur1 = new Chauffeur("Romu", "Nathan", new DateTime(1996, 1, 1), "16 rue de Rennes, Rennes", "nathan.romu@mail.com", "0123456704", 778901234, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur1);
            Salarie chauffeur2 = new Chauffeur("Rome", "Mia", new DateTime(1997, 3, 3), "17 rue de Dijon, Dijon", "mia.rome@mail.com", "0123456705", 889012345, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur2);
            Salarie chauffeur3 = new Chauffeur("Romi", "Leo", new DateTime(1995, 4, 4), "18 rue de Brest, Brest", "leo.romi@mail.com", "0123456706", 990123456, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur3);
            Salarie chauffeur4 = new Chauffeur("Rimou", "Zoe", new DateTime(1993, 5, 5), "19 rue de Limoges, Limoges", "zoe.rimou@mail.com", "0123456707", 101234567, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur4);
            Salarie chauffeur5 = new Chauffeur("Roma", "Ella", new DateTime(1992, 6, 6), "20 rue de Bayonne, Bayonne", "ella.roma@mail.com", "0123456708", 112345678, "Chauffeur", "1000", new DateTime(2024, 5, 14), true);
            all_salarie.Add(chauffeur5);

            foreach (Salarie sal in all_salarie) sal.To_csv();
            #endregion

            #region Hiérarchie
            // Creation de la hiérarchie
            chefequipe1.AddSubordonnes(chauffeur1);
            chefequipe1.AddSubordonnes(chauffeur3);
            chefequipe1.AddSubordonnes(chauffeur5);

            chefequipe2.AddSubordonnes(chauffeur2);
            chefequipe2.AddSubordonnes(chauffeur4);

            directioncomptable.AddSubordonnes(comptable1);
            directioncomptable.AddSubordonnes(comptable2);

            DC.AddSubordonnes(commercial1);
            DC.AddSubordonnes(commercial2);

            DRH.AddSubordonnes(contrats1);
            DRH.AddSubordonnes(contrats2);

            DO.AddSubordonnes(chefequipe1);
            DO.AddSubordonnes(chefequipe2);

            DF.AddSubordonnes(directioncomptable);
            DF.AddSubordonnes(controleurgestion);

            PDG.AddSubordonnes(DC);
            PDG.AddSubordonnes(DO);
            PDG.AddSubordonnes(DRH);
            PDG.AddSubordonnes(DF);
            #endregion

            return all_salarie;
        }
        /// <summary>
        /// Initie les tuples Commande et les range dans une liste
        /// </summary>
        /// <param name="cli0"></param>
        /// <returns></returns>
        static List<Commande> InitCommande(List<Client> cli0)
        {
            List<Commande> all_commande = new List<Commande>();

            Random random = new Random();

            string[] villes = { "Paris", "Angers", "La Rochelle", "Bordeaux", "Pau", "Toulouse", "Montpellier", "Nimes", "Marseille", "Monaco", "Toulon", "Avignon", "Biarritz", "Lyon", "Rouen" };

            for (int i = 0; i < 30; i++)
            {
                // Choix aléatoire des villes
                int indexVilleDepart = random.Next(villes.Length);
                int indexVilleArrivee = random.Next(villes.Length);

                // S'assurer que la ville d'arrivée est différente de celle de départ
                while (indexVilleArrivee == indexVilleDepart)
                {
                    indexVilleArrivee = random.Next(villes.Length);
                }

                string villeDepart = villes[indexVilleDepart];
                string villeArrivee = villes[indexVilleArrivee];

                // Génération aléatoire du montant de la commande entre 25 et 70
                double montantCommande = 25 + random.NextDouble() * (70 - 25);

                Livraison livraison = new Livraison(villeDepart, villeArrivee);
                Random random1 = new Random();
                int client_i = random1.Next(cli0.Count());
                Commande commande = new Commande(i, livraison, cli0[client_i], montantCommande);
                all_commande.Add(commande);
            }
            foreach (Commande com in all_commande) com.To_csv();
            return all_commande;
        }
        #endregion

        #region Creation tuple par saisie utilisateur
        /// <summary>
        /// Fait saisir une date à l'utilisateur et la retourne
        /// </summary>
        /// <returns></returns>
        public static DateTime SaisirDate()
        {

            DateTime date;
            bool saisieValide = false;

            do
            {
                Console.WriteLine("Saisir une date de naissance au format dd/MM/yyyy");
                string saisie = Console.ReadLine();

                // Tenter de convertir la saisie en DateTime
                saisieValide = DateTime.TryParseExact(saisie, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out date);

                if (!saisieValide)
                {
                    Console.WriteLine("Format invalide. Veuillez saisir une date et une heure au format dd/MM/yyyy.");
                }

            } while (!saisieValide);

            return date;
        }
        /// <summary>
        /// Choisit un véhicule et le retourne
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vehicule Choix_vehicule(bool r = true)
        {
            // Creation de la flotte
            #region
            List<string> flotte = new List<string>();
            flotte.Add("voiture");
            flotte.Add("camionnette");
            flotte.Add("camion citerne");
            flotte.Add("camion benne");
            flotte.Add("camion frigorifique");

            Voiture voiture1 = new Voiture(4, 2.8, "AZE");
            Voiture voiture2 = new Voiture(5, 2.5, "AZERTY");
            Voiture voiture3 = new Voiture(2, 1.2, "AZERTYUIOP");

            Camionnette camionnette1 = new Camionnette("usage1", 2.4, "GHYJ");
            Camionnette camionnette2 = new Camionnette("usage2", 2.6, "KODH");
            Camionnette camionnette3 = new Camionnette("usage3", 2.1, "LOPD");

            Camion_citerne camion_cit1 = new Camion_citerne(90, "matiere1", 3.4, "HTDFG", "cuve1");
            Camion_citerne camion_cit2 = new Camion_citerne(98, "matiere2", 3.8, "LDMPC", "cuve2");
            Camion_citerne camion_cit3 = new Camion_citerne(87, "matiere3", 3.0, "NBDJI", "cuve3");

            Camion_benne camion_benne1 = new Camion_benne(95, "matiere4", 9.4, "GTDFU", 2, false);
            Camion_benne camion_benne2 = new Camion_benne(99, "matiere5", 9.8, "JELSP", 3, true);
            Camion_benne camion_benne3 = new Camion_benne(90, "matiere6", 9.4, "GTDFU", 1, true);

            Camion_frigo camion_frigo1 = new Camion_frigo(87, "matiere7", 8.5, "QKDNO", 2);
            Camion_frigo camion_frigo2 = new Camion_frigo(84, "matiere8", 8.2, "QKDNO", 1);
            Camion_frigo camion_frigo3 = new Camion_frigo(89, "matiere7", 8.9, "QKDNO", 3);
            #endregion

            // Choix Vehicule
            #region

            string[] gamme = { "voiture", "camionnette", "camion citerne", "camion benne", "camion frigorifique" };
            string? answ_v;
            if (r)
            {
                Random random = new Random();
                int rIndex = random.Next(gamme.Length);
                answ_v = gamme[rIndex];
            }
            else
            {
                Console.WriteLine("Quel véhicule sera utilisé entre : voiture, camionnette, camion citerne, camion benne, camion frigorifique ?");
                answ_v = Console.ReadLine();
            }
            while (true)
            {
                if (flotte.Contains(answ_v)) break;
                else
                {
                    Console.WriteLine("Choisir entre : voiture, camionnette, camion citerne, camion benne, camion frigorifique");
                    answ_v = Console.ReadLine();
                }
            }
            #endregion

            // Return
            #region
            switch (answ_v)
            {
                case ("voiture"): return voiture1;
                case ("camionnette"): return camionnette1;
                case ("camion citerne"): return camion_cit1;
                case ("camion benne"): return camion_benne1;
                case ("camion frigorifique"): return camion_frigo1;
            }
            #endregion

            return null;
        }
        /// <summary>
        /// Choisit une ville et la retourne
        /// </summary>
        /// <returns></returns>
        static string Choix_Ville()
        {
            Console.WriteLine("Choisissez entre Paris, Angers, La Rochelle, Bordeaux, Pau, Toulouse, Montpellier, Nimes, Marseille, Monaco, Toulon, Avignon, Biarritz, Lyon, Rouen");
            List<string> villes = new List<string>();
            villes.Add("Paris");
            villes.Add("Angers");
            villes.Add("La Rochelle");
            villes.Add("Bordeaux");
            villes.Add("Pau");
            villes.Add("Toulouse");
            villes.Add("Montpellier²");
            villes.Add("Nimes");
            villes.Add("Marseille");
            villes.Add("Monaco");
            villes.Add("Toulon");
            villes.Add("Avignon");
            villes.Add("Biarritz");
            villes.Add("Lyon");
            villes.Add("Rouen");

            string? answer_ville = Console.ReadLine();
            while (true)
            {
                if (villes.Contains(answer_ville)) return answer_ville;
                else
                {
                    Console.WriteLine("Paris, Angers, La Rochelle, Bordeaux, Pau, Toulouse, Montpellier, Nimes, Marseille, Monaco, Toulon, Avignon, Biarritz, Lyon, Rouen");
                    answer_ville = Console.ReadLine();
                }
            }
        }
        /// <summary>
        /// Créer un Client ou un Salarie avec saisie utilisateur et le retourne
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        static T CreerIndicidu<T>()
        {
            Console.WriteLine("Nom : ");
            string nom = Console.ReadLine();
            Console.WriteLine("Prenom : ");
            string prenom = Console.ReadLine();
            Console.WriteLine("Date de Naissance (dd/MM/yyyy) : ");
            Console.WriteLine("Jour : ");
            int jour = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Mois : ");
            int mois = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Annee : ");
            int annee = Convert.ToInt32(Console.ReadLine());
            DateTime naissance = new DateTime(annee, mois, jour);
            Console.WriteLine("Adresse : ");
            Console.WriteLine("Numero : ");
            string adresse = Console.ReadLine() + " ";
            Console.WriteLine("Rue :");
            adresse += "rue de " + Console.ReadLine();
            Console.WriteLine("Ville : ");
            adresse += ", " + Console.ReadLine();
            Console.WriteLine("Mail : ");
            string mail = Console.ReadLine();
            Console.WriteLine("Telephone : ");
            string tel = Console.ReadLine();

            if (typeof(T) == typeof(Client))
            {
                Client res = new Client(nom, prenom, naissance, adresse, mail, tel);
                res.To_csv();
                return (T)(object)res;
            }
            if (typeof(T) == typeof(Salarie))
            {
                Console.WriteLine("Numero ss : ");
                int num_ss = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Poste : ");
                string poste = Console.ReadLine();
                Console.WriteLine("Salaire : ");
                string salaire = Console.ReadLine();
                Salarie res = new Salarie(nom, prenom, naissance, adresse, mail, tel, num_ss, poste, salaire);
                res.To_csv();
                return (T)(object)res;
            }
            else
            {
                throw new ArgumentException("Type de personne non reconnu.");
            }
        }
        /// <summary>
        /// Créer une livraison grace aux différentes méthode
        /// </summary>
        /// <returns></returns>
        static Livraison CreerLivraison()
        {
            Console.WriteLine("Quel produit voulez-vous commander ?");
            Chauffeur chauffeur_commande = Chauffeur_dispo();
            if (chauffeur_commande == null)
            {
                Console.WriteLine("La commande est annulée, réessayez demain.");
                return null;
            }
            Vehicule vehicule_commande = Choix_vehicule(false);
            Console.WriteLine("Quelle ville de départ ?");
            string? depart = Choix_Ville();
            Console.WriteLine("Quelle ville d'arrivée ?");
            string? arrivee = Choix_Ville();

            return new Livraison(depart, arrivee);
        }
        /// <summary>
        /// Créer une commande grace aux différentes méthodes
        /// </summary>
        /// <param name="cli"></param>
        /// <param name="num_commande"></param>
        /// <returns></returns>
        static Commande CreerCommande(Client cli, int num_commande)
        {
            Console.WriteLine("Quel est le prix de la commande ?");
            double prix_c = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
            Commande res = new Commande(num_commande, CreerLivraison(), cli, prix_c);
            res.To_csv();
            return res;
        }
        /// <summary>
        /// Verifie la disponibilié d'un chauffeur et le retourne
        /// </summary>
        /// <returns></returns>
        public static Chauffeur Chauffeur_dispo()
        {
            string nom = "";
            string prenom = "";
            DateTime date_naissance = new DateTime();
            string adresse = "";
            string mail = "";
            string tel = "";
            int num_ss = 0;
            string poste = "";
            string salaire = "";
            DateTime date_derniere_commande = new DateTime();
            bool dispo = true;

            try
            {
                foreach (string line in File.ReadLines("Chauffeurs.csv").Skip(1))
                {
                    string[] cols = line.Split(",");
                    date_naissance = DateTime.ParseExact(cols[2], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date_derniere_commande = DateTime.ParseExact(cols[10], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (cols[11] == "True")
                    {
                        nom = cols[0];
                        prenom = cols[1];
                        adresse = cols[3] + " " + cols[4];
                        mail = cols[5];
                        tel = cols[6];
                        num_ss = Convert.ToInt32(cols[7]);
                        poste = cols[8];
                        salaire = cols[9];
                    }
                    else
                    {
                        Console.WriteLine("Aucun chauffeur disponible aujourd'hui");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur est survenue lors de la lecture du fichier");
                Console.WriteLine(e.Message);
            }
            Chauffeur res = new Chauffeur(nom, prenom, date_naissance, adresse, mail, tel, num_ss, poste, salaire, date_derniere_commande, dispo);
            //Console.WriteLine($"C'est M.{res.Nom} qui s'occupe de la livraison, il {res.Anciennete} ans d'ancienneté. Le prix de la livraison est donc {res.Anciennete} fois plus cher.");
            return res;
        }
        public static Chauffeur Choix_Chauffeur(List<Chauffeur> all_chauf)
        {
            List<Chauffeur> ch = new List<Chauffeur>();
            foreach(Chauffeur c in all_chauf)
            {
                if(c.Dispo == true) ch.Add(c);
            }
            if (ch.Count() == 0)
            {
                Console.WriteLine("Aucun chauffeur dispo, annulation de la commande");
                return null;
            }
            Random random = new Random();
            int index_ = random.Next(0, ch.Count);
            return ch[index_];
        }
        #endregion

        #region Manipulation CSV
        /// <summary>
        /// Ajouter le montant de la commande au montant cumulé du client de la commande
        /// </summary>
        /// <param name="cli"></param>
        /// <param name="com"></param>
        static void Update_Client(List<Client> cli, List<Commande> com)
        {
            foreach (Commande c in com)
            {
                Console.WriteLine(c.ToString());
                cli.Find(c2 => c2.Nom == c.Client.Nom).Montant_cumule += c.Prix_commande;
            }
        }
        /// <summary>
        /// Utilisé pour lire Distances.csv, Retourne un List<Tuple<string, string, double>>
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<Tuple<string, string, double>> ReadFile(string filename)
        {
            List<Tuple<string, string, double>> res = new List<Tuple<string, string, double>>();

            try
            {
                foreach (string line in File.ReadLines(filename))
                {

                    string[] cols = line.Split(";");
                    Tuple<string, string, double> key = Tuple.Create(cols[0], cols[1], double.Parse(cols[2]));
                    res.Add(key);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur est survenue lors de la lecture du fichier");
                Console.WriteLine(e.Message);
            }
            return res;
        }
        /// <summary>
        /// Utilisé pour lire un csv et retourner une liste d'objet Client, Salarie, Chauffeur ou Commande
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        static List<T> ReadCSV<T>(string filename) where T : class
        {
            List<T> res = new List<T>();
            try
            {
                foreach (string line in File.ReadAllLines(filename).Skip(1))
                {
                    string[] cols = line.Split(',');
                    string nom = cols[0];
                    string prenom = cols[1];
                    DateTime naissance = DateTime.ParseExact(cols[2], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string adresse = cols[3] + ", " + cols[4];
                    string mail = cols[5];
                    string telephone = cols[6];
                    int num_ss = 0;
                    string poste = "";
                    string salaire = "";
                    DateTime dernierecomm = new DateTime();

                    if (typeof(T) == typeof(Client))
                    {
                        double montant = Convert.ToDouble(cols[7]);
                        Client cli = new Client(nom, prenom, naissance, adresse, mail, telephone, montant);
                        res.Add(cli as T);
                    }
                    if (cols.Length > 9)
                    {
                        num_ss = Convert.ToInt32(cols[7]);
                        poste = cols[8];
                        salaire = cols[9];
                    }
                    if (typeof(T) == typeof(Salarie))
                    {
                        Salarie sal = new Salarie(nom, prenom, naissance, adresse, mail, telephone, num_ss, poste, salaire);
                        res.Add(sal as T);
                    }
                    if (typeof(T) == typeof(Chauffeur))
                    {
                        dernierecomm = DateTime.ParseExact(cols[10], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        bool dispo = Convert.ToBoolean(cols[11]);
                        Chauffeur chau = new Chauffeur(nom, prenom, naissance, adresse, mail, telephone, num_ss, poste, salaire, dernierecomm, dispo);
                        res.Add(chau as T);
                    }
                    if (typeof(T) == typeof(Commande))
                    {
                        int num_commande = Convert.ToInt32(cols[0]);
                        Livraison liv = new Livraison(cols[3], cols[4]);
                        double prix_commande = Convert.ToDouble(cols[5]);
                        Client clicom = new Client(cols[6]);

                        Commande com = new Commande(num_commande, liv, clicom, prix_commande);
                        res.Add(com as T);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return res;
        }
        #endregion

        /// <summary>
        /// Methode de test pour l'algo de Dijkstra
        /// </summary>
        /// <param name="depart"></param>
        /// <param name="arrivee"></param>
        static void TestDijkstra(string depart, string arrivee)
        {
            Console.WriteLine($"{depart} -> {arrivee}");
            GrapheDijkstra test = new GrapheDijkstra(ReadFile("Distances.csv"));
            List<string> dijk = test.CourtChemin(depart, arrivee);
            for (int i = 0; i < dijk.Count(); i++) Console.Write(dijk[i] + " ");
            Console.WriteLine();
            double dist = test.DistanceChemin(dijk);
            Console.WriteLine();
        }
        static void Menu()
        {
            //List<Salarie> all_salaries = ReadCSV<Salarie>("Salaries.csv");
            //List<Chauffeur> all_chauffeurs = ReadCSV<Chauffeur>("Chauffeurs.csv");
            //List<Client> all_clients = ReadCSV<Client>("Clients.csv");
            //List<Commande> all_commande = ReadCSV<Commande>("Commandes.csv");

            List<Salarie> all_salaries = InitSalarie();
            List<Chauffeur> all_chauffeurs = InitChauffeur();
            List<Client> all_clients = InitClient();
            List<Commande> all_commande = InitCommande(all_clients);


            //Update_Client(all_clients, all_commande);
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("=== Menu Principal ===" +
                "\nPress : 1 -> Module Client (afficher les clients selon plusieurs critères)." +
                "\nPress : 2 -> Module Salarié (gérer la hiérarchie de l'entreprise : afficher, recruter, licencier)." +
                "\nPress : 3 -> Module Commande (gérer une commande : créer, consulter)." +
                "\nPress : 4 -> Module Statistiques (consulter certaines statistiques)." +
                "\nPress : 5 -> Autres Modules" +
                "\nPress : q -> Quitter");
                string? answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        Console.WriteLine("Bienvenue dans le module client. Que souhaitez vous faire ?" +
                            "\nPress : 1 -> Afficher les clients par ordre alphabetique sur leur nom ?" +
                            "\nPress : 2 -> Afficher les clients qui habite dans une certaine ville ?" +
                            "\nPress : 3 -> Afficher le client qui a le plus grand montnant cumulé ?");
                        string answ0 = Console.ReadLine();
                        while (answ0 != "1" && answ0 != "2" && answ0 != "3") answ0 = Console.ReadLine();

                        switch (answ0)
                        {
                            case "1":
                                Console.WriteLine("Voici la liste de clients : ");
                                all_clients.Sort((client1, client2) => client1.Nom.CompareTo(client2.Nom));
                                foreach (Client client in all_clients) Console.WriteLine(client.ToString());
                                break;
                            case "2":
                                Console.WriteLine("Quelle ville ?");
                                string ville = Console.ReadLine().ToLower();
                                List<Client> filtreville = all_clients.FindAll(client => client.Adresse.ToLower().Contains(ville));
                                foreach (Client client in filtreville) Console.WriteLine(client.ToString());
                                break;
                            case "3":
                                Console.WriteLine("Voici le client qui a le plus consommé ?");
                                Client viiiehein = all_clients.OrderByDescending(client => client.Montant_cumule).FirstOrDefault();
                                Console.WriteLine(viiiehein.ToString());

                                break;
                        }
                        break;
                    case "2":
                        #region Intro du module
                        Console.WriteLine("Que souhaitez-vous faire ?" +
                            "\nPress : 1 -> Afficher la hiérachie de l'entreprise" +
                            "\nPress : 2 -> Licencier un salarié existant" +
                            "\nPress : 3 -> Recruter un salarié");
                        string? answer2 = Console.ReadLine();
                        while (answer2 != "1" && answer2 != "2" && answer2 != "3") answer2 = Console.ReadLine();
                        #endregion

                        #region Déroulement du module
                        switch (answer2)
                        {
                            case "1":
                                Console.WriteLine("A partir de quel salarie voulez-vous afficher la hiérarchie (saisir le nom)?");
                                string? affiche_par_nom = Console.ReadLine();
                                foreach (Salarie sal in all_salaries)
                                {
                                    if (sal.Nom == affiche_par_nom) Salarie.AfficheSubbordonnes(sal);
                                }
                                break;
                            case "2":
                                Console.WriteLine("Quel client voulez-vous licencier (tapez le nom correct)");
                                string? salarie_licencier = Console.ReadLine();
                                foreach (Salarie sal in all_salaries)
                                {
                                    if (sal.Nom == salarie_licencier) Salarie.FireSalarie(all_salaries.Find(s => s.Poste == "Directeur General"), sal);
                                }
                                break;
                            case "3":
                                Console.WriteLine("Veuiller entrer les informations nécessaires pour le nouveau salarié.");
                                Salarie new_sal = CreerIndicidu<Salarie>();
                                all_salaries.Add(new_sal);
                                Console.WriteLine("Qui sera son supérieur hiérarchique ? (Ecrire le nom correct)");
                                string? boss_new_salarie = Console.ReadLine();
                                foreach (Salarie sal in all_salaries)
                                {
                                    if (sal.Nom.ToLower() == boss_new_salarie.ToLower()) Salarie.HireSalarie(sal, new_sal);
                                }
                                break;
                        }
                        #endregion

                        break;
                    case "3":
                        #region Intro au module
                        Console.WriteLine("Que souhaitez-vous faire ?" +
                            "\nPress : 1 -> Créer une nouvelle commande ?" +
                            "\nPress : 2 -> Consulter une commande passée ?");
                        string? answer_3 = Console.ReadLine();
                        while (answer_3 != "1" && answer_3 != "2") answer_3 = Console.ReadLine();
                        #endregion

                        switch (answer_3)
                        {
                            case ("1"):
                                Console.WriteLine("Voici les clients existants : ");
                                foreach (Client cli7 in all_clients) Console.WriteLine(cli7.Nom);
                                Console.WriteLine("Vous allez passer une commande. " +
                                    "\nVérifions si vous appartenez à la base de données. Veuillez saisire votre nom.");
                                string? nom_client_commande = Console.ReadLine();
                                Commande commande = null;
                                int num_commande = all_commande.OrderBy(c => c.Num_commande).LastOrDefault().Num_commande + 1;                               
                                
                                foreach (Client c in all_clients)
                                {
                                    if (c.Nom.ToLower() == nom_client_commande.ToLower())
                                    {
                                        commande = CreerCommande(c, num_commande);
                                    }
                                }
                                if (commande == null)
                                {
                                    Client new_client = CreerIndicidu<Client>();
                                    commande = CreerCommande(new_client, num_commande);
                                }
                                Console.WriteLine($"Votre commande a bien été enregistré au numéro : " + commande.Num_commande);
                                all_commande.Add(commande);
                                commande.To_csv();

                                break;
                            case ("2"):
                                int max_num_commande = all_commande.OrderBy(comm => comm.Num_commande).LastOrDefault().Num_commande;
                                Console.WriteLine("Les commandes vont de 0 à " + max_num_commande);
                                Console.WriteLine("Quelle commande voulez vous afficher ?");
                                int num_aff = Convert.ToInt32(Console.ReadLine());
                                while (true)
                                {
                                    if (num_aff > max_num_commande)
                                    {
                                        Console.WriteLine("Les commandes vont de 0 à " + max_num_commande);
                                        num_aff = Convert.ToInt32(Console.ReadLine());
                                    }
                                    else { break; }
                                }
                                Console.WriteLine("Voici la commande " + num_aff);
                                Commande com3 = all_commande.Find(comm => comm.Num_commande == num_aff);
                                Console.WriteLine(com3.ToString());
                                Console.WriteLine("Le chemin utilisé est : ");
                                foreach(string element in com3.Livraison.Chemin) Console.Write(element + " ");
                                Console.WriteLine();
                                break;
                        }
                        break;
                    case "4":
                        Console.WriteLine("Vous souhaiter afficher des statistiques. Que voulez vous afficher ?" +
                            "\nPress : 1 -> Afficher par chauffeur le nombre de livraisons effectuées." +
                            "\nPress : 2 -> Afficher les commandes selon une période de temps." +
                            "\nPress : 3 -> Afficher la moyenne des prix des commandes." +
                            "\nPress : 4 -> Afficher la moyenne des comptes clients." +
                            "\nPress : 5 -> Afficher la liste des commandes pour un client.");
                        string answ4 = Console.ReadLine();
                        while (answ4 != "1" && answ4 != "2" && answ4 != "3" && answ4 != "4" && answ4 != "5") answ4 = Console.ReadLine();

                        switch (answ4)
                        {
                            case "1":
                                Console.WriteLine("Vous souhaitez afficher les commandes par chauffeur.");

                                Dictionary<Chauffeur, List<Commande>> commandesParChauffeur = new Dictionary<Chauffeur, List<Commande>>();
                                foreach (Chauffeur ch in all_chauffeurs) commandesParChauffeur[ch] = new List<Commande>();
                                foreach (Commande comm in all_commande)
                                {
                                    int num_ss_commande = comm.Livraison.ChauffeurCommande.Num_ss;
                                    Chauffeur ch = commandesParChauffeur.Keys.FirstOrDefault(ch => ch.Num_ss == num_ss_commande);
                                    commandesParChauffeur[ch].Add(comm);
                                }
                                foreach (Chauffeur ch in commandesParChauffeur.Keys)
                                {
                                    Console.WriteLine($"Voici les commandes qu'a livré M. {ch.Nom} :");
                                    foreach (Commande comm in commandesParChauffeur[ch]) Console.WriteLine(comm.ToString());
                                }
                                break;
                            case "2":
                                Console.WriteLine("Vous devez saisir une date de début (pour la période de temps");
                                Console.WriteLine("Jour:");
                                int jour = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Mois:");
                                int mois = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Annee:");
                                int annee = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Vous devez saisir une date de fin (pour la période de temps");
                                Console.WriteLine("Jour:");
                                int jour1 = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Mois:");
                                int mois1 = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Annee:");
                                int annee1 = Convert.ToInt32(Console.ReadLine());

                                DateTime debut = new DateTime(annee, mois, jour);
                                DateTime fin = new DateTime(annee1, mois1, jour1);

                                List<Commande> commandesEntreDates = all_commande.Where(comm => comm.DateCommanande >= debut && comm.DateCommanande <= fin).ToList();
                                Console.WriteLine($"Voici les commandes comprises entre {debut.ToString("dd/MM/yyyy")} et {fin.ToString("dd/MM/yyyy")} :");
                                foreach (Commande commande in commandesEntreDates) Console.WriteLine(commande.ToString());

                                break;
                            case "3":
                                Console.WriteLine($"La moyenne des prix de toutes les commandes est de {all_commande.Sum(comm => comm.Prix_commande) / all_commande.Count()} euros.");
                                break;
                            case "4":
                                Console.WriteLine($"En moyenne un client dépense {all_clients.Sum(cli => cli.Montant_cumule) / all_clients.Count()} euros");
                                break;
                            case "5":
                                Console.WriteLine("Vous souhaiter afficher les commandes par clients.");
                                Dictionary<Client, List<Commande>> commandesParClient = new Dictionary<Client, List<Commande>>();
                                foreach (Client cli in all_clients) commandesParClient[cli] = new List<Commande>();
                                foreach (Commande comm in all_commande)
                                {
                                    Client cli = comm.Client;
                                    Console.WriteLine(cli.ToString());
                                    commandesParClient[cli].Add(comm);
                                }
                                foreach (Client cli in commandesParClient.Keys)
                                {
                                    Console.WriteLine($"Voici les commandes de M. {cli.Nom} :");
                                    foreach (Commande comm in commandesParClient[cli]) Console.WriteLine(comm.ToString());
                                }
                                break;

                        }
                        break;
                    case "5":
                        break;
                    case "q":
                        loop = false;
                        break;
                    default:
                        answer = Console.ReadLine();
                        break;
                }
            }
            
        }

        static void Main(string[] args)
        {
            //List<Salarie> s = InitSalarie();
            //List<Commande> c = InitCommande(InitClient());
            Menu();
        }
    }
}
