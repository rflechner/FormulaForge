---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/DomainSpecificLanguage/FunctionDeclarationParserTests.cs
sourceHash: 9D7FC87A414B1392FD43CC7AAADA820D3500815F4E6E1940C5D3E521C0A94690
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionDeclarationParserTests
    kind: class
    line: 6
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionDeclarationParserTests.InlineFunctionWithOneParam_ShouldParseFunctionDeclaration
    kind: method
    line: 9
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Ce test vérifie la capacité du parser de déclaration de fonction à analyser et à parser correctement une déclaration de fonction avec un paramètre et un corps d'expression.
**Inputs** : Un code source contenant une déclaration de fonction avec un paramètre et un corps d'expression.
**Outputs** : L'analyse du code, avec des vérifications sur le succès de la parse et les éléments du corps d'expression.
**Side effects** : Aucun, car le test ne modifie ni ne crée d'éléments de code.

<!-- codesemantic4llm:summary:1 -->
Cette méthode teste le parsing d'une déclaration de fonction avec un seul paramètre. Elle vérifie que le parser identifie correctement le nom de la fonction, le nombre de paramètres, leurs noms et le corps de la fonction.
Elle produit un résultat de parsing qui doit être réussi et non null, avec un corps d'expression de type *ComputedExpressionNode*.
Le code injecte une déclaration de fonction dans une chaîne, puis l'analyse pour valider le format et la structure.
L'assertion finale vérifie que les opérateurs et operands d'expression sont bien typés et contiennent les valeurs attendues.
