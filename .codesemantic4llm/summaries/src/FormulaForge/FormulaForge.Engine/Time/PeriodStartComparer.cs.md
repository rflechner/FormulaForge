---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/Time/PeriodStartComparer.cs
sourceHash: 79B1A3D6351925BDB52746CC21F9A1C56D8C0D9309CD1E67EDFA1D34452E04A8
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Time.PeriodStartComparer
    kind: class
    line: 3
    column: 16
  - qualifiedName: FormulaForge.Engine.Time.PeriodStartComparer.Compare
    kind: method
    line: 5
    column: 16
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Compare periods based on their inclusive start times.
**Inputs** : Two Period objects or null.
**Outputs** : Integer indicating the comparison result.
**Side effects** : None.
**XML documentation** : Not present.
**ReferenceEquals** : Checks for object identity.
**null handling** : Returns 1 for null y, -1 for null x.
**Comparison logic** : Uses .Compare method on start times.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Compare two `Period` objects based on their inclusive start times.
**Inputs** : Two `Period` values, possibly `null`.
**Outputs** : An integer indicating whether `x` is less than, greater than, or equal to `y`.
**Side Effects** : None. The method does not modify any state.
