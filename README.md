# MultipleRotatorEditor #

## Description ##

Ce projet a pour but de créer un outil (Tool) dans Unity pour améliorer notre environnement de travail (workbench).
L'outil est destiné à manipuler des "Rotator". Il va permettre de choisir un ensemble de rotator à modifier, ainsi que les variables que l'on souhaite modifier pour chacun de ces rotator tout en les affichants à l'écran.
On peut le voir comme divisé en 3 parties : 
- Rotators to edit
- Editor
- Selected rotators

## Auteur ##

Jules Jehanno

## Fonctionnement ##

- Aucune dépendances n'ont besoins d'être ajoutées
- Il suffit de télécharger le projet et lancer dans Unity.


## Contenu du projet ##

Essentiellement le plus important sont les scripts présents dans le dossier "Editor" :
- CustomRotatorEditor
- RotatorEditorWindow


## Choix de développement ##

Je me suis en premier lieu diriger vers un "ScriptableWizard" pour créer mon "EditorWizard" qui constiturai mon nouvel outil.
Cependant après avoir fait la première partie (comme mentionné dans Description), je me suis rendu compte qu'il ne serait pas très simple de gérer ma fenêtre comme je le voudrai. Je me suis alors penché sur un "EditorWindow", beaucoup plus maniable.
La tâche la plus complexe dans ce projet fût de manipuler les variables de la classe Rotator, car elles sont presques toutes soumises à la sérialisation (dû au SerializedField). Et également du côté visuel, car comme on peut le voir mon outil n'est pas le plus jolie possible mais est toutefois compréhensible et userFriendly.
