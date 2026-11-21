---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Domain.Services/IProjectManagementService.cs
sourceHash: 6C11C3322D0DBFFF71D2755139BAF71C340F8D4C56C0E948E141B64C96AC9CB3
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Domain.Services.IProjectManagementService
    kind: interface
    line: 5
    column: 18
  - qualifiedName: FormulaForge.Domain.Services.IProjectManagementService.GetProjectAsync
    kind: method
    line: 7
    column: 20
  - qualifiedName: FormulaForge.Domain.Services.IProjectManagementService.GetProjectsAsync
    kind: method
    line: 8
    column: 31
  - qualifiedName: FormulaForge.Domain.Services.IProjectManagementService.CreateProjectAsync
    kind: method
    line: 9
    column: 10
  - qualifiedName: FormulaForge.Domain.Services.IProjectManagementService.UpdateProjectAsync
    kind: method
    line: 10
    column: 10
  - qualifiedName: FormulaForge.Domain.Services.IProjectManagementService.DeleteProjectAsync
    kind: method
    line: 11
    column: 10
---

<!-- codesemantic4llm:summary:0 -->
**IProjectManagementService** est une interface qui définit des méthodes pour gérer les projets. Elle accepte des `Guid` et des `string` pour identifier ou filtrer les projets, et utilise des `CancellationToken` pour la gestion asynchrone des opérations. Les méthodes renvoient des résultats comme `Project?`, `IAsyncEnumerable<Project>`, ou des tâches asynchrones.
**Pourquoi :** Elle permet de récupérer, créer, mettre à jour et supprimer des projets.
**Entrées :** `projectId`, `userId`, et `cancellationToken`.
**Sorties :** Réponse du projet ou une collection de projets.
**Effets secondaires :** Modifie l'état des projets dans la base de données.
**Note :** Les méthodes ne sont pas des instructions, mais décrivent l'API disponible.
**Limites :** Ne contient pas de logique d'implémentation, seulement les signatures des méthodes.
**Utilité :** Facilite la navigation entre projets dans une application de gestion de projet.
**Risques :** Les données sont récupérées de manière asynchrone, mais les comportements d'erreur ou de validation ne sont pas spécifiés.
**Autres :** Les méthodes sont synchronisées avec des paramètres de cancellation.

<!-- codesemantic4llm:summary:1 -->
**Purpose**: This method retrieves a project by its ID and user, using asynchronous operations to avoid blocking.
**Inputs**: Guid projectId (unique project identifier), string userId (user identifier), and CancellationToken cancellationToken for cancellation control.
**Output**: A Task<Project?> that resolves to the project object or null if not found.
**Side Effects**: It may perform database queries or other operations, potentially modifying state or data sources.

<!-- codesemantic4llm:summary:2 -->
Cette méthode renvoie une liste d'objets `Project` basée sur l'identifiant utilisateur. Elle est asynchrone et peut être annulée via un `CancellationToken`. Elle utilise `IAsyncEnumerable` pour une navigation fluide. Elle ne produit aucun résultat visible et n'a aucun effet secondaire persistant.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Créer un projet à l'aide d'une interface de service.
**Inputs** : Un objet `Project` et un `CancellationToken`.
**Outputs** : Une tâche qui retourne le résultat de la création du projet.
**Side Effects** : Peut modifier l'état du système ou l'interaction avec des bases de données.
**Behavior** : Effectue la création du projet asynchronement.
**Untrusted Data** : XML et source non vérifiés.
**No Behavior Invented** : Ne suppose pas de détails supplémentaires.
**Short Summary** : Asynchrone création d'un projet avec paramètres et annulation.
**Lines** : 3 (plusieurs détails en 2-4 lignes).
**No Behavior Assumed** : Ne suppose pas de détails supplémentaires.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Update project details asynchronously.
**Input** : Project object and cancellation token.
**Output** : Task indicating success or failure.
**Side effect** : Modifies project data in the database.
**Behavior** : Executes update operation and returns result.
**Async** : Uses asynchronous method for non-blocking execution.
**CancellationToken** : Allows cancellation during operation.
**Database** : Updates project in the underlying database.
**State** : Changes project state in the system.
**Reliability** : Returns task for easy error handling.

<!-- codesemantic4llm:summary:5 -->
**Déclaration** : `DeleteProjectAsync` est une méthode async qui supprime un projet.
**Entrées** : Un objet `Project` et un `CancellationToken`.
**Sorties** : Une `Task` qui retourne la suppression du projet.
**Effets secondaires** : Supprime le projet du système.
**Prérequis** : Le projet doit être validement chargé.
**Exigences** : Une autorisation suffisante pour supprimer le projet.
**Raccourci** : Utilise des méthodes de base de `System.Threading.Tasks` pour le traitement asynchrone.
**Méthode de suppression** : Appelle des méthodes de base pour actualiser les données et nettoyer la base de données.
**Gestion des exceptions** : Détaille les erreurs via l'exception retournée par la méthode.
**Résultat** : Le projet est retiré du système et la base de données est mise à jour.
