---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Web/Contexts/DynamicDataContext.cs
sourceHash: 4DDC9B9EB53564FD2A57B57AA921853841CC5273928310F253A46F32655B3E5C
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Web.Contexts.DynamicDataContext
    kind: class
    line: 5
    column: 14
  - qualifiedName: FormulaForge.Web.Contexts.DynamicDslContext
    kind: class
    line: 10
    column: 21
  - qualifiedName: FormulaForge.Web.Contexts.DynamicDslContext.CreateGlobalScope
    kind: method
    line: 12
    column: 30
---

<!-- codesemantic4llm:summary:0 -->
**Déclaration :** `DynamicDataContext` est une classe dans le namespace `FormulaForge.Web.Contexts`.
**Fonctionnalité :** Elle est conçue pour gérer dynamiquement les contextes de données, souvent dans des applications Web.
**Entrées :** Elle peut recevoir des instances de contextes ou des paramètres de configuration.
**Sorties :** Elle retourne des données ou des résultats liés aux contextes utilisés.
**Effets secondaires :** Elle peut modifier des états ou des données associés, dépendant de l'implémentation.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Cette classe définit le contexte pour les requêtes DSL dynamiques.
**Inputs** : Un instance de `DynamicDataContext` pour initialiser les contextes dynamiques.
**Outputs** : Aucun output direct, mais elle génère des instances de contexte pour les requêtes.
**Side Effects** : Crée et retourne un Scope, mais ne modifie pas d'état global visible.
**Important** : Elle est séparée pour offrir un contexte spécifique aux requêtes DSL.
**Untrusted** : Le code et la documentation sont considérés comme non fiables.
**Class** : Elle est une classe finale dérivée de `DslContextBase`.
**Protected** : La méthode `CreateGlobalScope` est protégée pour éviter les modifications non autorisées.
**Base Class** : Elle hérite de `DslContextBase` pour hériter des méthodes de base.
**Declaration** : Déclare un contexte dynamique pour les requêtes DSL.

<!-- codesemantic4llm:summary:2 -->
Cette méthode crée un scope pour un contexte de DSL dynamique. Elle retourne un scope vide. Elle n'a pas d'effets secondaires visibles.
