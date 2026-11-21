---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/Runtime/TimeSeriesBindingTests.cs
sourceHash: 8E68FEE27F53986738FC03C4143496A3EC54774D5AA23238DBF02A54CB33BA90
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.TimeSeriesBindingTests
    kind: class
    line: 7
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.TimeSeriesBindingTests.ApplyRateFromBinding_ShouldComputeExpectedValues
    kind: method
    line: 10
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.TimeSeriesBindingTests.ApplyRateFromBindingWithTimeSeriesOfDecimalAndInteger_ShouldComputeExpectedValues
    kind: method
    line: 69
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.TimeSeriesBindingTests.AdditionWithTimeSeriesOfDecimalAndInteger_ShouldComputeExpectedValues
    kind: method
    line: 112
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.TimeSeriesBindingTests.SubtractionWithTimeSeriesOfDecimalAndInteger_ShouldComputeExpectedValues
    kind: method
    line: 155
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.TimeSeriesBindingTests.DivideWithTimeSeriesOfDecimalAndInteger_ShouldComputeExpectedValues
    kind: method
    line: 198
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.TimeSeriesBindingTests.MultiplicationBetweenTimeSeriesOfIntegerAndScalarDecimal_ShouldComputeExpectedValues
    kind: method
    line: 241
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Runtime.TimeSeriesBindingTests.AdditionBetweenTimeSeriesOfIntegerAndScalarDecimal_ShouldComputeExpectedValues
    kind: method
    line: 284
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Tests time series binding and computation in a C# application, validating arithmetic operations between decimal and integer time series values.
**Inputs** : Customer data including account balance, assets count, and change rate as time series.
**Outputs** : Resulting time series values from arithmetic operations, verified against expected outcomes.
**Side Effects** : Initializes test context, runs script interpreter with defined operations, and asserts expected values.
**Behavior** : Uses a script interpreter to evaluate arithmetic expressions involving time series data, ensuring results align with expected values.
**Language** : C# with XML documentation and test framework.
**Untrusted Data** : Source code and XML documentation are treated as unverified data.
**Test Coverage** : Applies addition, subtraction, division, multiplication, and scalar operations to different data types.
**Assumptions** : Arithmetic operations follow standard rules, and time series values are correctly mapped.
**Output Validation** : Compares results against manually calculated expected values using the TimeSeries.Aggregate method.

<!-- codesemantic4llm:summary:1 -->
**Déclaration :** `ApplyRateFromBinding_ShouldComputeExpectedValues` est une méthode de test qui vérifie si un calcul de taux à partir de séries temporielles est correct.
**Entrées :** Des séries temporielles représentant des comptes bancaires avec des valeurs de balances et de taux de changement.
**Sorties :** Une série temporelle calculant le solde en devise (balance_in_currency), ainsi que des variables globales comme `x`, `y`, et `last_year`.
**Effets secondaires :** La méthode modifie la contexte de l'interprète de script et affecte des variables globales, ce qui peut entraîner des changements dans la logique du script global.

<!-- codesemantic4llm:summary:2 -->
Ce test vérifie que le calcul du taux d'évolution à partir d'un binding avec des séries temporelles de valeurs décimales et d'entiers produit les résultats attendus. Il crée des données contextuelles avec des séries temporelles pour les actifs et le taux de changement, puis exécute un script pour multiplier les deux. Le résultat est vérifié contre une séries temporelle calculée manuellement. L'important est que le script interprète les expressions et que les résultats scalaires soient correctement capturés et comparés.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Tests that adding a time series of assets count to another time series of change rate yields the correct aggregated result.
**Inputs** : A data context with three time series: `AssetsCount`, `ChangeRate`, and `result` computed via a script.
**Outputs** : Expected aggregated time series and actual result time series.
**Side Effects** : Modifies the data context and global scope to store the result variable.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Tests subtraction of two time series (decimal and integer) to ensure expected output values are computed.
**Inputs** : Two time series (assets_count and change_rate) with decimal values and an integer value.
**Output** : Result of subtraction as a time series, expected to match aggregated difference.
**Side Effects** : Initializes database context, runs script interpreter, retrieves and compares results.
**Behavior** : Uses `ScriptInterpreter` to execute a subtraction script and validates output against an expected time series.
**Untrusted Data** : XML comments and source code are treated as unverified data.
**Assumptions** : All time series values are correctly mapped and accessible via DSL.
**Validation** : Compares actual result with expected value using `Assert.Equal`.
**Language** : C# with F# interop and time series data handling.
**Constraints** : Limited to 10 lines, 2-4 sentences per explanation.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Tests division of two time series values, ensuring the result matches expected values using DTS DSL.
**Inputs** : Time series data for assets count and change rate.
**Outputs** : Result of division as a time series.
**Side Effects** : Modifies the data context, runs a script, and asserts the result.
The method creates test data for a time period, populates it into a data context, and uses a script interpreter to compute the result of dividing assets count by change rate. It then compares the result with an expected time series.
It ensures that the runtime calculation of the division result is accurate and aligns with the predefined values.
The test validates the correctness of the arithmetic operation in the time series context.
It relies on the script interpreter to execute the arithmetic expression and return the result.

<!-- codesemantic4llm:summary:6 -->
**Purpose** : This method tests if multiplying a time series of integers by a scalar decimal produces the expected result. It verifies the correctness of the multiplication logic in a time series framework.
**Inputs** : It uses a `CustomerDataContext` with three time series (`AssetsCount`, `ChangeRate`, and `dataContext`), which are created with predefined values. The `dataContext` contains a list of months.
**Outputs** : It computes the expected result of multiplying `AssetsCount` by `0.9` and compares it with the actual result retrieved from the runtime system.
**Side Effects** : It modifies the `CustomerDataContext` by creating time series values and runs a script interpreter to evaluate the expression, potentially affecting the runtime state.

<!-- codesemantic4llm:summary:7 -->
**Objectif** : Vérifier que l'addition de deux séries temporelles (une entier, une décimale) produit une série temporelle attendue.
**Entrées** : Une série `assetsCount` de décimales, et une série `changeRate` de décimales. Les séries sont créées à partir de données d'exemple (années, mois) et d'objets de données correspondants.
**Sorties** : Une série temporelle `resultTimeSeries` qui devrait refléter l'addition de la valeur de `assetsCount` par mois avec 10.
**Effets secondaires** : Création de données temporelles, exécution de code dans un contexte de script, et vérification des résultats via une assertion.
