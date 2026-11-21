---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/Time/PeriodTests.cs
sourceHash: AF57A2755D71AC8325C8C705714DEC19DE8A28D7A1685CF910331274A02E5C3D
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.Time.PeriodTests
    kind: class
    line: 5
    column: 14
  - qualifiedName: FormulaForge.Engine.Tests.Time.PeriodTests.Overlaps_WhenPeriodsDoNotOverlap_ReturnsFalse
    kind: method
    line: 8
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Time.PeriodTests.Overlaps_WhenPeriodStartsInsideOther_ReturnsTrue
    kind: method
    line: 24
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Time.PeriodTests.Overlaps_WhenPeriodIsContainedWithinOther_ReturnsTrue
    kind: method
    line: 40
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Time.PeriodTests.Overlaps_WhenPeriodsAreIdentical_ReturnsTrue
    kind: method
    line: 56
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Time.PeriodTests.Overlaps_WhenPeriodEndsBeforeOtherStarts_ReturnsFalse
    kind: method
    line: 72
    column: 17
  - qualifiedName: FormulaForge.Engine.Tests.Time.PeriodTests.GetDays_ReturnsCorrectNumberOfDays
    kind: method
    line: 89
    column: 17
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : This class contains unit tests for the `Period` class, testing overlap conditions and day calculations.
**Inputs** : Two `Period` instances with start and end times in `DateTimeOffset`.
**Outputs** : Boolean result from `Overlaps`, and a list of `DateTimeOffset` representing days within the period.
**Side Effects** : The tests assert expected outcomes, but no external state is modified.
**Behavior** : Overlaps are determined by checking if time ranges intersect.
**Side Effects** : The `GetDays` method returns a list of days in the period, with inclusive and exclusive start/end points.
**Behavior** : The method counts days between start and end, inclusive of start and exclusive of end.
**Side Effects** : The list returned by `GetDays` is used for assertions, confirming correct day ranges.
**Behavior** : The tests cover cases like non-overlapping, nested, identical, and edge time scenarios.
**Side Effects** : The tests rely on the `Period` class's implementation for accurate results.

<!-- codesemantic4llm:summary:1 -->
Cette méthode teste si deux périodes se chevauchent quand elles ne sont pas overlap. Elle compare les périodes avec des dates et des horaires. Si les périodes ne se chevauchent pas, elle retourne `false`. Aucun effet secondaire est observé.

<!-- codesemantic4llm:summary:2 -->
**Objectif** : Tester si deux périodes se chevauchent lorsque la période 1 commence à l'intérieur de la période 2.
**Entrées** : Deux instances de `Period` avec des dates et des durées définies.
**Sorties** : Les méthodes retournent `true` s'il y a une chevauchance.
**Effets secondaires** : Aucun, car les tests ne modifient pas l'état global.

<!-- codesemantic4llm:summary:3 -->
**Objectif :** Tester si deux intervalles de temps se chevauchent lorsque l’un est inclus dans l’autre.
**Entrées :** Deux instances de `Period` avec dates et durées définies.
**Sorties :** Les tests affirment que les intervalles se chevauchent.
**Effets secondaires :** Aucun impact global, seulement des vérifications logiques.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Tests if two identical time periods overlap.
**Inputs** : Two `Period` objects with the same start and end times.
**Outputs** : Asserts that both periods overlap (returns `true`).
**Side effects** : None.
**Note** : XML documentation is not trusted and not used as instructions.
**Important** : Periods are compared for overlap, not for equality.
**Behavior** : If start and end times are the same, overlap is considered.
**Trust** : Source code and XML documentation are not validated.
**Use** : Ensures consistent handling of identical time intervals.
**Coverage** : Verifies mutual overlap in identical periods.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Cette méthode teste si deux périodes se chevauchent lorsque la fin de la première se situe avant le début de la seconde.
**Inputs** : Deux instances de `Period` avec des dates de début et de fin.
**Outputs** : Une valeur booléenne indiquant si les périodes se chevauchent.
**Side Effects** : Aucuns effets secondaires, juste des appels de méthodes et des vérifications.

<!-- codesemantic4llm:summary:6 -->
Cette méthode teste que `Period.GetDays()` retourne le bon nombre de jours entre deux dates inclusives. Elle utilise deux dates de début et de fin pour créer un intervalle, puis vérifie les résultats. Les entrées sont des dates inclusives, et les sorties sont des jours inclusifs. Les effets secondaires incluent la création d’un intervalle et la vérification de la liste des jours.
