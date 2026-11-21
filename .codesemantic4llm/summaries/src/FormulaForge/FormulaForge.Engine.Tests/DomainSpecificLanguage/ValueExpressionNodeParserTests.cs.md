---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/DomainSpecificLanguage/ValueExpressionNodeParserTests.cs
sourceHash: 7B4E8C7C7E1E4E266C1BBDFC4BEA641F4AEB18331594759471A4426B9DF9D266
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ValueExpressionNodeParserTests
    kind: class
    line: 8
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ValueExpressionNodeParserTests.BinaryOperationExpressionParser_ShouldParseAddOperationsOfTreeIntegers
    kind: method
    line: 11
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ValueExpressionNodeParserTests.BinaryOperationExpressionParser_ShouldParseComplexExpressionWithFunctionCall
    kind: method
    line: 51
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ValueExpressionNodeParserTests.BinaryOperationExpressionParser_ShouldParseNestedExpressionsWithVariablesAndFunctions
    kind: method
    line: 102
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Pourquoi :** Ce test vérifie que le parser de l'expression numérique peut reconnaître et parser correctement des expressions avec des opérations binaires et des appels de fonction.
**Entrées :** Des chaînes d'expressions telles que "(1 + 25) * 589" ou "(1 + 25) * 589 - add(1, 32)".
**Sorties :** Des instances de `ComputedExpressionNode` avec des opérations correctement identifiées.
**Effets secondaires :** Aucuns, car le test ne modifie pas l'état global.

<!-- codesemantic4llm:summary:1 -->
Ce test vérifie que le parser de l'expression peut identifier correctement une opération d'addition dans une expression composée. Il examine une expression binnaire, puis affirme que le résultat est un nœud d'expression calculé. Le test vérifie la structure du nœud et les valeurs associées. Il compare les positions des constantes avec les nœuds opérés. La méthode ne produit pas d'effets secondaires notables.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Tests the ability of the parser to handle complex expressions with function calls.
**Inputs** : A string containing a mathematical expression with a function call.
**Outputs** : A parsed expression tree structure.
**Side Effects** : Parses and validates the input, but does not modify the original data.

<!-- codesemantic4llm:summary:3 -->
**Summary (fr):**
Ce test vérifie que le parser de l'expression peut traiter des expressions avec des variables, des fonctions et des opérations composées. Il vérifie la structure de la sortie et les nœuds associés pour confirmer que les opérations et fonctions sont correctement identifiées et structurées.
**Entrées/Sorties:**
- **Entrée:** Une expression de notation de domaine avec des opérations, variables et fonctions.
- **Sortie:** Un objet de type `ComputedExpressionNode` représentant la structure parsee.
**Effets secondaires:**
- Le parser ne modifie pas l'expression source.
- Les nœuds de l'expression sont créés et analysés, ce qui peut affecter l'état de l'expression.
