---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/Time/TimeSeriesTests.cs
sourceHash: 59BFEE01C70AEF706882BA8DCC754656C728CADD944F0BFB5EE5205E6FF55106
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.Time.TimeSeriesTests
    kind: class
    line: 6
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.Time.TimeSeriesTests.Add_WhenPeriodDoesNotExist_ShouldAddValue
    kind: method
    line: 9
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Time.TimeSeriesTests.Add_WhenPeriodExists_ShouldMergeValues
    kind: method
    line: 24
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Time.TimeSeriesTests.Add_MultiplePeriods_ShouldBeSorted
    kind: method
    line: 47
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Time.TimeSeriesTests.Add_OverlappingPeriods_ShouldBeMerged
    kind: method
    line: 64
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Time.TimeSeriesTests.Add_PartialOverlap_ShouldFragmentCorrectly
    kind: method
    line: 103
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** :
The `TimeSeriesTests` class contains unit tests for the `TimeSeries` class to verify its behavior with adding values to periods, merging values when periods overlap, and handling partial overlaps.
**Inputs/Outputs** :
- Input: A `TimeSeries` object and a `Period` object with an associated value and an aggregation function.
- Output: Updated `TimeSeries` object with values merged or added based on the period's existence and overlap.
**Side Effects** :
- Modifies the `TimeSeries` object by adding or merging values.
- Does not affect external data, only the test state.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : This method tests that a value is added to a TimeSeries when the specified period does not exist.
**Input** : A TimeSeries and a Period with a start and end time.
**Output** : The series should contain the period and have the value 10 associated with it.
**Side Effect** : The time series is modified with the added value for the specified period.

<!-- codesemantic4llm:summary:2 -->
**Déclaration :** `Add_WhenPeriodExists_ShouldMergeValues` est une méthode de test C# utilisée pour vérifier que les valeurs d'une série de temps sont fusionnées lorsqu'une période spécifique existe.
**Entrées :** Une instance de `TimeSeries<int>`, un point de départ et une période définie.
**Sorties :** La valeur fusionnée de la série pour la période spécifique.
**Effets secondaires :** Modifie la série avec des nouvelles valeurs et fusionne celles existantes.
**Comportement attendu :** La valeur fusionnée est 15, car 10 + 5 = 15.
**Prérequis :** La série doit contenir des valeurs pour la période définie.
**Cas d'erreur :** Si la période n'existe pas, la méthode ne fusionne pas les valeurs.
**Événements :** L'assertion vérifie que les valeurs fusionnées sont correctes et que les périodes sont identiques.
**Validation :** L'assertion finale confirme que la valeur fusionnée est 15.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : This method tests that adding multiple periods to a time series correctly sorts them based on their start time.
**Inputs** : A `TimeSeries<int>` object, two `Period` objects with specific start and end times, and a sorting key based on period values.
**Outputs** : The expected values of the time series at the specified periods.
**Side Effects** : None, as the method only performs assertions.
**Behavior** : The method adds two periods to the time series and verifies that the values are assigned correctly.
**Untrusted Data** : The source code and XML documentation are treated as unverified information.
**No Behavior Invented** : The test does not modify the system or simulate any external behavior.
**No Behavior Invented** : The test does not modify the system or simulate any external behavior.
**No Behavior Invented** : The test does not modify the system or simulate any external behavior.
**No Behavior Invented** : The test does not modify the system or simulate any external behavior.

<!-- codesemantic4llm:summary:4 -->
**Purpose**
This method tests that overlapping periods in a time series are merged, and values are aggregated correctly.
**Inputs/Outputs**
It takes a `TimeSeries<T>` and adds multiple `Period` entries with a specified aggregation function. It returns an array of merged periods and values.
**Side Effects**
The method modifies the `TimeSeries` by adding periods and values, and then asserts the expected merged structure.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Verifies that time series data is fragmented correctly when periods have partial overlap.
**Inputs** : A time series object, two overlapping periods with start and end times, and a value addition function.
**Outputs** : The resulting time series with fragmented data and expected values.
**Side Effects** : Modifies the time series object by adding the specified periods.
**Behavior** : When periods partially overlap, the time series is split into separate entries, and values are summed according to the provided lambda function.
**Test Coverage** : Checks the start and end times of each fragment and the corresponding summed value.
