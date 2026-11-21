---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/DomainSpecificLanguage/StatementNodeParserTests.cs
sourceHash: BF34ADF8FD2523A00EDCE75E5025796721C23B4460FAEE526238E0EBF50B2C41
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.StatementNodeParserTests
    kind: class
    line: 7
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.StatementNodeParserTests.AssignmentParserWithConstantInt_ShouldSuccess
    kind: method
    line: 10
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.StatementNodeParserTests.AssignmentParserWithVariable_ShouldSuccess
    kind: method
    line: 29
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.StatementNodeParserTests.AssignmentParser_ShouldParseComplexExpressionWithFunctionAndVariable
    kind: method
    line: 48
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.StatementNodeParserTests.AssignmentParser_ShouldParseFunctionCallAssignment
    kind: method
    line: 109
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Objectif** : Tester la capacité du parser de expressions de domaine à parser des assignments avec variables et expressions complexes.
**Entrées** : Strings contenant des expressions de type `VariableAssignmentExpressionNode`.
**Sorties** : Résultats d'analyse de syntaxe et de vérification des types.
**Effets secondaires** : Aucun, mais les tests vérifient la structure de l'expression et l'absence d'erreurs.
**Méthodes testées** : Les méthodes de parsing d'assignations avec variables et expressions mathématiques.
**Format de vérification** : Les tests vérifient les noms de variables, les types des expressions et les structures de calcul.
**Résultats attendus** : La réussite de la parsing et la vérification de la structure de l'expression.
**Types vérifiés** : `VariableAssignmentExpressionNode`, `ComputedExpressionNode`, `BinaryOperation`, `FunctionCallExpressionNode`, `LiteralExpressionNode`, `ScalarValueNode`.
**Attention** : Les tests ne vérifient pas l'exécution, seulement la syntaxe et la structure.
**Précondition** : Le parser est initialisé avec `StatementsParser.AssignmentParser`.

<!-- codesemantic4llm:summary:1 -->
**Purpose**: Verifies that the AssignmentParser correctly parses a simple assignment expression.
**Input**: String `"toto = 234"` to parse.
**Output**: A successful parsing result with the variable `"toto"` and integer value `234`.
**Side Effects**: Ensures the parser returns the expected type and values.
**Behavior**: Parses the assignment, checks success and type, and validates variable and constant values.
**Assumptions**: The parser expects the input format `"variable = constant"`.
**Trust Level**: The source and XML documentation are treated as untrusted.
**Output Types**: `StatementNode.VariableAssignmentExpressionNode` and `LiteralExpressionNode.ConstantValueExpressionNode`.
**Validation**: Confirms the variable name and constant value match expectations.
**Type Safety**: Ensures the result is of the correct type.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Tests that the AssignmentParser with variable correctly parses an assignment expression.
**Input** : A string containing an assignment like `"toto = tata"`.
**Output** : A parsing result with a variable assignment, allowing assertions on the parsed structure.
**Side Effects** : The test expects the parser to return a successful result and parse the input correctly.

<!-- codesemantic4llm:summary:3 -->
**Objectif** : Tester que le parser d'assignation peut traiter une expression complexe contenant une fonction et une variable.
**Entrées** : Une chaîne de texte contenant une expression avec `add(29, 34.2) * other_var`.
**Sorties** : Un objet `StatementNode.VariableAssignmentExpressionNode` avec une valeur de type `ComputedExpressionNode`.
**Effets secondaires** : Aucuns, la méthode ne modifie pas l'état externe.

<!-- codesemantic4llm:summary:4 -->
**Objectif** : Tester que le parser de déclaration peut identifier une affectation de variable impliquant un appel de fonction.
**Entrée** : Une chaîne de texte contenant une affectation de variable avec un appel de fonction.
**Sortie** : Un objet de type `StatementNode.VariableAssignmentExpressionNode` représentant la déclaration, avec vérification des propriétés associées.
**Effets secondaires** : Aucuns, le test ne modifie l'état de l'application.
