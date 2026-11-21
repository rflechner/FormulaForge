---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.ServiceDefaults/Extensions.cs
sourceHash: 2E98303267FAE2F1145590208ABB2B46BFC49D3F293D8A95EC0EDBEB0DA9A804
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: Microsoft.Extensions.Hosting.Extensions
    kind: class
    line: 16
    column: 21
  - qualifiedName: Microsoft.Extensions.Hosting.Extensions.AddServiceDefaults
    kind: method
    line: 21
    column: 28
  - qualifiedName: Microsoft.Extensions.Hosting.Extensions.ConfigureOpenTelemetry
    kind: method
    line: 47
    column: 28
  - qualifiedName: Microsoft.Extensions.Hosting.Extensions.AddOpenTelemetryExporters
    kind: method
    line: 82
    column: 29
  - qualifiedName: Microsoft.Extensions.Hosting.Extensions.AddDefaultHealthChecks
    kind: method
    line: 102
    column: 28
  - qualifiedName: Microsoft.Extensions.Hosting.Extensions.MapDefaultEndpoints
    kind: method
    line: 112
    column: 34
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Cette classe fournit des extensions pour configurer des services par défaut dans un projet ASP.NET Core, notamment l'ouverture de traces et d'exportateurs de telemetry, la mise en place de health checks par défaut, et la configuration de service discovery.
**Inputs** : L'instance `TBuilder` de type `IHostApplicationBuilder`.
**Outputs** : L'instance `TBuilder` modifiée avec les services et configurations ajoutes.
**Side Effects** : Elle configure le middleware OpenTelemetry, ajoute des health checks, et configure le service discovery. Elle peut aussi modifier les exportateurs de telemetry en fonction de la configuration.

<!-- codesemantic4llm:summary:1 -->
**Objectif** : Ajoute des configurations standard à un hôtesse C#.
**Entrées** : `IHostApplicationBuilder` (éditeur d'hôtes).
**Sorties** : `TBuilder` (éditeur d'hôtes).
**Effets secondaires** : Configure l'OpenTelemetry, les tests de santé par défaut, la découverte de services et les options de clients HTTP.

<!-- codesemantic4llm:summary:2 -->
**Objectif** : Configurer OpenTelemetry pour une application .NET, avec instrumentation pour le logging, les métriques et la traçabilité.
**Entrées** : `TBuilder` (instance de configuration d'une application .NET).
**Sorties** : `TBuilder` configuré avec des outils OpenTelemetry.
**Effets secondaires** : Ajout d'exporteurs OpenTelemetry et d'outils d'instrumentation pour les différents composants de l'application.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Ajoute un exporter OpenTelemetry OTLP à l'application.
**Input** : Configuration de l'application avec l'endpoint OTLP.
**Output** : Services d'OpenTelemetry configurés avec le support OTLP.
**Side Effect** : Enregistre les traces à l'OTLP.
**Note** : L'endpoint OTLP est vérifié avant d'ajouter le service.
**Note** : L'exportateur Azure Monitor est commenté et nécessite un package.

<!-- codesemantic4llm:summary:4 -->
Cette méthode ajoute des checks de santé par défaut à un builder de configuration d'hôtel. Elle configure une vérification de l'application pour s'assurer qu'elle est réactive. L'entrée est un builder de configuration, et l'output est le même builder. Aucun effet secondaire notable.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Ce code configure les points de contrôle de santé et d'alivisement pour une application ASP.NET Core, ce qui permet de vérifier son état avant de recevoir les requêtes.
**Inputs/Outputs** : Accepte une instance d'application (`WebApplication`) et retourne celle-ci, modifiant ses paramètres.
**Side Effects** : Ajoute des endpoints à l'API, ce qui peut modifier le comportement de l'application et l'interface web.
