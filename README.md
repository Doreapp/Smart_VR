# Smart_VR

Jeu de Réalité virtuelle, développé pour une utilisation sur machines de musculation de ANTS (ENS Lyon). 
Développé par l'Hexanome H4241, Promo 2022 du département IF de l'INSA.

# Environnement de développement

Jeu developpé avec Unity. Nous utilisons l'**API** [Maps SDK](https://www.microsoft.com/en-us/maps/mixed-reality) de **Bing** ([Repository GitHub](https://github.com/Microsoft/MapsSDK-Unity))

## Installation 

1. Suivre ce [tutoriel](https://www.xrterra.com/developing-for-vr-with-quest-2-unity-for-the-first-time-a-step-by-step-guide/) afin d'avoir **Unity** installé correctement pour le développement sur *Oculus Quest 2*

2. Cloner ce repository

```
git clone https://github.com/Doreapp/Smart_VR
```

3. Ouvrir la scène 
```
cd Assets/Scenes
./SampleScene.unity
```

Pour envoyer sur le casque : 
* `File` > `Build Settings...`
* Aller dans `Android` et cliquer `Switch Platform`
* Lorsque le casque est branché, vérifier qu'à la ligne `Run Device`, l'*Oculus Quest 2* est disponible 
* Aller dans `Player Settings...` et définir `Company Name` et `Product Name`
* Fermer `Player Settings`
* Cliquer sur `Build and run`
* Enregistrer l'`APK`
* Attendre que le jeu se lance sur le casque.

Un fois que vous avez envoyer le jeu une fois dans le casque, il suffit de faire `File` > `Build and run` pour envoyer le jeu sur le casque.

## Structure des fichiers 

* `Assets`/`BasicObject` : Les *prefabriqués*, objets basiques, non placés directement dans le jeu. 
    * `BasicBalise`, une balise à atteindre lors du jeu de piste
    * `BasicMap`, un *chunk* correspondant à un *bou de carte*, implementant `MapRenderer` (de `Maps SDK`)
    * `BasicText`, un texte 3D classique
* `Assets`/`Maps` : 
* `Assets`/`Material` : Contient les matériaux utilisés
    * `Balise`, l'objet 3D représentant une balise
* `Assets`/`Scene` : Les scènes 
    * `SampleScene`, scène de déplacement dans une représentation 3D de notre monde, utilisant l'API `Maps SDK`
* `Assets`/`Script` : Les scripts utilisés
    * `BaliseLoader` 
    * `ComputerControls`, permet de se déplacer sans envoyer le jeu dans le casque
    Avec les touches :
        * `Z` pour monter (axe `y`)
        * `S` pour descendre (axe `y`)
        * Flèches directionnelles gauche/droite pour tourner la caméra (autour de l'axe `y`)
        * Flèches directionnelles haut/bas pour avancer/reculer dans la direction de la caméra
    * `Inertie`, gère le déplacement du joueur de manière fluide.
        * `Game Friction` est le coefficient de frottement fluide
        * `Head` est l'objet modélisant le casque, et tourner en fonction de l'orientation réelle du casque
        * `Cam` est un `RigidBody` correspondant à la caméra (joueur)
        * `Player` est le joueur, c'est-à-dire même objet que `Cam` en vision *Première Personne*
        * `Log Text` est un texte 3D permettant d'afficher des informations de *log* durant le jeu (sur le casque)
        * `Coef speed` est un coefficient sans réalité physique, permettant de définir la vitesse de déplacement
        * `Min Angle` est l'angle de rotation minimal (dans Unity) pour faire tourner la direction du joueur 
        * `Coef rotation` est un coefficient sans réalité physique, définissant la vitesse de rotation du joueur
        * `COMPUTER_CONTROL` est un booléen activant ou non la gestion de l'inertie sur l'ordinateur (pour tester hors de casque). S'il est activé, appuyer sur la barre espace permet de faire avancer le joueur. **Attetion, il faut le desactiver lors d'un envoi sur le casque, sinon le déplacement du joueur sera impossible**
    * `LookAtPlayer`
    * `MapLoader` Permet de charger la carte 3D en fonction des déplacements du joueur. Utilise un principe de *chunk*.
        * `Basic Map` est l'objet classique (préfabriqué) implémentant `MapRenderer` (voir `Assets/BasicObject/BasicMap`)
        * `Player` est l'objet représentant le joueur
        * `Size` est le nombre de *couronnes* chargées autour du chunk central
            * `Size=0` implique de ne charger qu'une seul chunk
            * `Size=1` implique de charger 3x3 chunks 
            * `Size=2` --> 5x5 chunks
            * ... 
    * `Scoring`
    * `TouchCamera`


