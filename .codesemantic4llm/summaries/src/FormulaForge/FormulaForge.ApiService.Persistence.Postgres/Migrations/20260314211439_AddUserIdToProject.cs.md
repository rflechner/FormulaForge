---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.ApiService.Persistence.Postgres/Migrations/20260314211439_AddUserIdToProject.cs
sourceHash: B532382B61799C7401D7FB6D68D027C7B575D96CFB859BE3A9E7AF8C4F4D3E79
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.AddUserIdToProject
    kind: class
    line: 8
    column: 26
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.AddUserIdToProject.Up
    kind: method
    line: 11
    column: 33
  - qualifiedName: FormulaForge.ApiService.Persistence.Postgres.Migrations.AddUserIdToProject.Down
    kind: method
    line: 22
    column: 33
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Cette classe définit une migration PostgreSQL qui ajoute un champ "UserId" dans la table "Projects".
**Inputs** : Aucun paramètre directement visible, mais dépend du contexte de la base de données et des configurations existantes.
**Outputs** : Ajout d'une colonne "UserId" dans la table "Projects" avec un type "text" et une valeur par défaut vide.
**Side Effects** : Modifie la structure de la base de données, créant une colonne si elle n'existe pas et la supprimant lors de la rollback.

<!-- codesemantic4llm:summary:1 -->
Cette méthode ajoute un champs `UserId` à la table `Projects` dans la base de données. Elle prend en charge les migrations PostgreSQL et suppose que l'ID utilisateur est déjà disponible. Elle ne produit aucun résultat visible et ne modifie pas les données existantes.

<!-- codesemantic4llm:summary:2 -->
Cette méthode effectue la suppression de la colonne "UserId" dans la table "Projects" lors de la migration de base de données. Elle est appelée en mode de retrait et reçoit un paramètre de configuration pour la gestion des dépendances. Aucun effet secondaire notable est mentionné.
