---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.ApiService/Program.cs
sourceHash: 9D0F3DA3DEA9D8B8BDC0FD4BB359A26F54D3D37C0CEBB909B4A716C8936459CB
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: WeatherForecast
    kind: record
    line: 135
    column: 8
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Ce record représente une prévision météorologique avec une date, une température en Celsius et un résumé. Il inclut une propriété calculée pour la température en Fahrenheit.
**Inputs** : DateOnly, TemperatureC, Summary (optionnel).
**Outputs** : TemperatureF (calculée à partir de TemperatureC).
**Side effects** : Aucun effet secondaire, la valeur de TemperatureF est calculée et accessible uniquement via la propriété.
