---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/DomainSpecificLanguage/LiteralExpressionNodeParserTests.cs
sourceHash: 7240F7AEDE8E6F5A6CABF6B1979901D128D401362F9B496B8405360ACD230BF9
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.LiteralExpressionNodeParserTests
    kind: class
    line: 7
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.LiteralExpressionNodeParserTests.VariableValueExpressionParser_ShouldParseDecimalValues
    kind: method
    line: 20
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.LiteralExpressionNodeParserTests.LiteralExpressionNodeParser_ShouldParseVariableName
    kind: method
    line: 35
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.LiteralExpressionNodeParserTests.LiteralExpressionNodeParser_ShouldParseBooleanValues
    kind: method
    line: 49
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.LiteralExpressionNodeParserTests.LiteralExpressionNodeParser_ShouldParseDecimalValues
    kind: method
    line: 61
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.LiteralExpressionNodeParserTests.LiteralExpressionNodeParser_ShouldParseIntegerValues
    kind: method
    line: 73
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Tests the parsing of literal expressions in the FormulaForge.Engine domain, focusing on variables, boolean, and numeric values.
**Inputs** : Variable names, boolean strings, and numeric strings.
**Outputs** : Parsed expression nodes with expected success and result values.
**Side Effects** : Asserts the correctness of parsing without modifying the input or output data.

<!-- codesemantic4llm:summary:1 -->
Ce test vérifie que le parser de l'expression de valeur peut interpréter des valeurs décimales issues d'une variable. Il passe plusieurs noms de variables pour vérifier la réussite de la parsing et la présence du résultat. Les résultats doivent être identiques à ceux obtenus avec des noms de variables par défaut.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Ce test vérifie que le parser de noeuds d'expression littéraire peut identifier et parser les noms de variables.
**Inputs** : Un nom de variable comme "t", "testVariable", etc.
**Outputs** : Une expression de valeur valide avec le nom de variable.
**Side Effects** : Aucun, l'expression est simplement créée et vérifiée.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Cette méthode teste la capacité du parseur de nœuds d'expression littéraire à parser correctement les valeurs booléennes à partir d'un texte.
**Inputs** : Un string contenant une expression littéraire booléenne et une booléenne attendue.
**Outputs** : La résolution de l'expression et une vérification de sa valeur.
**Side effects** : Aucun effet secondaire notable.
**Behavior** : Le parseur tente de parser le texte et vérifie que le résultat est réussi et contient la valeur booléenne attendue.

<!-- codesemantic4llm:summary:4 -->
**Purpose**: This method tests if a literal expression node parser can correctly parse decimal values from string input.
**Inputs**: A string representing a decimal number and the expected decimal value.
**Outputs**: A parsed result object that should match the expected value.
**Side Effects**: It verifies the parser's correctness without altering the input or output data.

<!-- codesemantic4llm:summary:5 -->
Cette méthode teste la capacité du parseur de expressions littérales à convertir des chaînes numériques en valeurs entières. Elle compare le résultat du parseur avec l'attente spécifiée.
Input : une chaîne de texte et une valeur entière.
Output : un objet de résultat contenant la valeur entière.
Side effects : aucun, la méthode ne modifie l'état.
Test : vérifie que la chaîne "1" et "123" sont correctement convertis en 1 et 123.
