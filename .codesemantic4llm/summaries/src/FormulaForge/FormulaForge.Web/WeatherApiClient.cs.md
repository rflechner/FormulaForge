---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Web/WeatherApiClient.cs
sourceHash: 16D38C323C6502EB2D40B41C20E82DC48F53C73C783169B84C11B67B7184F961
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Web.WeatherApiClient
    kind: class
    line: 3
    column: 14
  - qualifiedName: FormulaForge.Web.WeatherApiClient.GetWeatherAsync
    kind: method
    line: 5
    column: 42
  - qualifiedName: FormulaForge.Web.WeatherForecast
    kind: record
    line: 29
    column: 15
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Cette classe encapsule une logique d'acquisition de prévisions météorologiques asynchrones via un client HTTP.
**Entrées** : Elle accepte un `HttpClient` et des paramètres de quantité maximum de résultats.
**Sorties** : Elle retourne un tableau de prévisions météorologiques, limité par le paramètre `maxItems`.
**Effets secondaires** : Elle modifie la liste `forecasts` et peut affecter la mémoire par l'ajout de résultats.
**Note** : Le comportement exact dépend des données retournées par l'endpoint et de la logique de traitement dans la classe `WeatherForecast`.

<!-- codesemantic4llm:summary:1 -->
**Purpose:** This method retrieves weather forecasts asynchronously from an API and returns up to `maxItems` forecasts.
**Inputs:** Takes an integer `maxItems` (default 10) and a `CancellationToken`.
**Outputs:** Returns an array of `WeatherForecast` objects or an empty array if the request fails or returns fewer items.
**Side Effects:** Modifies the `forecasts` list in-place and may throw exceptions if the API call fails or returns unexpected data.

<!-- codesemantic4llm:summary:2 -->
**Purpose**: This C# record represents a weather forecast with a date, temperature in Celsius, and a summary. It also provides a computed temperature in Fahrenheit.
**Inputs**: `Date`, `TemperatureC`, and `Summary`.
**Outputs**: A `WeatherForecast` record with `TemperatureF` as a computed value.
**Side effects**: None; the record is immutable.
