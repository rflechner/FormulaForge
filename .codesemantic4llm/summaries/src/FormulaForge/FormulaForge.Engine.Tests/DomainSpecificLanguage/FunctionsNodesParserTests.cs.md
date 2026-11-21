---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/DomainSpecificLanguage/FunctionsNodesParserTests.cs
sourceHash: 041EA05B75AD88D138CA38BDA293C797C092A9E05BF25217B729FFA09FDF0BB4
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests
    kind: class
    line: 7
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests.FunctionCallWithoutParameters_ShouldParseFunctionCalls
    kind: method
    line: 10
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests.FunctionCallWithOneIntParameter_ShouldParseFunctionCalls
    kind: method
    line: 24
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests.FunctionCallWithTwoIntParameters_ShouldParseFunctionCalls
    kind: method
    line: 39
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests.FunctionCallWithTreeIntParameters_ShouldParseFunctionCalls
    kind: method
    line: 55
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests.FunctionCallWithFourIntParameters_ShouldParseFunctionCalls
    kind: method
    line: 72
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests.FunctionCallWithTreeIntAndOneDecimalParameters_ShouldParseFunctionCalls
    kind: method
    line: 90
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests.FunctionCallWithTreeIntAndOneDecimalAndOneFuncCallAsParameters_ShouldParseFunctionCalls
    kind: method
    line: 108
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests.FunctionCallWithTreeIntAndOneDecimalAndOneFuncCallWithOneParamAsParameters_ShouldParseFunctionCalls
    kind: method
    line: 129
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FunctionsNodesParserTests.FunctionCallWithTreeIntAndOneDecimalAndOneFuncCallWithLotOfParamsAsParameters_ShouldParseFunctionCalls
    kind: method
    line: 154
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Ce classe teste la capacité du parser de fonction à extraire et analyser les appels de fonction avec différents paramètres.
**Inputs** : Des chaînes d'expressions contenant des appels de fonctions avec des arguments.
**Outputs** : Une structure d'analyse contenant le nom de la fonction, les arguments et leur type.
**Side effects** : Aucun, les tests sont non invasifs.
**Behavior** : Le parser découpe les expressions en arguments et les identifie comme des appels de fonctions ou des expressions constantes.
**Testing** : Les tests vérifient que les arguments sont parseés correctement, indépendamment du nombre ou du type.
**Assumptions** : Les tests supposent que le parser ne génère pas d'erreur lors de la parsing.
**Error handling** : Aucun traitement d'erreur n'est observé dans les tests.
**Structure** : Les tests utilisent des assertions pour valider les résultats d'analyse.
**Scalability** : Les tests couvrent plusieurs cas d'arguments, montrant la flexibilité du parser.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Tests the parsing of function calls without parameters.
**Inputs** : A string representing a function call ("testFunction()").
**Outputs** : A parsed result with the function name and empty arguments.
**Side Effects** : Asserts parsing success and function name extraction.
**Behavior** : Ensures the parser correctly identifies the function name.
**No Parameters** : The input has no arguments, so the result should be empty.
**Validation** : Verifies the parsed function call is successful and properly formatted.
**Assumptions** : The parser handles function calls with no parameters.
**Trusted Data** : The input and output are based on the parser's implementation.

<!-- codesemantic4llm:summary:2 -->
**Summary :**
Ce test vérifie que le parser de fonctions peut identifier correctement les appels de fonctions avec un seul paramètre numérique.
**Purpose :**
Valider la capacité du parser à extraire et identifier les noms de fonctions et leurs arguments lors d'une chaîne d'appels de fonction.
**Inputs/Outputs :**
Entrée : chaîne de texte contenant une fonction appelée avec un seul paramètre.
Sortie : un objet contenant le nom de la fonction, les arguments et un indicateur de succès.
**Side Effects :**
Le test ne produit aucun effet tangible, mais vérifie l'opération du parser sur une chaîne d'entrée.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Test that a function call with two integer parameters is parsed correctly.
**Inputs** : A string containing a function call like "testFunction(12, 21)".
**Outputs** : A parsed result with the function name and arguments.
**Side Effects** : Validates parsing logic without altering the input or output.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Ce test vérifie que le parser de fonctions peut identifier et extraire correctement les arguments des fonctions appelées.
**Inputs** : Une chaîne de code contenant une appel de fonction avec trois paramètres entiers.
**Outputs** : Un résultat contenant le nom de la fonction, les arguments et leur position.
**Side Effects** : Aucun effet secondaire visible.
**Behavior** : Le parser devrait retourner un succès avec les arguments correctement extraits et validés.
**Assumptions** : Les arguments sont bien formés et présentent des valeurs entières.
**Trust Level** : Les sources et documentation sont traitées comme non fiables.
**Output Validation** : Les arguments doivent être comparés avec les expressions constantes correspondantes.

<!-- codesemantic4llm:summary:5 -->
**Purpose:** This test ensures that a function call with four integer parameters is correctly parsed by the parser.
**Inputs:** A string representing a function call with four integer arguments.
**Outputs:** A parsed result with the function name and arguments.
**Side Effects:** The parser processes the input string and returns a parsed structure.
The method checks that the function name is correctly extracted and that each argument is a literal integer.
It verifies that the parser handles the function call with four parameters as expected.

<!-- codesemantic4llm:summary:6 -->
**Déclaration :** Méthode de test pour vérifier que la parser de fonctions peut interpréter des appels de fonctions avec des paramètres d'entier et un décimal.
**Entrées :** Chaîne de test contenant une appelle de fonction avec des arguments numériques.
**Sorties :** Résultat de la parsing avec le nom de la fonction et les arguments.
**Effets secondaires :** La méthode ne produit aucun effet secondaire, elle ne modifie pas l'environnement ou les données externes.

<!-- codesemantic4llm:summary:7 -->
**Purpose** : This test verifies that a function call parser correctly identifies and parses function calls, including those with integer, decimal, and function parameters, from a C# source string.
**Important Inputs/Outputs** : The method takes a C# source string containing a function call and returns a parsed result. It expects the parser to return a success status and a list of arguments.
**Side Effects** : The test asserts the parser's output, ensuring it parses the function call correctly and validates the arguments' types and values.

<!-- codesemantic4llm:summary:8 -->
Cette méthode vérifie que le parseur traite correctement les appels de fonctions avec des arguments. Elle retourne un résultat avec des arguments valides, dont certains sont des expressions variables ou des valeurs scalaires. Les validations incluent la longueur des arguments et leur nom, type et valeur. Le parseur ne modifie pas l'entrée, mais l'analyse pour la structure et la validité. Les résultats sont utilisés dans des assertions pour confirmer le comportement attendu.

<!-- codesemantic4llm:summary:9 -->
**Summary (French):**
Cette méthode teste la parsing des appels de fonctions avec différents paramètres, en vérifiant que les noms de fonctions et les valeurs sont correctement identifiés. Elle retourne un résultat avec des arguments et des expressions valides.
**Purpose:**
Tester la capacité du parser à identifier les noms de fonctions et les valeurs associées lors de la décomposition des appels de fonction.
**Inputs/Outputs:**
Input: une chaîne contenant des appels de fonction avec paramètres. Output: un objet de résultat avec des arguments et des expressions parsees.
**Side Effects:**
Aucun effet secondaire, juste l'exécution du parsing et la vérification des résultats.
