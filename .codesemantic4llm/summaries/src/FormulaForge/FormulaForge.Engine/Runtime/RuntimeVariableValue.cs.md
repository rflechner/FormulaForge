---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/Runtime/RuntimeVariableValue.cs
sourceHash: 2BB75F5951E874DF3363C2AC25F034033C3AD7901D72011282765860AD8D772F
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Runtime.RuntimeVariableValue
    kind: record
    line: 6
    column: 24
  - qualifiedName: FormulaForge.Engine.Runtime.RuntimeVariableValue.RuntimeScalarValue
    kind: record
    line: 8
    column: 19
  - qualifiedName: FormulaForge.Engine.Runtime.RuntimeVariableValue.RuntimeTimeSeriesValue
    kind: record
    line: 10
    column: 19
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Define a base type for various variable value representations in a runtime system.
**Inputs** : A `ScalarValueNode` or a `TimeSeries<ScalarValueNode>`.
**Outputs** : A derived type that wraps the value in a runtime context.
**Side Effects** : None, as it's a data structure only.
**Behavior** : Abstract class with derived records for scalar and time series values.
**Trusted Data** : XML documentation is considered untrusted.
**Used In** : Code navigation and variable handling.
**Not Used In** : Any runtime execution or modification.
**Depends On** : `ScalarValueNode` and `TimeSeries<ScalarValueNode>`.
**Inherits From** : `RuntimeVariableValue`.

<!-- codesemantic4llm:summary:1 -->
**Purpose**: Represents a scalar value associated with a runtime variable, encapsulating the value in a structured format.
**Inputs**: A `ScalarValueNode` which contains the actual data value.
**Outputs**: A `RuntimeVariableValue` record that wraps the scalar value.
**Side effects**: None, as it merely stores the value without modifying it.
**Usage**: Used in runtime variable systems to manage and access scalar values.
**Trust note**: XML documentation is considered untrusted and not used as instruction.
**Language**: C#
**Type**: Record
**Dependencies**: `RuntimeVariableValue` and `ScalarValueNode`
**Behavior**: No behavior beyond storing the value.

<!-- codesemantic4llm:summary:2 -->
**Purpose** :
Définit une valeur d'une série temporelle dans le contexte du runtime de FormulaForge.
**Inputs/Outputs** :
Accepte un objet `TimeSeries<ScalarValueNode>` comme entrée et retourne un record.
**Side Effects** :
Aucun effet secondaire visible.
**Code Navigation** :
Sert à naviguer dans la structure de données liée aux valeurs temporielles.
**XML Documentation** :
L'XML n'est pas inclus dans la déclaration.
