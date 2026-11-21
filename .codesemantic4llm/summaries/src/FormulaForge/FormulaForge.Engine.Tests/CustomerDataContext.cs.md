---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/CustomerDataContext.cs
sourceHash: ACA2D67B39702C4AA4CB19DE0CD193BB9919E36A42EACBA4E9B8CAA8A8F85845
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.CustomerDataContext
    kind: class
    line: 8
    column: 21
  - qualifiedName: FormulaForge.Engine.Tests.CustomerDslContext
    kind: class
    line: 19
    column: 21
  - qualifiedName: FormulaForge.Engine.Tests.CustomerDslContext.CustomerDslContext
    kind: constructor
    line: 24
    column: 12
  - qualifiedName: FormulaForge.Engine.Tests.CustomerDslContext.CreateGlobalScope
    kind: method
    line: 31
    column: 30
  - qualifiedName: FormulaForge.Engine.Tests.CustomerDslContext.TryGetSeries
    kind: method
    line: 55
    column: 26
---

<!-- codesemantic4llm:summary:0 -->
**Purpose:**
This class represents a customer context with data properties for balance, assets, and change rate.
**Inputs/Outputs:**
It accepts customer ID, account balance, and assets count, and returns a change rate time series.
**Side Effects:**
It initializes the `ChangeRate` property with a default empty time series.
**Behavior:**
It is sealed and contains required properties, ensuring data integrity.
**Note:**
It is not a navigation index, but rather a container for customer-related data.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Ce classe affiche des données de compte à travers des périodes en tant que séries dans un contexte de test.
**Inputs** : Un instance de `CustomerDataContext` pour obtenir les données.
**Outputs** : Des séries de type `TimeSeries<decimal>` pour les balances, taux de changement et compte d'actifs.
**Side Effects** : Modifie le contexte global en ajoutant des variables avec des valeurs préconfigurées.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Ce constructeur initialise le contexte CustomerDslContext en utilisant le contexte CustomerDataContext.
**Inputs** : Un object CustomerDataContext.
**Outputs** : Un objet CustomerDslContext initialisé.
**Side Effects** : Affecte l'état de l'objet CustomerDslContext en lui assignant le contexte CustomerDataContext.

<!-- codesemantic4llm:summary:3 -->
Cette méthode crée un `Scope` pour un test global, en initialisant des variables avec des valeurs préconfigurées. Elle extrait des données de `_c` et les convertit en types spécifiques. Les résultats sont stockés dans un dictionnaire avec des clés comme `"current_month"`.
Elle ne produit pas d'effets directs sur l'environnement, mais configure des contextes pour la navigation de code. Les entrées sont les propriétés de `_c` et les sorties sont des instances de `RuntimeVariableValue` avec des valeurs initiales.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Cette méthode tente d'obtenir une série de données associée à un nom donné.
**Entrées** : Le nom de la série (string) et un paramètre sortant (out TimeSeries<decimal> series).
**Sorties** : La série de données (TimeSeries<decimal>) si le nom est valide, null si le nom est invalide.
**Effets secondaires** : Le paramètre sortant est initialisé à null, et la méthode retourne une booléenne indiquant le succès.
**Note** : La logique de récupération est illustrée dans le code mais n'est pas complète.
