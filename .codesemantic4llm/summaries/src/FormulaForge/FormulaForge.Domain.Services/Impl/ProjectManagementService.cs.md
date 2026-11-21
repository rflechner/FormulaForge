---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Domain.Services/Impl/ProjectManagementService.cs
sourceHash: CC0AB20D07CB8F2EEBBE7CFC0A19ADFFF89FC0F0F1359A29B311B9DBDAA01FCA
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Domain.Services.Impl.ProjectManagementService
    kind: class
    line: 6
    column: 14
  - qualifiedName: FormulaForge.Domain.Services.Impl.ProjectManagementService.GetProjectAsync
    kind: method
    line: 8
    column: 27
  - qualifiedName: FormulaForge.Domain.Services.Impl.ProjectManagementService.GetProjectsAsync
    kind: method
    line: 13
    column: 38
  - qualifiedName: FormulaForge.Domain.Services.Impl.ProjectManagementService.CreateProjectAsync
    kind: method
    line: 18
    column: 17
  - qualifiedName: FormulaForge.Domain.Services.Impl.ProjectManagementService.UpdateProjectAsync
    kind: method
    line: 23
    column: 17
  - qualifiedName: FormulaForge.Domain.Services.Impl.ProjectManagementService.DeleteProjectAsync
    kind: method
    line: 28
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Cette classe expose des méthodes pour gérer les projets, en appelant une interface de dépôt de projets.
**Inputs** : L'ID du projet, l'utilisateur, et un token de cancellation.
**Outputs** : Une tâche renvoyant le projet ou une collection de projets.
**Side Effects** : Les méthodes modifient le dépôt de projets et peuvent modifier les données.
**Behavior** : Les méthodes retourneront la valeur du dépôt ou null si le projet n'est pas trouvé.
**Untrusted Sources** : XML de documentation et code source sont considérés comme non fiables.

<!-- codesemantic4llm:summary:1 -->
This method retrieves a project by ID and user ID. It calls the project repository's `GetProjectAsync` method. The output is a `Project?` type, indicating the project if found. It uses a cancellation token for asynchronous operations.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Retreiver les projets associés à un utilisateur en utilisant une répositoryire.
**Inputs** : Utilisateur (ID), token de cancellation.
**Outputs** : Séquence d'objets `Project`.
**Side Effects** : Aucun, sauf l'appel à la méthode de répositoryire.
**Behavior** : Appelle `GetProjectsAsync` de `projectRepository` avec les paramètres fournis.
**Untrusted Data** : XML et code source ne sont pas vérifiés pour garantir leur exactitude.
**No Invented Behavior** : Ne suppose aucun comportement supplémentaire.
**Short Summary** : Cette méthode récupère les projets d'un utilisateur à partir du répositoryire, avec un token de cancellation.
**C# Method** : Utilise `IAsyncEnumerable` pour la navigation asynchrone.
**No Side Effects** : Ne modifie pas l'état de l'application.

<!-- codesemantic4llm:summary:3 -->
Cette méthode crée un projet en appelant une fonction de la répositoryire. Elle prend un projet et un token de cancellation. Elle retourne une tâche représentant le résultat de l'opération.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : This method updates a project in the database asynchronously.
**Inputs** : A `Project` object and a `CancellationToken`.
**Output** : A `Task` indicating the asynchronous update operation.
**Side Effects** : Modifies the database and potentially triggers related business logic.
**Behavior** : Delegate to `ProjectRepository.UpdateProjectAsync` for actual database updates.
**Untrusted Data** : XML documentation and source code are considered unreliable.
**Simplified** : It acts as a wrapper for an async database update method.
**No Behavior** : The method itself does not perform any operations.
**Default** : Uses `default(CancellationToken)` if no value is provided.
**Reliable** : Assumes the repository method is trusted and correctly implemented.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Cette méthode supprime un projet depuis le dépôt.
**Inputs** : L'ID du projet et un token de cancellation.
**Output** : Une tâche représentant la suppression asynchrone.
**Side Effects** : Le projet est effacé du système, et des ressources sont libérées.
**Behavior** : Utilise la méthode `DeleteProjectAsync` de `projectRepository`.
**Security** : Le token de cancellation permet de gérer les interruptions.
**Reliability** : L'exception de suppression est retournée sous forme de `Task`.
**Trust** : Les déclarations et la documentation sont considérées comme non fiables.
**Usage** : Appelé depuis un service ou un contrôle d'orientation.
**Design** : Implémentation simple basée sur une abstraction.
