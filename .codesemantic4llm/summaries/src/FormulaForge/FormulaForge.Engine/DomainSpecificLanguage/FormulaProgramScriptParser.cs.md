---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/DomainSpecificLanguage/FormulaProgramScriptParser.cs
sourceHash: 3A214FBAE73DBF4AAB209793D225FD8FAC5EE3B9F1EE464DDB20B3FC57D4A111
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.FormulaProgramScriptParser
    kind: class
    line: 9
    column: 21
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Cette classe parse des scripts de programme en langage formule pour générer des nœuds d'astre.
**Inputs** : Elle utilise des flux d'analyse pour lire les lignes et les expressions du script.
**Outputs** : Elle produit un tableau d'astres contenant les nœuds parseés.
**Side Effects** : Elle peut générer des exceptions si des lignes invalides sont rencontrées.
**Note** : Les flux d'analyse sont définis dans la méthode `Parse`.
**Important** : Les nœuds sont convertis à partir des types spécifiques de la classe `StatementNode`.
**Untrusted** : Les données provenant des flux d'analyse sont non vérifiées et peuvent contenir des erreurs.
**Purpose** : Elle fournit un moyen d'extraire les structures logiques d'un script formule.
**Outputs** : L'astre est un repère pour les nœuds structurels du script.
**Side Effects** : Elle peut réduire la performance en créant des tableaux d'astres.
