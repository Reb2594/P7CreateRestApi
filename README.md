# P7CreateRestApi
# Findexium - PostTrades

## Description

Ce projet s'incrit dans le cadre de la formation **OpenClassrooms**. Il s'agit d'un projet fictif dans lequel il était demandé de développer une API REST sécurisée avec ASP.NET Core.

Il s'agissait de reprendre les missions de Stéphanie, développeuse qui a quitté le service, et poursuivre le développement de PostTrades, qui est une application destinée à simplifier la communication et l'utilisation des informations post-transaction entre le front et le back-office pour les entreprises institutionnelles à revenu fixe.

2 missions principales : 
- Corriger et implémenter les fonctionnalités de l’application en utilisant ASP.NET Core, API Web, Entity Framework, et API Web Security. 
- Implémenter l’authentification JWT. 

## Technologies utilisées

- **ASP.NET Core**
- **Entity Framework Core**
- **API Web**
- **JWT Authentication**
- **Serilog** (pour la journalisation)
- **Swagger** (pour la documentation de l'API)
- **xUnit** (pour les tests unitaires)

## Installation et exécution

### Prérequis
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server ou un autre fournisseur de base de données compatible
- Un éditeur de code comme [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### Étapes d'installation

1. **Cloner le projet**
   ```sh
   git clone https://github.com/votre-utilisateur/findexium-posttrades.git
   ```

2. **Configurer la base de données**
   - Modifier la chaîne de connexion dans `appsettings.json`.
   - Appliquer les migrations :
     ```sh
     dotnet ef database update
     ```
     
**Remarque** : à la création, la base de données est automatiquement crée et peuplée avec des données fictives pour faciliter les tests.

3. **Lancer l'application**
   ```sh
   dotnet run
   ```

4. **Accéder à la documentation Swagger**
   - Ouvrir : `http://localhost:7210/swagger`

## Fonctionnalités principales

- Authentification et gestion des utilisateurs avec **JWT**
- API REST pour la gestion des transactions
- Sécurisation des endpoints
- Documentation avec **Swagger**
- Gestion des logs avec **Serilog**
- **Données pré-remplies** : à la création, la base de données est automatiquement peuplée avec des données fictives pour faciliter les tests.
- **Utilisateur administrateur** : Un utilisateur avec un rôle administrateur est créé automatiquement. Vous pouvez retrouver son login et son mot de passe dans `Program.cs`.
- **Projet de tests unitaires** : Un projet de tests est inclus, couvrant les couches **Controller** et **Service** avec des tests unitaires utilisant **xUnit**.

## Auteur

**Rebecca Bajazet**

