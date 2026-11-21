---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/Time/Period.cs
sourceHash: 6851B10928E93808AF95560555B9B283CADA1B5149326C051DF8AF47FA3B8B3D
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Time.Period
    kind: record
    line: 6
    column: 15
  - qualifiedName: FormulaForge.Engine.Time.Period.OneDay
    kind: method
    line: 28
    column: 26
  - qualifiedName: FormulaForge.Engine.Time.Period.OneMonth
    kind: method
    line: 39
    column: 26
  - qualifiedName: FormulaForge.Engine.Time.Period.OneYear
    kind: method
    line: 50
    column: 26
  - qualifiedName: FormulaForge.Engine.Time.Period.OfYear
    kind: method
    line: 61
    column: 26
  - qualifiedName: FormulaForge.Engine.Time.Period.Contains
    kind: method
    line: 68
    column: 17
  - qualifiedName: FormulaForge.Engine.Time.Period.Overlaps
    kind: method
    line: 75
    column: 17
  - qualifiedName: FormulaForge.Engine.Time.Period.Divide
    kind: method
    line: 82
    column: 32
  - qualifiedName: FormulaForge.Engine.Time.Period.GetDays
    kind: method
    line: 97
    column: 32
  - qualifiedName: FormulaForge.Engine.Time.Period.GetMonths
    kind: method
    line: 103
    column: 32
  - qualifiedName: FormulaForge.Engine.Time.Period.GetYears
    kind: method
    line: 109
    column: 32
---

<!-- codesemantic4llm:summary:0 -->
Represents a time period with a start and an end. The start of the period is inclusive, while the end is exclusive.

<!-- codesemantic4llm:summary:1 -->
Creates a period that spans a single day.

<!-- codesemantic4llm:summary:2 -->
Creates a period that spans a single month.

<!-- codesemantic4llm:summary:3 -->
Creates a period that spans a single year.

<!-- codesemantic4llm:summary:4 -->
Creates a period that spans a single year.

<!-- codesemantic4llm:summary:5 -->
Returns true if the period contains the specified date.

<!-- codesemantic4llm:summary:6 -->
Determines whether the current period overlaps with the specified period.

<!-- codesemantic4llm:summary:7 -->
Divides the current period into multiple sub-periods based on the specified function that calculates the next boundary.

<!-- codesemantic4llm:summary:8 -->
Divides the current period into daily sub-periods, each representing a single day within the period.

<!-- codesemantic4llm:summary:9 -->
Returns all the months in the period.

<!-- codesemantic4llm:summary:10 -->
Returns all the years in the period.
