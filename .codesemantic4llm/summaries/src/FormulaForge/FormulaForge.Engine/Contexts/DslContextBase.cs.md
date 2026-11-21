---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/Contexts/DslContextBase.cs
sourceHash: 8B769FC80365D213D46F71453625B1EB5BBFA263C8D2FB12B64D058D39402CDE
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Contexts.DslContextBase
    kind: class
    line: 7
    column: 23
  - qualifiedName: FormulaForge.Engine.Contexts.DslContextBase.DslContextBase
    kind: constructor
    line: 12
    column: 12
  - qualifiedName: FormulaForge.Engine.Contexts.DslContextBase.TryGetSeries
    kind: method
    line: 19
    column: 25
  - qualifiedName: FormulaForge.Engine.Contexts.DslContextBase.GetScope
    kind: method
    line: 25
    column: 18
  - qualifiedName: FormulaForge.Engine.Contexts.DslContextBase.TryGetFunction
    kind: method
    line: 30
    column: 17
  - qualifiedName: FormulaForge.Engine.Contexts.DslContextBase.RegisterFunction
    kind: method
    line: 35
    column: 26
  - qualifiedName: FormulaForge.Engine.Contexts.DslContextBase.ClearFunctions
    kind: method
    line: 40
    column: 17
  - qualifiedName: FormulaForge.Engine.Contexts.DslContextBase.CreateGlobalScope
    kind: method
    line: 45
    column: 30
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Ce classe abstraite gère le contexte pour les fonctions dans un moteur de DSL, permettant de stocker, récupérer et enregistrer des fonctions.
**Inputs** : Un `FunctionRegistryId` pour identifier une fonction, un `StatementNode` pour la déclaration de la fonction.
**Outputs** : Un `Scope` pour le contexte associé, ou `null` si la fonction n'est pas trouvée.
**Side effects** : Modifie le `_functions` pour stocker une fonction, crée ou récupère un scope global.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Cette constructor initialise le contexte global pour le DSL.
**Input** : Aucun paramètre spécifique.
**Output** : Un instance de DslContextBase avec un scope global.
**Side Effect** : Crée et configure un scope global pour la gestion des ressources dans le contexte.

<!-- codesemantic4llm:summary:2 -->
**Objectif** : Essayer de récupérer une série à partir d’un contexte de DSL.
**Entrées** : Nom de la série.
**Sorties** : Pointeur vers la série (ou null si absent).
**Effets secondaires** : Affecte la valeur de `series` si une série est trouvée.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Cette méthode crée et retourne un objet `Scope` vide.
**Input** : L'identifiant de registre de fonction `scope`.
**Output** : Un objet `Scope` initialisé.
**Side Effects** : Aucun effet secondaire visible.
**Behavior** : Ne fait pas de vérification ou de traitement spécifique.
**Trust** : L'information est considérée comme non fiable.
**Use Case** : Peut être utilisé pour initialiser des contextes dans un registre de fonction.
**Security Note** : Ne peut pas être utilisé pour obtenir des informations sensibles.
**Language** : C#
**XML Doc** : Aucune documentation XML présente.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Ce méthode tente d'obtenir une déclaration de fonction depuis un registre de fonctions.
**Inputs** : L'ID de la fonction et un pointeur pour stocker la déclaration.
**Output** : Une déclaration de fonction si trouvée, null sinon.
**Side Effects** : Modifie le registre des fonctions interne.
**Security** : Ne vérifie pas la validité de l'ID, ne fait pas de vérification de permissions.
**Trust** : Ne peut pas être utilisé pour des actions non autorisées, car les données sont considérées comme non fiables.
**Language** : C#
**Behavior** : Retourne `true` si la fonction existe, `false` autrement.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Cette méthode insère une fonction dans un registre de fonctions, en vérifiant si elle existe déjà.
**Entrées** : Un identifiant de registre de fonction et une déclaration de fonction.
**Sorties** : Un résultat indiquant success ou erreur de surécrivure.
**Effets secondaires** : Modifie le registre de fonctions, peut éventuellement surécrivre une fonction existante.

<!-- codesemantic4llm:summary:6 -->
Ce méthode efface les fonctions stockées dans la collection `_functions` de `DslContextBase`.
Elle ne retourne pas de valeur et n'a pas d'effets secondaires visibles.
Elle est utilisée pour nettoyer les fonctions d'une contexte DSL.
Le comportement exact dépend de la logique du contexte ou de l'implémentation sous-jacente.

<!-- codesemantic4llm:summary:7 -->
**Purpose** : Crée un scope global pour l'environnement de développement de l'application.
**Input** : Aucun paramètre nécessaire.
**Output** : Un objet Scope qui représente le contexte global.
**Side effects** : Instance un contexte global pour l'ensemble de l'application.
**Behavior** : Méthode abstraite qui doit être implémentée par les descendants.
