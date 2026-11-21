---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Domain.Services/IDataSourcesProvider.cs
sourceHash: C577CFB6BB873B3BFC94537A9F1D7DFC5B8CE3A85167C0566F98203F44F14F1D
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Domain.Services.IDataSourcesProvider
    kind: interface
    line: 7
    column: 18
  - qualifiedName: FormulaForge.Domain.Services.IDataSourcesProvider.GetDecimalTimeSeriesAsync
    kind: method
    line: 9
    column: 32
  - qualifiedName: FormulaForge.Domain.Services.RestApiDataSourcesProvider
    kind: class
    line: 12
    column: 14
  - qualifiedName: FormulaForge.Domain.Services.RestApiDataSourcesProvider.GetDecimalTimeSeriesAsync
    kind: method
    line: 14
    column: 45
---

<!-- codesemantic4llm:summary:0 -->
**Interface `IDataSourcesProvider`**
Defines an asynchronous method to retrieve a `TimeSeries<decimal>` from a specified data source.
**Purpose**
Provides a way to access and retrieve time series data in decimal format through a provider interface.
**Inputs**
- `dataSource`: Specifies the data source to query.
- `cancellationToken`: Enables cancellation of the operation.
**Outputs**
- Returns an `Optional<TimeSeries<decimal>>` to indicate success or failure.
**Side Effects**

<!-- codesemantic4llm:summary:1 -->
Cette méthode récupère une séries de temps decimalisée à partir d'une source de données. Elle prend une spécification de source et un token de cancellation. Elle retourne une séries de temps ou `null` si une erreur survient.

<!-- codesemantic4llm:summary:2 -->
**Déclaration :** `FormulaForge.Domain.Services.RestApiDataSourcesProvider` est une classe qui fournit des sources de données pour une API.
**Entrées :** Un ` IHttpClientFactory` pour créer un client HTTP.
**Sorties :** Une séries de temps `TimeSeries<decimal>` avec des valeurs calculées.
**Effets secondaires :** Création d'une instance d'`IHttpClientFactory`, mais l'utilisation effective est bloquée par une exception.
**Note :** La méthode retourne une séries de temps avec une logique de calcul, mais elle ne fait pas de requête réelle.
**Précautions :** Les données de sortie sont stockées dans une structure, mais elles ne sont pas mises à jour ou utilisées ailleurs.
**Utilisation :** Peut être injecté dans un composant pour gérer les requêtes aux API.
**Dépendances :** Re suppose la présence d'une implémentation de `IDataSourcesProvider`.
**Options :** Accepte un `CancellationToken` pour la flexibilité.
**Inactivité :** La méthode `GetDecimalTimeSeriesAsync` est désactivée par une exception.

<!-- codesemantic4llm:summary:3 -->
Cette méthode crée un client HTTP à partir d'un spec de source, puis construit une série temporelle de décimales avec une plage de dates et une opération de calcul. Elle retourne une instance de `TimeSeries<decimal>` ou `null` si une exception survient. Elle ne fait aucun effet secondaire et ne produit aucun résultat visible.
