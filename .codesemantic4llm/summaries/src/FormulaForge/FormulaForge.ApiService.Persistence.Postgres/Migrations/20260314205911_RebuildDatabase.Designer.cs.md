---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.ApiService.Persistence.Postgres/Migrations/20260314205911_RebuildDatabase.Designer.cs
sourceHash: 072D09E598F1A3C4DC1A232C197037BC6A8839702FB5121F24B0C1DFF5025A12
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.RebuildDatabase
    kind: class
    line: 16
    column: 19
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.RebuildDatabase.BuildTargetModel
    kind: method
    line: 19
    column: 33
---

<!-- codesemantic4llm:summary:0 -->
Ce code définit des modèles de données pour une base de données PostgreSQL avec plusieurs entités liées aux projets. Il configure des propriétés comme les types de données, les clés primaires, et les relations avec des projets. Les annotations spécifient les comportements de navigation et les comportements de suppression. Les entités sont mappées à des tables avec des noms correspondants.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Builds a database schema for a system involving entities like `BooleanScalarValue`, `BooleanTimeSeries`, `DecimalScalarValue`, and `Project`, along with navigation relationships.
**Inputs** : The `ModelBuilder` object, which represents the model structure.
**Outputs** : A configured `NpgsqlModelBuilder` that generates the corresponding PostgreSQL database table definitions.
**Side Effects** : Modifies the model builder to include specific annotations, set default columns, and define navigation properties.
