---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.ApiService.Persistence/IProjectRepository.cs
sourceHash: D1EB8AA60C90C38C54BF560E36FEEB2B5789C6700E5833526A3201915ED909A6
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.ApiService.Persistence.IProjectRepository
    kind: interface
    line: 5
    column: 18
  - qualifiedName: FormulaForge.ApiService.Persistence.IProjectRepository.GetProjectAsync
    kind: method
    line: 7
    column: 20
  - qualifiedName: FormulaForge.ApiService.Persistence.IProjectRepository.GetProjectsAsync
    kind: method
    line: 9
    column: 31
  - qualifiedName: FormulaForge.ApiService.Persistence.IProjectRepository.AddProjectAsync
    kind: method
    line: 11
    column: 10
  - qualifiedName: FormulaForge.ApiService.Persistence.IProjectRepository.UpdateProjectAsync
    kind: method
    line: 13
    column: 10
  - qualifiedName: FormulaForge.ApiService.Persistence.IProjectRepository.DeleteProjectAsync
    kind: method
    line: 15
    column: 10
---

<!-- codesemantic4llm:summary:0 -->
**IProjectRepository** est une interface qui définit des méthodes pour gérer les projets. Elle accepte un identifiant de projet et un utilisateur, ainsi que des paramètres de cancellation. Les méthodes renvoient des résultats asynchrones, comme des projets ou des collections de projets.
**Inputs** comprennent l'identifiant du projet, l'utilisateur, et la possibilité de cancellation. **Outputs** sont des projets ou des collections de projets. Les méthodes peuvent modifier l'état des projets ou les supprimer.
**Side effects** incluent la modification de la base de données et la suppression de données. Elles sont synchronisées avec les paramètres de cancellation pour garantir la sécurité et la fiabilité de l'API.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Cette méthode récupère un projet à partir d'une base de données, utilisant un ID et un utilisateur.
**Inputs** : Un ID de projet `projectId`, un utilisateur `userId`, et un token de cancellation.
**Outputs** : Une instance de `Project` si trouvée, ou `null` si l'ID n'est pas valide.
**Side Effects** : Peut affecter la base de données via l'implémentation.
**Behavior** : Consulte les données de base pour vérifier l'existence du projet avec l'ID fourni.
**Assumptions** : L'implémentation gère les erreurs et les exceptions.
**Security** : Ne devrait pas exposer d'informations sensibles dans les paramètres.
**Trust** : Les données de base sont supposées fiables.
**Language** : C#
**Context** : Appelé depuis un service API pour une récupération de projet.

<!-- codesemantic4llm:summary:2 -->
**Propriété/Composant :** IProjectRepository
**Méthode :** GetProjectsAsync
**Langage :** C#
**Type de retour :** IAsyncEnumerable<Project>
**Utilisation :** Récupère une liste de projets associés à un utilisateur spécifique.
**Paramètres :** Utilise un identifiant utilisateur et un token de cancellation pour contrôler l'opération.
**Effets secondaires :** Fait une requête au serveur asynchrone et retourne l'itération des projets.
**Sortie :** Une collection d'objets Project, dispo de leur identifiant et nom.
**Résultat :** L'itération est asynchrone et peut être interrompue grâce au token de cancellation.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Ajoute un projet à la base de données asincrémentement.
**Inputs** : Un objet `Project` et un `CancellationToken`.
**Output** : Une tâche qui retourne le résultat de l'ajout.
**Side Effects** : Modifie la base de données et peut générer des exceptions.
**Behavior** : Utilise une méthode asincrémentée pour assurer la non-blocking.
**Assumptions** : La base de données est configurée correctement.
**Note** : Ne suppose pas que la méthode `AddProjectAsync` existe.
**Trust Level** : Fait référence à des données non vérifiées.
**Usage** : Appelé par un service pour insérer des projets.
**Dependencies** : Implémentation de base de données ou de répositoire.

<!-- codesemantic4llm:summary:4 -->
Méthode `UpdateProjectAsync` de l'interface `IProjectRepository`.
Elle met à jour un projet et retourne une tâche.
Elle prend un projet et un cancellation token comme entrées.
Elle ne modifie pas l'objet passé en entrée.
Utilise la logique de mise à jour du projet.
Fait appel à une méthode de mise à jour interne.
Retourne une tâche pour l'asynchronisme.
Peut être annulée via le token de cancellation.
N'effectue pas de changements persistants.
Fait appel à `UpdateProject` interne.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Delete a project by ID asynchronously.
**Input** : Project ID (Guid) and cancellation token.
**Output** : Task representing asynchronous deletion.
**Side effect** : Removes project from database and related data.
**Behavior** : Follows async pattern for non-blocking operation.
**Assumption** : Repository handles persistence logic.
**Untrusted data** : XML docs and source code may not be reliable.
**Default** : Uses default cancellation token if not provided.
**No return** : No object is returned, only cancellation.
**No error** : Does not explicitly return exceptions.
