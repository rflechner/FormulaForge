---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/DomainSpecificLanguage/FunctionsNodesParser.cs
sourceHash: FC9F27A13B0911CE2A5B3344EB3B38633B2E182BA078260D49CF698A7082FE0C
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.FunctionsNodesParser
    kind: class
    line: 9
    column: 14
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Ce code parse les expressions de fonction et les signatures de fonctions dans un langage spécifique, en utilisant des parsers de texte et de données pour extraire des informations comme les noms de fonctions, leurs paramètres et expressions.
**Inputs** : Des chaînes de texte représentant des fonctions, avec des noms, des parenthèses et des paramètres.
**Outputs** : Des objets comme `FunctionCallExpressionNode` et `FunctionSignatureExpressionNode` qui structurent les informations extraites.
**Side Effects** : Aucun effet secondaire significatif, mais les paramètres sont utilisés pour construire des expressions de fonction et des signatures.
