---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Web/Components/Pages/EditProject.razor.cs
sourceHash: 1DEC49B86489A0CB7DBA5915C38191D940B9CD71F666454B498E02DE43A9901C
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject
    kind: class
    line: 15
    column: 22
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.OnInitializedAsync
    kind: method
    line: 54
    column: 35
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.OnAfterRenderAsync
    kind: method
    line: 90
    column: 35
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.TrySetEditorValue
    kind: method
    line: 102
    column: 24
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.InitializeContext
    kind: method
    line: 108
    column: 18
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.ClearContext
    kind: method
    line: 146
    column: 18
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.LoadInputs
    kind: method
    line: 153
    column: 18
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.DisplayRows
    kind: method
    line: 186
    column: 24
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.RunScript
    kind: method
    line: 205
    column: 24
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.AddLog
    kind: method
    line: 273
    column: 18
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.SaveProject
    kind: method
    line: 275
    column: 24
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.OnEditorDidInit
    kind: method
    line: 366
    column: 24
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.MapSeries
    kind: method
    line: 373
    column: 38
  - qualifiedName: FormulaForge.Web.Components.Pages.EditProject.MapScalar
    kind: method
    line: 378
    column: 28
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Ce code représente une page C# d'édition de projet, qui affiche et permet d'exécuter des scripts FormulaForge avec des données temporielles et scalaires.
**Inputs/Outputs** : Accepte un `Guid?` pour identifier le projet. Produit un `CodeRunResult`, qui indique si le script a réussi ou non. Affiche des logs dans le console.
**Side Effects** : Modifie le projet et ses données, initialise l'éditeur, gère les erreurs et effectue des tâches asynchrones.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Ce code initialise une page de modification d'un projet, récupère son utilisateur et ses données, et affiche les entrées pour la gestion temporelle.
**Inputs** : L'ID du projet, l'ID de l'utilisateur, les valeurs des séries temporelles.
**Outputs** : Le projet est chargé, les dates sont sélectionnées et affichées.
**Side Effects** : L'ID du projet est initialisé, les entrées sont chargées et affichées, et l'éditeur peut être mis à jour avec la valeur du code du projet.

<!-- codesemantic4llm:summary:2 -->
**Purpose**: This method updates the project editor after rendering, checking for a dark mode class and setting the editor value if a project is loaded.
**Inputs/Outputs**: It reads a JavaScript value to determine if dark mode is active and writes the project code to the editor.
**Side Effects**: It modifies the DOM by adding a class if dark mode is active and may update the UI state.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Cette méthode tente de définir la valeur d'un éditeur avec un code fourni.
**Entrées** : Un string contenant le code à utiliser.
**Sorties** : Aucune valeur renvoyée, mais peut modifier l'éditeur.
**Effets secondaires** : Affiche les erreurs dans la console, peut modifier l'éditeur.
**Méthode** : Utilise une tentative asynchrone pour éviter les blocs d'attente.
**Type** : Méthode `private async Task` pour une utilisation internationale.
**Dépendance** : L'éditeur `_editor!` est utilisé, donc il doit être initialisé.
**Gestion d'exception** : Catches et affiche les erreurs, mais ne les traite pas.
**Utilisation** : Souvent appelée dans des contextes de navigation ou de mise à jour.
**Caveat** : La dépendance sur `_editor!` suppose qu'il est valide et initialisé.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Initialise variables in a DSL context for a project, including Boolean, Integer, Decimal scalars and time series.
**Inputs** : A `Project` object containing scalar and time series values.
**Outputs** : Variables added to the global scope for use in code navigation.
**Side Effects** : Modifies the DSL context's global scope by adding variables, which can affect subsequent code execution.

<!-- codesemantic4llm:summary:5 -->
**Purpose**: The `ClearContext` method resets the database context by clearing its functions, variables, and built-in variables.
**Important inputs/outputs**: It does not accept any inputs and does not return any values.
**Side effects**: It modifies the internal state of the database context, potentially affecting ongoing operations.
**Behavior**: It is designed to remove data from the context, which may be useful during testing or data reset scenarios.
**Untrusted data**: XML documentation and source code should not be relied upon for accurate behavior.

<!-- codesemantic4llm:summary:6 -->
**Objectif :** Charger des données de séries temporelles et scalaires dans un composant d'éditeur de projet.
**Entrées :** Aucune entrée explicite, mais utilise des variables globales du contexte de la DLL.
**Sorties :** Ajoute des séries temporelles (`balance` et `change_rate`) et des scalaires (`x`) à `_inputsSeries` et `_inputsScalars`.
**Effets secondaires :** Modifie les variables globales du contexte, et appelle `StateHasChanged()` pour signaler une modification de l'état.

<!-- codesemantic4llm:summary:7 -->
**Purpose**: This method updates the display of variables in a computation grid by adding their values to the output series or scalars, depending on their type.
**Inputs**: The method receives a `bool` parameter `clear` which determines whether to reset the output data.
**Outputs**: It adds data to `_outputsSeries` and `_outputsScalars` based on the variable types.
**Side Effects**: It modifies the output data structures and calls `StateHasChanged()` to notify the UI of the update.

<!-- codesemantic4llm:summary:8 -->
**Objectif** : Exécute du code JavaScript à partir d'un éditeur de script et affiche les résultats.
**Entrées** : Le contexte de DSL, les inputs de l'éditeur, et le code à exécuter.
**Sorties** : Les logs d'information, d'erreur et de succès, ainsi que le nombre de variables produites.
**Effets secondaires** : Met à jour l'interface avec les résultats, met en évidence les erreurs, et synchronise l'état avec l'éditeur.

<!-- codesemantic4llm:summary:9 -->
**Purpose** : Ajoute un journalisation avec un niveau et un message.
**Input** : Niveau de journalisation (ex: "Info") et message.
**Output** : Aucun, mais ajoute à la liste `_consoleLogs`.
**Side effect** : Modifie la liste `_consoleLogs` en ajoutant des éléments.
**Behavior** : Utilise le constructeur pour créer un tuple et l'ajoute.
**Assumption** : `_consoleLogs` est une liste de tuples ou un type similaire.
**No code navigation** : L'indice ne pointe pas vers d'autres méthodes ou classes.
**Untrusted data** : Ne dépend pas de documentation ou de sources externes.
**Simple** : Une méthode courte et claire, sans boucles ou complexité.
**Language** : C# (méthode privée avec déclaration de type).

<!-- codesemantic4llm:summary:10 -->
**Objectif** : Permettre de sauvegarder un projet avec ses valeurs scalaires et séries temporelles.
**Entrées** : Le projet, l'éditeur, les variables globales du contexte de base de données.
**Sorties** : Le projet mis à jour, un message d'erreur si le nom est vide.
**Effets secondaires** : Création de nouveaux projets, mise à jour existants, enregistrement de la date de sauvegarde.
Ce code vérifie les noms de projet, récupère les données de l'éditeur, nettoie les collections, et ajoute les valeurs scalaires et séries temporelles. Il génère un ID de projet si nécessaire et enregistre la date de modification.

<!-- codesemantic4llm:summary:11 -->
Cette méthode initialise l'éditeur d'un projet. Elle vérifie si l'éditeur est null, puis met à jour la propriété `_ideInitialized` et tente de configurer la valeur de l'éditeur avec le code du projet. Elle ne modifie pas directement d'objets externes.

<!-- codesemantic4llm:summary:12 -->
Cette méthode convertit les valeurs d'une série de temps en un dictionnaire avec des indices et des valeurs mappées. Elle prend une valeur de série `RuntimeVariableValue.RuntimeTimeSeriesValue` comme entrée.
Elle produit un dictionnaire où chaque clé est l'index de la série, et la valeur est le type `MapScalar`.
Aucun effet secondaire est observé, car elle ne modifie pas l'entrée ou la sortie.

<!-- codesemantic4llm:summary:13 -->
**Purpose** : Convert scalar values to their numeric representation.
**Inputs** : ScalarValueNode containing boolean, decimal, or integer values.
**Outputs** : Decimal or integer value, or exception.
**Side effects** : Throwing an ArgumentOutOfRangeException on invalid types.
