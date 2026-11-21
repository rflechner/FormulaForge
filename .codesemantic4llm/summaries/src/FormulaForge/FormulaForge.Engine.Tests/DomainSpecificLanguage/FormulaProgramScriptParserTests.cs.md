---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/DomainSpecificLanguage/FormulaProgramScriptParserTests.cs
sourceHash: BE25EB55AA742C4D20E45628AF0B08D8AD21455ABD721F1DD4BD9C3D2D79B00F
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FormulaProgramScriptParserTests
    kind: class
    line: 6
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FormulaProgramScriptParserTests.FunctionCallWithoutParameters_ShouldParseFunctionCalls
    kind: method
    line: 9
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.FormulaProgramScriptParserTests.ParsingFailure_ShouldReturnFailure
    kind: method
    line: 58
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Ce code testifie la capacité du parser à interpréter des expressions de programmation dans un langage spécifique, en vérifiant la réussite des parois et les détails des déclarations de fonctions et des appels de fonctions.
**Inputs/Outputs** : L'entrée est un fichier `.formula` contenant des instructions de programmation. La sortie est un objet `ParseResult` qui indique le succès de la parsing et contient un tableau d'objets parseurs.
**Side Effects** : Le code produit des assertions, mais ne modifie pas l'environnement externe. Il ne crée aucun objet persistant ni ne manipule de données en dehors des tests.

<!-- codesemantic4llm:summary:1 -->
Ce test vérifie que le parser de langage spécifique de FormulaForge peut identifier et parser les appels de fonctions sans paramètres. Il lit un fichier `.formula`, initialise le parser, et vérifie que les résultats contiennent trois éléments : une déclaration de fonction, une assignation de variable avec une appel de fonction, et une autre assignation. Le test vérifie que chaque appel de fonction est correctement parse, avec le bon nom et les arguments attendus.

<!-- codesemantic4llm:summary:2 -->
Ce test vérifie que le parser identifie correctement un échec de parsing. Il utilise un code contenant une expression invalide et vérifie que le résultat inclut un nœud d'erreur. L'entrée est un script contenant des assignments et une expression mathématique invalide. Le résultat contient des informations sur le succès, le nœud d'erreur, et les détails du problème. Ce test n'a pas d'effet secondaire notable.
