Feature: Location

#TACHES AU LANCEMENT DE L'APP
Background: 
	Given ajouter les clients suivants
	| Prénom  | Nom      | Mot de passe | Date de naissance | Numero du permis | Date de permis |
	| Clément | Bouboule | pass123      | 01/01/1970        | 22AD67619        | 01/01/2010     |
	Given ajouter les voitures suivantes
	| Immatriculation | Marque	| Modele | Couleur | Prix de base | Prix Km | CheveauxFisc |
	| AA55BBFG        | Smart   | Fortwo | Noir    | 100          | 5       | 6            |
	| AA55XXFF        | BMW     | Serie1 | Rouge   | 150          | 7       | 12           |
	| AA44RRHY        | Mercedes| A45	 | Noir    | 250          | 10      | 25           |

#CLIENT
Scenario: Connexion Client - Mot de passe incorrect
	Given le nom est "Clément" "Bouboule"
	And le mot de passe est "mauvaismdp"
	When essaie de connecter au compte
	Then la connexion est refusée
	And le message d'erreur est "Mot de passe incorrect"

Scenario: Connexion - Client reconnu
	Given le nom est "Clément" "Bouboule"
	And le mot de passe est "pass123"
	When essaie de connecter au compte
	Then la connexion est établie

#LOCATION
Scenario: Location - Reservation d'un vehicule - Conducteur mineur
	Given je suis néé le "01/01/2006"
	Then le client obtient la liste des vehicules
	Given la voiture que je veux est une "Smart" "Fortwo" "Noir"
	When verification de l'age pour les permission de location
	Then le message d'erreur est "Le client est mineur, il ne peut pas louer ce vehicule"

Scenario: Location - Reservation d'un vehicule - Vehicule est trop puissant
	Given je suis néé le "01/01/1999"
	Then le client obtient la liste des vehicules
	Given la voiture que je veux est une "Mercedes" "A45" "Noir"
	When verification de l'age pour les permission de location
	Then le message d'erreur est "Le vehicule est trop puissant, il ne peut pas louer ce vehicule"

Scenario: Location - Reservation d'un vehicule - Le vehicule est réserver
	Given je suis néé le "01/01/1999"
	Then le client obtient la liste des vehicules
	Given la voiture que je veux est une "BMW" "Serie1" "Rouge"
	When verification de l'age pour les permission de location
	Then le message d'erreur est "Le client peux louer ce vehicule"

Scenario: Location - Mise en location d'un vehicule
	Given je suis néé le "01/01/1999"
	And je prevois de faire "55" Km
	And je veux louer le vehicule du "10/03/2022" au "15/03/2022"
	Then le client obtient la liste des vehicules
	Given la voiture que je veux est une "BMW" "Serie1" "Rouge"
	When verification de l'age pour les permission de location
	And la voiture est loue au client 
	Then le message est "La voiture est louer"

Scenario: Location - Retour de location
	Given je rends le vehicule loue "BMW" "Serie1" "Rouge"
	And j'ai reserver le vehicule du "10/03/2022" au "15/03/2022"
	And j'ai parcouru "55" km
	When le client recoit sa facture 
	Then le montant de la facture est "535" euros