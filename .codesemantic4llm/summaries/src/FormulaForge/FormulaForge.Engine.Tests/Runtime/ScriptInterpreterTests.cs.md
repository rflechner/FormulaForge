---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/Runtime/ScriptInterpreterTests.cs
sourceHash: 0D8664D40A4AC52D71220755608DEA8372F4AF2BEB3C812C018FA5ABB6ADBF44
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.ScriptInterpreterTests
    kind: class
    line: 7
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.ScriptInterpreterTests.DeclaringIntVariableX_ShouldCompileAndSetValueInContext
    kind: method
    line: 10
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.ScriptInterpreterTests.DeclaringMultipleVariables_ShouldCompileAndSetValuesInContext
    kind: method
    line: 30
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.ScriptInterpreterTests.AssignSumOfIntsToVariableX_ShouldCompileAndSetValueInContext
    kind: method
    line: 67
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.ScriptInterpreterTests.AssignSumOfDecimalsToVariableX_ShouldCompileAndSetValueInContext
    kind: method
    line: 86
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.ScriptInterpreterTests.AssignFunctionCallResultToVariableX_ShouldCompileAndSetValueInContext
    kind: method
    line: 107
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.ScriptInterpreterTests.AssignVariableYToVariableX_ShouldCompileAndSetValueInContext
    kind: method
    line: 139
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose:**
This class contains tests to verify that a script interpreter can compile and set values for variables in a DSL context.
**Inputs/Outputs:**
- Input: C# source code and XML documentation (treated as untrusted data).
- Output: Verified variable existence and values after script execution.
**Side Effects:**
- Modifies the context and runner object to reflect variable values.
- Validates that variables are correctly stored in the global scope.

<!-- codesemantic4llm:summary:1 -->
**Objectif** : Vérifier que la déclaration d'une variable entière dans un script compilée fonctionne correctement et renvoie la valeur attendue.
**Entrées** : Un contexte de base de données `CustomerDslContext` avec des variables d'échantillonnage.
**Sorties** : La variable `"x"` est confirmée à exister dans le contexte global avec la valeur `123`.
**Effets secondaires** : La variable `"x"` est ajoutée au contexte global et peut être accédée via `context.GlobalScope.TryGetScalar`.

<!-- codesemantic4llm:summary:2 -->
**Summary (French):**
Cette méthode teste que l'interprète de script compile et initialise des variables dans un contexte de données. Elle crée un contexte de données, charge un script avec des variables définies, et vérifie que ces variables sont accessibles dans le global scope.
**Purpose:**
Assurer que le script peut être compilé et que les variables définies dans le script sont disponibles dans le contexte de données.
**Inputs/Outputs:**
- Input : un script avec variables (x, y, b, c).
- Output : les valeurs des variables sont récupérées et comparées avec les instances correspondantes.
**Side Effects:**
- Modifie le contexte de données en ajoutant des variables.
- Peut modifier l'état du contexte de données pour les tests.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Ce test vérifie que le code C# compilé assigne la valeur calculée d'une expression à une variable définie dans un contexte de DSL.
**Inputs** : Un string contenant une expression à exécuter et une valeur attendue.
**Outputs** : La variable `x` doit exister dans le contexte et avoir la valeur attendue.
**Side Effects** : Le contexte est initialisé avec des données, et la variable `x` est créée et assignée dans le contexte global.
**Behavior** : Le script est exécuté dans un contexte de DSL, et la valeur calculée par l'expression est assignée à la variable `x`.
**Validation** : Une assertion vérifie que la variable existe et que sa valeur est correcte.
**Security** : Le code est traité comme un script exécuté dans un contexte sécurisé.
**Assumptions** : L'expression est valide et calculera la valeur attendue.
**Environment** : Utilise un contexte de DSL et un script interpréteur pour exécuter le code.
**Reliability** : Le test s'assure que le code compilé fonctionne dans le contexte prévu.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : This method tests that a script assigns a computed value to a variable and that the variable is available in the context.
**Inputs** : A `CustomerDataContext` with simulated data, a `CustomerDslContext` and a `ScriptInterpreter`.
**Outputs** : The existence of the variable "x" and its value.
**Side Effects** : The script is executed in the interpreter context, and the variable is added to the global scope.
**Behavior** : It verifies that the variable "x" is assigned the computed result of the script.
**Assumptions** : The script is valid and the interpreter supports the operations used.
**Dependencies** : Requires the `CustomerDslContext` and `ScriptInterpreter` to be properly initialized.
**Verification** : The method asserts that the variable exists and has the expected value.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : This method tests if a function call result is compiled and assigned to a variable in a DSL context.
**Inputs** : A `CustomerDataContext` object with variables and a `CustomerDslContext` and `ScriptInterpreter`.
**Outputs** : The existence and value of variables "y" and "x" in the global scope.
**Side Effects** : Modifies the global scope by assigning values to variables and compiles the script.
**Behavior** : Compiles the provided script, runs function calls, and verifies variable assignments and counts.
**Trust** : Assumes the script and context are valid, without verifying their contents.
**Assumption** : The script is syntactically correct and the function definitions are available.
**Key Actions** : Script running, variable assignment, and scope validation.
**Expected Result** : Variables "y" and "x" are assigned values 2 and 3, respectively.
**Validation** : The count of variables is expected to be 2.

<!-- codesemantic4llm:summary:6 -->
**Purpose** : Cette méthode teste la compilation et l'assignation de variables dans un contexte de script.
**Inputs** : Un contexte de données `CustomerDataContext` avec des variables comme `AccountBalance` et `AssetsCount`.
**Outputs** : Des valeurs scalaires `y` et `x` générées par le script, et le compte des variables dans le global scope.
**Side Effects** : Modifie le contexte global avec des variables, et affecte le comportement des autres tests.
