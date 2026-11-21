---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.ApiService.Persistence.Postgres/Migrations/20260314205911_RebuildDatabase.cs
sourceHash: 8AF89F3084A0BCEE18D66EAC1386FC64E6281A0C0A7BE89A55014118EEB5016A
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.RebuildDatabase
    kind: class
    line: 9
    column: 26
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.RebuildDatabase.Up
    kind: method
    line: 12
    column: 33
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.RebuildDatabase.Down
    kind: method
    line: 254
    column: 33
---

<!-- codesemantic4llm:summary:0 -->
**Purpose**: This C# class defines a database migration for creating and managing tables for storing scalar and time series data related to projects.
**Inputs/Outputs**: It accepts a `SqlParameter` for database connection and creates tables for `Projects`, various scalar values, and time series entries, with foreign key relationships to the `Projects` table.
**Side Effects**: The migration creates database tables and indexes, and drops them in the `Down` method, ensuring the database schema is consistent with the model.

<!-- codesemantic4llm:summary:1 -->
This C# method creates database tables for storing project-related scalar and time series values, along with entries for each value. It uses Entity Framework's migrations to define schema changes, including primary keys, foreign keys, and indexes. The method expects a `SqlParameterBuilder` object and does not return any value. It may cause side effects such as database schema updates and data insertion if called in a migration context.

<!-- codesemantic4llm:summary:2 -->
Cette méthode `Down` réinitialise la base de données en supprimant les tables définies dans le migration. Elle reçoit un paramètre `SqlParameter` et opère sur une instance de `DbContext`. Les actions principales incluent la suppression de plusieurs tables, ce qui peut entraîner la perte de données si elles ne sont pas correctement gérées.
