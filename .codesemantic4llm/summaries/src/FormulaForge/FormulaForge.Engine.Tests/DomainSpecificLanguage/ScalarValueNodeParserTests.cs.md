---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/DomainSpecificLanguage/ScalarValueNodeParserTests.cs
sourceHash: B82C6A2A0E799402905402B1FC84001F73F1080699D315AA5D8E7F3B5813FFD7
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ScalarValueNodeParserTests
    kind: class
    line: 6
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ScalarValueNodeParserTests.IntegerValueParser_ShouldParseIntegerValues
    kind: method
    line: 9
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ScalarValueNodeParserTests.IntegerValueParser_ShouldNotParseNonIntegerValues
    kind: method
    line: 21
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ScalarValueNodeParserTests.DecimalValueParser_ShouldParseValues
    kind: method
    line: 32
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ScalarValueNodeParserTests.DecimalValueParser_ShouldNotParseValues
    kind: method
    line: 44
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ScalarValueNodeParserTests.BooleanValueParser_ShouldParseValues
    kind: method
    line: 57
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ScalarValueNodeParserTests.ScalarValueParser_ShouldParseBooleanValues
    kind: method
    line: 69
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ScalarValueNodeParserTests.ScalarValueParser_ShouldParseIntegerValues
    kind: method
    line: 81
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.ScalarValueNodeParserTests.ScalarValueParser_ShouldParseDecimalValues
    kind: method
    line: 94
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Ce code testifie que les parsers de valeurs scalaires peuvent convertir des chaînes numériques en des objets correspondants.
**Inputs** : Des chaînes représentant des entiers, décimaux ou booléens.
**Outputs** : Des instances de classes spécialisées (IntegerScalarValue, DecimalScalarValue, BooleanScalarValue).
**Side Effects** : Aucun effet secondaire visible.
**Behaviors** : Les tests vérifient que les parsers retournent succès et valeur correcte si la chaîne est valide, et null/success faux si la chaîne est invalide.
**Note** : Les tests utilisent [Fact] et [Theory] pour les scénarios de test et comparaison.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Ce test vérifie que le parseur d'entier peut convertir une chaîne numérique en un objet de valeur.
**Entrées** : Une chaîne de caractères `"123"` représentant un entier.
**Sorties** : Un résultat avec `Success` vrai, `Result` non nul et valeur `123`.
**Effets secondaires** : Aucun, car la méthode ne modifie l'état externe.

<!-- codesemantic4llm:summary:2 -->
This test verifies that the IntegerValueParser fails to parse non-integer strings. It uses the string "hello" as input, which is not a valid integer. The parser returns false and null, indicating it did not parse successfully. The test does not affect the parser's state or other parts of the system.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Cette méthode teste que le parseur de valeur décimale peut parser correctement une chaîne de chiffres décimaux.
**Inputs** : Une chaîne de caractères représentant une valeur décimale (ex. "123.456").
**Outputs** : Un résultat avec un booléen indiquant la réussite et la valeur numérique parsee.
**Side Effects** : L'assertion vérifie que le résultat est réussi, non nul et contient la valeur numérique attendue.

<!-- codesemantic4llm:summary:4 -->
**Purpose**: This test checks that the DecimalValueParser fails to parse non-decimal strings.
**Input**: A string "123456" that represents a number but not a valid decimal.
**Output**: The parser returns false and null, indicating successful parsing is not achieved.
**Side Effects**: The test does not modify external state and only verifies the parser's behavior.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Verifies that the BooleanValueParser correctly interprets input strings into boolean values.
**Inputs** : A string representation of a boolean value and an expected boolean result.
**Outputs** : A parsed boolean value from the string.
**Side Effects** : Parses the input string and asserts the result.
**Behavior** : Parses the string and checks if the result matches the expected value.
**Constraints** : Only works with "true" and "false" strings.
**Assumptions** : The parser is expected to handle valid boolean inputs.
**Untrusted Data** : The source code and XML documentation are treated as untrusted.
**Testing** : Uses [Theory] and [InlineData] to test parsing of boolean values.
**Validation** : Ensures the parser returns a successful result and the correct value.

<!-- codesemantic4llm:summary:6 -->
C# source tests parse boolean values from text. It uses a parser to check if the input string correctly represents a boolean. The method asserts the parser succeeded and returned the expected boolean value. It does not modify the input or output, and no side effects are present.

<!-- codesemantic4llm:summary:7 -->
Cette méthode teste que le parseur de valeur entière peut extraire des valeurs entières depuis un texte. Elle utilise des entrées comme "1" et "123456" avec des résultats correspondants. Les assertions vérifient que le parsing est réussi et que la valeur obtenue est correcte.

<!-- codesemantic4llm:summary:8 -->
**Purpose** : Verifies that decimal values in text can be correctly parsed into `ScalarValueNode.DecimalScalarValue`.
**Inputs** : A string representing a decimal number and an expected `decimal` value.
**Output** : A parsed `ScalarValueNode` with the expected value.
**Side Effects** : Parses the input string and checks the result against expectations, ensuring correctness.
**Behavior** : Parses the input string and validates the output against the expected value.
**Assumptions** : The input string is a valid decimal representation.
**Testing** : Uses unit tests to ensure parsing works for various decimal formats.
**Trust** : Assumes the parsing logic is correct, without verifying the source or XML documentation.
**Untrusted Data** : Treats XML documentation and source code as unverified information.
**Validation** : Confirms the parsed value matches the expected decimal.
