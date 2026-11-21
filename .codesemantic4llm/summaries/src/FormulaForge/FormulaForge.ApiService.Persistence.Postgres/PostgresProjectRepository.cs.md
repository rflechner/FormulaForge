---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.ApiService.Persistence.Postgres/PostgresProjectRepository.cs
sourceHash: CDE5DE0F673BCBF435B5ACC3DD65EBC0E0F7602B9ED3BE0A56CE86567E24B2C1
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.PostgresProjectRepository
    kind: class
    line: 7
    column: 14
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.PostgresProjectRepository.GetProjectAsync
    kind: method
    line: 9
    column: 33
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.PostgresProjectRepository.GetProjectsAsync
    kind: method
    line: 25
    column: 38
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.PostgresProjectRepository.AddProjectAsync
    kind: method
    line: 33
    column: 23
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.PostgresProjectRepository.UpdateProjectAsync
    kind: method
    line: 39
    column: 23
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.PostgresProjectRepository.AddScalarAndTimeSeriesValuesAsync
    kind: method
    line: 59
    column: 24
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.PostgresProjectRepository.RemoveExistingScalarAndTimeSeriesValuesAsync
    kind: method
    line: 98
    column: 24
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.PostgresProjectRepository.DeleteProjectAsync
    kind: method
    line: 131
    column: 23
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : This class manages database operations for projects and their associated scalar and time series values.
**Important Inputs** : Project data (Name, Code, etc.) and a database context.
**Important Outputs** : Project data retrieved from the database.
**Side Effects** : Modifies existing data in the database, removes old values, and saves changes.
**Behavior** : Retrieves, adds, updates, and deletes projects, ensuring scalar and time series values are linked to the correct project.

<!-- codesemantic4llm:summary:1 -->
**Objectif** : Récupérer un projet par ID et utilisateur, avec des sélections de relations.
**Entrées** : `projectId` (ID du projet), `userId` (ID de l'utilisateur), `cancellationToken` (pour l'annulation des opérations).
**Sortie** : Un projet (`Project?`) ou `null` si aucun n'est trouvé.
**Effets secondaires** : Utilisation de `AsNoTracking()` pour éviter les mappages de navigation.

<!-- codesemantic4llm:summary:2 -->
**Objectif** : Récupérer une liste de projets associés à un utilisateur spécifique à partir de la base de données.
**Entrées** : Un identifiant utilisateur (`string userId`) et un token de cancellation (`CancellationToken cancellationToken`).
**Sortie** : Une collection asynchrone d'objets `Project`.
**Effets secondaires** : Aucun effet secondaire direct. Utilise `AsNoTracking()` pour éviter l'acquisition d'une mémoire de contexte, mais n'affecte pas les données récupérées.
**Méthode** : Appelée via un service, elle est probablement utilisée dans une interface pour obtenir des projets d'un utilisateur.

<!-- codesemantic4llm:summary:3 -->
**Purpose**: Adds a project to the database and saves it.
**Inputs**: Project object and cancellation token.
**Outputs**: Confirms project was added and saved successfully.
**Side effects**: Modifies the database and persists the project.
**Behavior**: Uses the database context to handle asynchronous operations.
**No inventions**: Follows standard pattern for adding and saving entities.
**Untrusted data**: XML doc and source code may contain false information.
**No guarantees**: Depends on the database and context availability.
**Synchronous**: Underlying operations are asynchronous.
**CancellationToken**: Used for cancellation during the operation.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Mise à jour d’un projet dans la base de données avec vérification de son existence et mise à jour des données.
**Entrées** : Un objet `Project` et un `CancellationToken`.
**Sorties** : Aucune, mais la base de données est sauvegardée.
**Effets secondaires** : Modifie les données de base de données, peut lancer des processus asynchrones.
**Nommé** : `UpdateProjectAsync`
**Méthode** : `PostgresProjectRepository`
**Contexte** : Appelé depuis un service d’API.
**Fonctionnalités** : Mise à jour du nom et du code, suppression et ajout de valeurs scalaires et de séries en temps réel.
**Gestion des exceptions** : Lancé si le projet n’est pas trouvé.
**Méthode asynchrone** : Utilise `async` et `Task`.

<!-- codesemantic4llm:summary:5 -->
**Pourquoi :** Cette méthode ajoute des valeurs scalaires et des séries temporelles associées à un projet dans une base de données PostgreSQL.
**Entrées :** Un objet `Project` contenant des collections de valeurs scalaires et de séries temporelles.
**Sortie :** Aucune, la base de données est mise à jour en place.
**Effets secondaires :** Modifie les propriétés `ProjectId` des objets et ajoute des données à la base de données.

<!-- codesemantic4llm:summary:6 -->
**Purpose** : This method removes all existing scalar and time series values from the database for a given project.
**Important Inputs** : The project ID and a cancellation token.
**Important Outputs** : No direct output, but it modifies the database.
**Side Effects** : It deletes data from the database and may affect related entities.
**Behavior** : It asynchronously retrieves and removes values using LINQ.
**Relies on** : The `dbContext` context and predefined entity classes.
**No validation** : Assumes the project exists and the database entities are correctly mapped.
**No logging** : Does not include any logging or error handling.
**No return value** : Returns void since it operates on the database directly.
**Assumes** : The entities are properly defined and the database relationships are correct.

<!-- codesemantic4llm:summary:7 -->
**Déclaration** : `DeleteProjectAsync` dans `PostgresProjectRepository`.
**Description** : Cette méthode supprime un projet par son ID depuis la base de données PostgreSQL. Elle récupère le projet, le retire du contexte, et sauvegarde les changements.
**Entrées** : L'ID du projet (`projectId`) et un token de cancellation (`cancellationToken`).
**Sorties** : Elle retourne `Task<T>` et ne renvoie pas de valeur directement.
**Effets secondaires** : Elle modifie la base de données et peut affecter d'autres entités via la relation parent-enfant.
