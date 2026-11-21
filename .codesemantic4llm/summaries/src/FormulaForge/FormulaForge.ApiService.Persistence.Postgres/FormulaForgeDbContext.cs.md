---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.ApiService.Persistence.Postgres/FormulaForgeDbContext.cs
sourceHash: 6ACF44FF7A866EBD454BDBC329AC6CFA2D6FE9C9E6C61755540007C0694804B3
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.FormulaForgeDbContext
    kind: class
    line: 8
    column: 14
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.FormulaForgeDbContext.OnModelCreating
    kind: method
    line: 21
    column: 29
---

<!-- codesemantic4llm:summary:0 -->
C# code définit un contexte de base pour une base de données PostgreSQL, en configurant des relations entre entités comme Projects et leurs valeurs scalaires. Il inclut des convertisseurs pour convertir les DateTimeOffset en DateTimeOffset avec UTC.
Le contexte prend en entrée des options de base de données et en sorti des ensembles de données pour plusieurs entités. Il affecte les propriétés des entités en définissant des clés, des relations et des comportements de suppression.
Il n’affecte pas d’effets secondaires visibles, car il ne contient pas de méthodes ou d’appels de gestion d’exception. Les relations sont configurées avec OnDelete(Cascade), ce qui signifie que la suppression de l’entité parent entraîne la suppression de l’entité enfant.

<!-- codesemantic4llm:summary:1 -->
C# code génère des relations de navigation avec des comportements de suppression.
Il convertit automatiquement les propriétés `DateTimeOffset` en `DateTimeOffset` avec UTC.
Des entités comme `Project` ont des relations many-to-one avec des tableaux de valeurs scalaires.
Les entités de séries temporelles ont des propriétés pour stocker les valeurs et les intervalles.
On configure les comportements de suppression avec `OnDelete(DeleteBehavior.Cascade)`.
Les conversions et les relations sont appliquées après la configuration de base.
Les propriétés `Start` et `End` sont obligatoires pour chaque séries temporelles.
Le code ne crée pas de nouveaux objets ou de ressources, mais configure des relations existantes.
Les méthodes affectent l'architecture de base de la base de données.
Le code est utilisé pour initialiser la structure de la base de données.
