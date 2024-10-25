# TransConnect - Projet C#

TransConnect est une application console en C# permettant de gérer les données liées à un service de transport, incluant les clients, salariés, chauffeurs, véhicules, livraisons et commandes. Ce projet met en pratique des concepts de Programmation Orientée Objet (POO) en structurant les différentes entités sous forme de classes et en implémentant des méthodes pour une gestion efficace.

## Table des matières
- [Structure du Projet](#structure-du-projet)
- [Classes et Fonctionnalités](#classes-et-fonctionnalités)
  - [Classe Personne](#classe-personne)
  - [Classe Client](#classe-client)
  - [Classe Salarié](#classe-salarié)
  - [Classe Chauffeur](#classe-chauffeur)
  - [Classe Véhicule](#classe-véhicule)
  - [Classes Livraison et Commande](#classes-livraison-et-commande)
  - [Classe GrapheDijkstra](#classe-graphedijkstra)
- [Fonctionnalités dans Program.cs](#fonctionnalités-dans-programcs)
- [Exécution](#exécution)
- [Contributions](#contributions)

---

## Structure du Projet
L’application TransConnect est structurée autour de plusieurs classes principales : `Personne`, `Client`, `Salarié`, `Chauffeur`, `Véhicule`, `Livraison`, `Commande` et `GrapheDijkstra`. Chaque classe est conçue pour représenter une entité spécifique et gérer les données et les comportements associés.

## Classes et Fonctionnalités

### Classe Personne
La classe `Personne` représente une personne avec les attributs suivants :
- `Nom`, `Prénom`, `DateDeNaissance`, `Adresse`, `Email` et `NuméroDeTéléphone`.
- Deux constructeurs sont disponibles : l'un pour créer une personne avec tous les attributs, et l'autre pour charger les informations d’une personne à partir d’un fichier CSV basé sur son nom.
- Méthodes : `ToString()` pour afficher le nom de la personne, `To_csv()` pour sauvegarder les informations dans un fichier CSV.

### Classe Client
La classe `Client` hérite de `Personne` et ajoute l'attribut `MontantCumule` pour enregistrer le montant total des commandes passées par le client.
- Méthodes : `AddMontant()` pour incrémenter le montant cumulé des commandes, `To_csv()` pour sauvegarder les informations.

### Classe Salarié
La classe `Salarié` étend `Personne` en ajoutant :
- Attributs : `NumSécuSociale`, `Poste`, `Salaire`, et une liste de subordonnés.
- Méthodes : `AjouterSubordonné()` et `RetirerSubordonné()` pour la gestion hiérarchique, ainsi que des méthodes pour embaucher et licencier un salarié.

### Classe Chauffeur
La classe `Chauffeur` hérite de `Salarié` et ajoute :
- Attributs : `DateDerniereCourse` et `Disponibilité`.
- Génère une ancienneté aléatoire entre 0 et 5 ans.
- Permet de suivre les données spécifiques aux chauffeurs.

### Classe Véhicule
La classe `Véhicule` contient les attributs `Immatriculation` et `Consommation`, et est la classe parente de `Voiture` et `PoidsLourd`.
- Sous-classes : `CamionBenne`, `CamionFrigo`, `CamionCiterne` héritent de `PoidsLourd` et ajoutent des caractéristiques spécifiques.

### Classes Livraison et Commande

- **Classe Livraison** : représente une livraison avec des attributs tels que le chauffeur assigné, le véhicule, les villes de départ et d’arrivée, et le prix. Elle utilise un graphe pour calculer le chemin le plus court entre les villes.
  - Méthode `GetPrice()` pour calculer le coût de la livraison en fonction de la distance, la consommation et l’ancienneté du chauffeur.
- **Classe Commande** : représente une commande passée par un client et inclut des attributs tels que le numéro de commande, la livraison associée, le client, le prix et la date.

### Classe GrapheDijkstra
La classe `GrapheDijkstra` implémente l'algorithme de Dijkstra pour déterminer le chemin le plus court entre deux points dans un graphe pondéré.
- **Méthode CourtChemin** : trouve le chemin le plus court entre un point de départ et un point d’arrivée.
- **Méthode DistanceChemin** : calcule la distance totale pour un chemin donné.

## Fonctionnalités dans Program.cs
Le fichier `Program.cs` inclut :
- Des méthodes pour initialiser les objets `Client`, `Commande`, `Chauffeur` et `Salarié`.
- Des options de menu pour interagir avec les clients, commandes, statistiques, et hiérarchie.
- Une organisation du menu sous forme de `switch` dans une boucle `while(true)` pour permettre une navigation libre.

Chaque classe implémente une méthode `To_csv()` pour sauvegarder les informations dans des fichiers CSV, ainsi qu’une lecture de ces fichiers pour charger les données au démarrage de l’application.

## Exécution
Pour exécuter ce projet :
1. Clonez le dépôt : 
   ```bash
   git clone https://github.com/Sevane7/TransConnect.git
