---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.ApiService.Persistence.Postgres/Migrations/20260314211439_AddUserIdToProject.Designer.cs
sourceHash: 6CD62B8EA2D5CE5FC307F136C72F8CADB8FE9F6A4AC0FCCA378CA88F3673FB2A
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.AddUserIdToProject
    kind: class
    line: 16
    column: 19
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.AddUserIdToProject.BuildTargetModel
    kind: method
    line: 19
    column: 33
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : This code defines a migration for adding a `UserId` to each project in a PostgreSQL database, ensuring proper relationships are established between projects and associated entities.
**Inputs/Outputs** : It modifies the database schema by adding a `UserId` field to the `Project` table and creates navigation properties in the corresponding entities to maintain relationships.
**Side Effects** : It alters the database structure by adding a new column and updating entity mappings, which can impact existing data and application behavior.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Créer des modèles Entity Framework pour gérer des données de projet et ses valeurs booléennes, décimales et entières, avec des associations et des contraintes de navigation.
**Inputs** : Le contexte de la base de données et les informations de l'entité (propriétés, types, relations).
**Outputs** : Les modèles de données avec les propriétés définies, les relations navigées, et les tableaux de données associés.
**Side Effects** : Active des avertissements de compilation, configure des types de colonnes et définit les comportements de suppression de données.
