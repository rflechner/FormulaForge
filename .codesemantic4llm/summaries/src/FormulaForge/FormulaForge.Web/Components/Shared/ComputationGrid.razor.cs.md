---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Web/Components/Shared/ComputationGrid.razor.cs
sourceHash: DD93CB31DE34D8733E962EB965EDEE6E9958CC70F5776DB7E45640ED9F62D166
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Web.Components.Shared.ComputationGrid
    kind: class
    line: 6
    column: 22
  - qualifiedName: FormulaForge.Web.Components.Shared.ComputationGrid.RowScalarData
    kind: class
    line: 12
    column: 18
  - qualifiedName: FormulaForge.Web.Components.Shared.ComputationGrid.RowSeriesData
    kind: class
    line: 19
    column: 18
  - qualifiedName: FormulaForge.Web.Components.Shared.ComputationGrid.GetValue
    kind: method
    line: 26
    column: 21
  - qualifiedName: FormulaForge.Web.Components.Shared.ComputationGrid.FormatValue
    kind: method
    line: 29
    column: 20
  - qualifiedName: FormulaForge.Web.Components.Shared.ComputationGrid.FormatScalarValue
    kind: method
    line: 31
    column: 20
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Cette classe `ComputationGrid` gère la visualisation de données en grille, avec des paramètres pour les mois, les séries et les scalaires.
**Inputs** : Liste de mois, liste de séries avec leurs valeurs, et liste de scalaires.
**Outputs** : Affichage des données dans une grille avec des valeurs formatées.
**Side Effects** : Création d'objets `RowScalarData` et `RowSeriesData` avec des identifiants générés.
**Behaviors** : Méthodes pour récupérer, formater et traiter les valeurs.
**Note** : Les données sont stockées dans des collections et peuvent être affichées, mais ne sont pas modifiées dans ce code.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : `RowScalarData` represents row data in a computation grid, containing an ID, name, and scalar value.
**Inputs** : None, as it is a class with properties initialized automatically.
**Outputs** : None, as it is a data structure rather than a method.
**Side effects** : The ID is generated with `Guid.NewGuid()`, which may have side effects depending on the context.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Cette classe représente des données pour une série de lignes dans un tableau de calcul, avec des informations sur l'identifiant, le nom et les valeurs.
**Inputs/Outputs** : Elle reçoit un nom et des valeurs, et peut générer un identifiant automatiquement.
**Side Effects** : L'initialisation de l'identifiant avec `Guid.NewGuid()` peut créer des identifiants aléatoires.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Retreigne la valeur d'une cellule dans un tableau de données, en utilisant l'index de la cellule.
**Inputs** : Un objet `RowSeriesData` contenant les données et un entier `index` désignant la position de la cellule.
**Output** : Une valeur de type `decimal` correspondant à la cellule, ou `0` si l'index est invalide.
**Side Effects** : Aucun effet secondaire.
**Code** : `private decimal GetValue(RowSeriesData row, int index) => row.Values.GetValueOrDefault(index, 0);`
**XML** : L'annotation XML n'est pas présente, donc le comportement est basé sur la déclaration de la méthode.
**Dépendances** : Utilise la méthode `GetValueOrDefault` de `RowSeriesData.Values`.

<!-- codesemantic4llm:summary:4 -->
**Purpose**: This method formats a decimal value into a string with two decimal places, replacing zero with an en dash.
**Input**: A `decimal` value representing a numeric quantity.
**Output**: A formatted string, either "—" for zero or the value with two decimal places.
**Side Effects**: None. The method only returns a string and does not modify any external state.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Format scalar values into strings based on their type.
**Inputs** : A `ScalarValueNode` object representing a scalar value.
**Outputs** : A string representation of the scalar value.
**Side Effects** : None.
This method uses a switch expression to check the type of the scalar value and returns its string representation. It handles boolean, integer, and decimal types, formatting decimals to two decimal places. If the type is not recognized, it returns a question mark.
