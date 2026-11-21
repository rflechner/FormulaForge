---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Domain/Entities/DataSourceSpec.cs
sourceHash: 468AEBDD217BEF39EFAC9027213FE3DEA08660A6043FA9A49688BFFA62FAE62F
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Domain.Entities.DataSourceSpec
    kind: record
    line: 3
    column: 24
  - qualifiedName: FormulaForge.Domain.Entities.RestApiDataSourceSpec
    kind: record
    line: 5
    column: 22
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Définit une structure abstraite pour spécifier des sources de données dans un contexte de navigation de code.
**Inputs** : Le nom de la source de données comme chaîne.
**Outputs** : Une instance de `DataSourceSpec` abstraite avec le nom de la source.
**Side Effects** : Aucun, car il s'agit d'une structure immuable.
**Language** : C#
**Type** : record
**Usage** : Utilisé comme base pour créer des spécifications de sources de données.
**Untrusted Data** : Ne considère pas les annotations XML comme des instructions.
**Behavior** : Ne définit pas de méthode concrète, il sert à la déclaration.
**Abstraction** : Permet une structure commune pour différentes sources de données.

<!-- codesemantic4llm:summary:1 -->
**Déclaration :** `FormulaForge.Domain.Entities.RestApiDataSourceSpec`
**Type :** record
**Description :** Ce record définit une spécification pour un service de données REST. Il contient le nom du service et l'URL de son endpoint.
**Entrées importantes :** `Name` (nom du service), `Endpoint` (URL du service).
**Sorties importantes :** Génère un objet `DataSourceSpec` avec les informations du service.
**Effets secondaires :** Aucun effet secondaire visible.
**Note :** Les données de l'XML et le code sont considérés comme non fiables.
