---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/Time/TimeSeries.cs
sourceHash: 4A03A98EFFB59C1E76959999BB6B716D49D66AE794A59D4D9FDB85BBEB406D24
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries
    kind: class
    line: 6
    column: 21
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.Empty
    kind: method
    line: 13
    column: 33
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.CreateTimeSeries
    kind: method
    line: 22
    column: 33
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.Aggregate
    kind: method
    line: 41
    column: 33
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.Multiply
    kind: method
    line: 64
    column: 33
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.Divide
    kind: method
    line: 77
    column: 33
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.Add
    kind: method
    line: 90
    column: 33
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.Subtract
    kind: method
    line: 103
    column: 33
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.Cast
    kind: method
    line: 116
    column: 36
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries
    kind: class
    line: 122
    column: 14
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.Add
    kind: method
    line: 133
    column: 17
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.GetValue
    kind: method
    line: 187
    column: 14
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.ContainsPeriod
    kind: method
    line: 189
    column: 17
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.GetEnumerator
    kind: method
    line: 192
    column: 44
  - qualifiedName: FormulaForge.Engine.Time.TimeSeries.GetEnumerator
    kind: method
    line: 197
    column: 29
  - qualifiedName: FormulaForge.Engine.Time.TimeSeriesValue
    kind: record
    line: 202
    column: 15
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : This class provides methods to create, aggregate, and manipulate time series data. It supports element-wise operations such as addition, subtraction, multiplication, and division, as well as type casting.
**Inputs/Outputs** : Methods take time series values (either an `IEnumerable` or an `IReadOnlyCollection`) and a merge function or a numeric type. They return new time series instances with computed or cast values.
**Side Effects** : The `Cast` method creates a new time series without modifying the original. Other methods may throw exceptions if values are incompatible or if merging is attempted during creation.

<!-- codesemantic4llm:summary:1 -->
Creates an empty TimeSeries.

<!-- codesemantic4llm:summary:2 -->
Creates a TimeSeries from the specified values.

<!-- codesemantic4llm:summary:3 -->
Aggregates the specified TimeSeries values using the specified merge function.

<!-- codesemantic4llm:summary:4 -->
Multiplies the values of two TimeSeries instances element-wise over their common periods.

<!-- codesemantic4llm:summary:5 -->
Divides the corresponding values of two TimeSeries instances.

<!-- codesemantic4llm:summary:6 -->
Adds the values of two TimeSeries instances element-wise for matching periods using the specified numeric type T.

<!-- codesemantic4llm:summary:7 -->
Subtracts the values of one TimeSeries from another.

<!-- codesemantic4llm:summary:8 -->
Casts a TimeSeries from one type to another type, ensuring compatibility between the types.

<!-- codesemantic4llm:summary:9 -->
**Purpose** :
Ce classe gère la séquence de données temporelle, stockant des valeurs associées à des périodes et fusionnant les valeurs lorsqu'une période est déjà présente.
**Inputs/Outputs** :
Elle accepte un `Period` et une valeur, et peut retourner une valeur pour un `Period` ou vérifier la présence d'un `Period`.
**Side Effects** :
Modifie la collection `_data` en ajoutant des nouveaux segments et en supprimant les anciens, affectant ainsi la structure interne de la séquence.

<!-- codesemantic4llm:summary:10 -->
Adds a new entry to the TimeSeries for the specified period and value.
If the period already exists in the TimeSeries, the mergeFunction is applied to handle the conflict.

<!-- codesemantic4llm:summary:11 -->
**Purpose** : This method retrieves a value from a time series data structure based on a given period.
**Inputs** : A `Period` object specifying the time point to query.
**Output** : The value stored at the specified period.
**Side Effects** : None — it simply returns data without modifying it.
**Behavior** : It accesses the `_data` array directly using the provided period.
**Assumptions** : The `_data` array is pre-initialized and indexed by period.
**Security** : Treats the input as untrusted — no validation is performed.
**Trust** : XML documentation is untrusted and not used as instructions.
**Constraints** : Operates within the bounds of the `_data` array.
**Generalization** : Could be applied to any indexed data structure.

<!-- codesemantic4llm:summary:12 -->
**Purpose** : Checks if a given `Period` exists in the internal data structure.
**Input** : A `Period` object to search for in the data.
**Output** : A boolean indicating whether the period is found.
**Side Effects** : None. The method simply checks for the presence of the period.

<!-- codesemantic4llm:summary:13 -->
C# `GetEnumerator` est une méthode de la classe `TimeSeries` qui permet d'itérer sur les valeurs de la série temporelle. Elle retourne un `IEnumerator` pour accéder aux paires clé-valeur.
L'entrée est un objet `TimeSeries` contenant des données, et l'output est une séquence d'objets `TimeSeriesValue<T>`.
Cette méthode ne produit aucun effet secondaire, mais elle expose les données de la série temporelle pour l'itération.
Elle ne manipule pas les données directement, mais la transformations via le `Select` dans le constructeur.

<!-- codesemantic4llm:summary:14 -->
**Purpose** : Cette méthode permet d'itérer sur les éléments d'une séquence de temps.
**Entrées** : Elle prend un paramètre de type `IEnumerable` qui contient les éléments.
**Sorties** : Elle retourne une `IEnumerator` qui permet d'accéder aux éléments.
**Effets secondaires** : Aucun effet secondaire visible.

<!-- codesemantic4llm:summary:15 -->
**Purpose** : Ce record représente une valeur temporelle associée à une période spécifique.
**Inputs** : Une instance de `Period` et une valeur de type `T`.
**Outputs** : Une instance de `TimeSeriesValue<T>` contenant la période et la valeur.
**Side Effects** : Aucun effet secondaire.
**Usage** : Utilisé pour stocker et manipuler des valeurs temporelles.
**Unreliability** : La documentation XML est considérée comme non fiable.
**Minimalism** : Le type est conçu pour la simplicité et la clarté.
**Trust** : Ne pas confier de code à partir de cette déclaration.
**Clarity** : Il est facile de comprendre sa structure.
**Reliability** : Le comportement n’est pas garantie.
