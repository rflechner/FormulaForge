---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/Contexts/IDslContext.cs
sourceHash: 25A3D0FCE0C42562131DCC7AC02CEAA1AA2313DD66BD3A224B700E652B7A044C
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Contexts.IDslContext
    kind: interface
    line: 7
    column: 18
  - qualifiedName: FormulaForge.Engine.Contexts.IDslContext.TryGetSeries
    kind: method
    line: 9
    column: 10
  - qualifiedName: FormulaForge.Engine.Contexts.IDslContext.GetScope
    kind: method
    line: 11
    column: 11
  - qualifiedName: FormulaForge.Engine.Contexts.IDslContext.TryGetFunction
    kind: method
    line: 15
    column: 10
  - qualifiedName: FormulaForge.Engine.Contexts.IDslContext.RegisterFunction
    kind: method
    line: 17
    column: 19
---

<!-- codesemantic4llm:summary:0 -->
**IDslContext** est une interface qui permet d'obtenir des contextes pour manipuler le code. Elle expose des méthodes comme `TryGetSeries` pour récupérer une série et `TryGetFunction` pour obtenir une déclaration de fonction. Les méthodes `GetScope` et `GlobalScope` gèrent les portes d'accès. L'inscription de fonctions via `RegisterFunction` affecte la structure du code et les scoping.

<!-- codesemantic4llm:summary:1 -->
Ce méthode tente d'obtenir une série par son nom. Elle retourne une série `TimeSeries<decimal>` si trouvé, sinon `null`. L'entrée est le nom de la série, et l'entrée/sortie est la série elle-même.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Retrieve a scope object for a given function registry ID.
**Input** : Function registry ID.
**Output** : Scope object.
**Side Effects** : None.
**Behavior** : Untrusted source; method is assumed to return a scope.
**XML Documentation** : Not provided.
**Trust Level** : Untrusted; does not verify input.
**Usage** : Used to manage function execution contexts.
**Dependence** : Requires access to function registry.
**Error Handling** : No error checking is visible.

<!-- codesemantic4llm:summary:3 -->
**Proposition :**
Le `TryGetFunction` est une méthode qui essaie de récupérer une déclaration de fonction à partir d'un registre de fonctions.
**Entrées :**
- `id` : Identifiant du registre de fonctions.
- `out function` : Pointeur pour la déclaration de fonction obtenue.
**Sorties :**
- Retourne `true` si la fonction existe, `false` sinon.
- La déclaration de fonction est assignée si elle est trouvée.
**Effets secondaires :**
- Peut modifier le registre de fonctions.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Register a function with a specific ID for execution in the context.
**Inputs** : A function ID and the function's declaration.
**Output** : A code run result indicating success or failure.
**Side Effects** : Modifies the context's function registry, potentially altering available functions.
**Behavior** : Executes the function if registered and the context supports it.
**Untrusted Data** : Relies on external data (XML docs) for function definitions, which may not be reliable.
**No Behavior** : No visible side effects in the given declaration.
**Language** : C#
**Type** : Method
**Context** : FormulaForge.Engine.Contexts.IDslContext
