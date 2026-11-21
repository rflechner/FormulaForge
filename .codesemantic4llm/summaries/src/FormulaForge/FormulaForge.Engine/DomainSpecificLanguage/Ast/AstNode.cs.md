---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/DomainSpecificLanguage/Ast/AstNode.cs
sourceHash: 53F7C54D003DA5A06541228FCF0CC1374D6A02FEC3A47A4C0212D6B88C500CBA
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.AstNode
    kind: record
    line: 6
    column: 24
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.CommentNode
    kind: record
    line: 8
    column: 22
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.ValueExpressionNode
    kind: record
    line: 10
    column: 24
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode
    kind: record
    line: 12
    column: 24
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode.IntegerScalarValue
    kind: record
    line: 14
    column: 26
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode.DecimalScalarValue
    kind: record
    line: 16
    column: 26
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.ScalarValueNode.BooleanScalarValue
    kind: record
    line: 18
    column: 26
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.VariableName
    kind: record
    line: 21
    column: 22
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.LiteralExpressionNode
    kind: record
    line: 23
    column: 24
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.LiteralExpressionNode.ConstantValueExpressionNode
    kind: record
    line: 25
    column: 26
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.LiteralExpressionNode.VariableValueExpressionNode
    kind: record
    line: 27
    column: 26
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.ComputedExpressionNode
    kind: record
    line: 30
    column: 22
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.FunctionSignatureExpressionNode
    kind: record
    line: 32
    column: 15
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.FunctionCallExpressionNode
    kind: record
    line: 34
    column: 15
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.StatementNode
    kind: record
    line: 36
    column: 24
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.StatementNode.VariableAssignmentExpressionNode
    kind: record
    line: 38
    column: 26
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.StatementNode.FunctionDeclarationNode
    kind: record
    line: 40
    column: 26
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.Ast.InvalidLine
    kind: record
    line: 43
    column: 15
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Définit une structure abstraite pour représenter des nœuds de code dans un langage spécifique.
**Entrées/Sorties** : Accepte une position de texte (TextRange) et retourne une instance.
**Effets secondaires** : Aucun, la structure est immuable.
**Notes** : La structure est abstraite et peut être subclassée pour représenter différents types de nœuds de code.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Cette déclaration définit un type `CommentNode` utilisé pour représenter les commentaires dans un langage de programmation spécifique.
**Inputs** : Position de l'intervalle de texte (TextRange) et le contenu du commentaire (string).
**Outputs** : Un objet `CommentNode` qui encapsule la position et le texte du commentaire.
**Side Effects** : Aucun effet secondaire visible, mais peut être utilisé pour des activités de navigation ou de traitement du code.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Ce type représente une expression de valeur dans un langage spécifique, avec une position de la plage de texte.
**Inputs** : PositionRange, qui définit l'emplacement de l'expression dans le code.
**Outputs** : AstNode, qui permet de naviguer dans l'arbre d'expression.
**Side Effects** : Aucun, car c'est un record immuable.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Ce type représente des valeurs scalaires dans un langage spécifique, avec des sous-classes pour chaque type (entier, décimal, booléen).
**Inputs/Outputs** : Aucun, il est utilisé comme un type interne pour structurer les expressions.
**Side Effects** : Aucun, il est pur et n'effectue pas d'actions.
**XML Documentation** : Aucune mention spécifique dans la déclaration.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Represents an integer value in the AST for a formula engine.
**Input** : An integer value.
**Output** : A record containing the integer value.
**Side Effects** : None.
**Behavior** : Holds a single integer value, no operations.
**Untrusted Data** : XML documentation is not considered reliable.
**C# Type** : Sealed record with a constructor.
**Usage** : Used in parsing or representing integer expressions.
**Security Note** : XML documentation should not be relied upon for accurate behavior.

<!-- codesemantic4llm:summary:5 -->
**Purpose**: Represents a scalar value with a decimal precision.
**Input**: A `decimal` value.
**Output**: A structured node with the decimal value.
**Side Effects**: None.
**Full declaration**: A `sealed` record with a single property `Value` of type `decimal`.
**XML documentation**: Not provided.
**Behavior**: Stores the value and can be used in language-specific parsing or processing.
**Trust**: XML documentation is treated as untrusted.
**Usage**: Typically used in parsing or generating code where decimal precision is required.
**Untrusted data**: The source code and any associated documentation are not considered authoritative.

<!-- codesemantic4llm:summary:6 -->
**Déclaration :** `BooleanScalarValue` est une classe record dans le domaine spécifique de FormulaForge.
**Usage :** Elle représente une valeur booléenne pour la navigation dans l'AST.
**Entrée :** Une seule valeur booléenne `Value`.
**Sortie :** Une instance de `BooleanScalarValue` contenant la valeur booléenne.
**Effet secondaire :** Aucun; elle ne modifie ses entrées.
**Exemple :** `new BooleanScalarValue(true)` crée une valeur booléenne.
**Documentation :** Aucune documentation XML dans la déclaration.
**Contexte :** Utilisée pour des expressions booléennes dans l'AST.
**Critères d'inférence :** Elle est déclarée comme `sealed` pour éviter la sous-classification.
**Nature :** Une instance de type `BooleanScalarValue` peut être utilisée comme une valeur booléenne.

<!-- codesemantic4llm:summary:7 -->
**VariableName** est une déclaration de type record dans le domaine spécifique de la formule. Elle encapsule une position de plage de texte et le nom du variable. Cela permet de représenter des variables dans un contexte syntaxique.
**Entrées** : Position de plage de texte et nom de la variable.
**Sorties** : Une instance du type `VariableName` avec ces attributs.
**Effets secondaires** : Aucun, puisque c'est un record immuable.

<!-- codesemantic4llm:summary:8 -->
**Purpose** : Ce type représente une expression littérale dans un langage spécifique à l'expression, avec une position de texte associée.
**Inputs** : Une position de texte (TextRange) et une valeur (ScalarValueNode ou VariableName).
**Outputs** : Une expression littérale avec une position de texte.
**Side effects** : Aucun. Il n'agit simplement sur les données, sans modifier l'état extérieur.

<!-- codesemantic4llm:summary:9 -->
**Purpose** : This class represents a constant value expression in a domain-specific language, storing its position and value.
**Inputs** : A `TextRange` indicating the location in the code, and a `ScalarValueNode` holding the actual value.
**Outputs** : A `LiteralExpressionNode` that wraps the constant value and its position.
**Side Effects** : None, as it is a immutable record.
**Trust** : XML documentation and source code are treated as untrusted data.
**Usage** : Used in parsing or navigation to reference constant values by their position in the source.
**Structure** : Combines `TextRange` and `ScalarValueNode` into a single immutable object.
**Immutability** : The record cannot be modified after creation, ensuring data integrity.
**Parent Class** : Inherits from `LiteralExpressionNode`, making it a specialized type.
**Use Case** : Useful in code navigation tools to reference constant values during analysis.

<!-- codesemantic4llm:summary:10 -->
**Purpose**: This class represents a node in an AST that holds a variable's value expression.
**Inputs**: Position range and variable name.
**Outputs**: Stores the variable's value expression.
**Side effects**: None.
The node is used in parsing and generating expressions.
It is part of a domain-specific language's AST structure.
It is immutable since it is a sealed record.
It relies on the LiteralExpressionNode base class.
It is used for storing variable values in the AST.
It is used in expression evaluation and transformation.

<!-- codesemantic4llm:summary:11 -->
**Purpose** : Ce type repère un nœud d'expression calculée dans un langage spécifique, associant une position de texte et une opération d'expression.
**Inputs** : Position de texte (TextRange) et une expression à calculer (BinaryOperationOperand<ValueExpressionNode>).
**Output** : Une instance du record ComputedExpressionNode avec la position et l'expression.
**Side Effects** : Aucun effet secondaire, car il ne modifie pas d'état externe.
**Important** : Utilise le texte de la position pour identifier l'expression dans le code.

<!-- codesemantic4llm:summary:12 -->
C# source defines a record for a function signature expression node. It holds the position range where the node appears, the function name, and an array of parameter names. This node represents a function signature in the AST, often used during code analysis or transformation. It is used as input for parsing or transforming function declarations. The record provides metadata for the function signature without encapsulating its implementation.

<!-- codesemantic4llm:summary:13 -->
**Purpose:**
Cette classe représente une expression de fonction dans un langage spécifique, en incluant sa position, le nom de la fonction et ses arguments.
**Inputs/Outputs:**
Elle prend une plage de position (`TextRange`), le nom de la fonction (`string`), et un tableau d'expressions (`ValueExpressionNode[]`). Elle retourne une instance de `FunctionCallExpressionNode`.
**Side Effects:**
Aucun effet secondaire visible, sauf la création d'une nouvelle instance.

<!-- codesemantic4llm:summary:14 -->
**Purpose** : Ce type représente un nœud de déclaration dans un langage spécifique. Il permet de représenter des expressions d'assignation ou des déclarations de fonctions.
**Inputs** : La position du nœud dans le texte et le contenu associé (variable, expression de valeur, fonction, paramètres).
**Outputs** : Un nœud de déclaration qui peut contenir des expressions d'assignation ou des corps de fonctions.
**Side Effects** : Aucun effet secondaire direct. Il est utilisé pour la navigation et l'analyse de code.

<!-- codesemantic4llm:summary:15 -->
**Purpose**: This class represents a variable assignment expression in C# code.
**Inputs**: A position range, a variable name, and a value expression.
**Output**: A structured representation of the assignment statement.
**Side Effects**: None; it only stores data.
It is a record, so it cannot be modified after creation.
The variable name and value are central to the assignment.
Position range helps in locating the statement in the code.
It inherits from StatementNode, indicating it is part of a larger statement structure.
Variables and expressions are core to C# syntax.
This class is used for code analysis or navigation in IDEs.

<!-- codesemantic4llm:summary:16 -->
**Purpose** : Represents a function declaration in a C# source file, including its position, name, parameters, and body.
**Inputs** : Position range, function name, parameter variables, and expression body.
**Outputs** : A structured data object with metadata about the function.
**Side effects** : None; the node is immutable and does not modify the source.
**Untrusted data** : XML documentation is not considered reliable in this context.
**Language** : French.
**Lines** : 6.

<!-- codesemantic4llm:summary:17 -->
**Purpose** : This record represents an invalid line in the code, specifying its position and the line content.
**Inputs** : A `TextRange` indicating the line's location and a `string` for the line's content.
**Outputs** : A `AstNode` containing the invalid line data.
**Side Effects** : Not applicable; the record is purely informational.
**Behavior** : The line is marked as invalid, possibly triggering error handling in the code navigation system.
**Untrusted Data** : The XML documentation and source code should not be relied upon for behavior.
**Language** : French.
**Lines** : 10.
**Explanations** : The record is used to flag problematic lines in the code. It contains the line's position and content. The `AstNode` is used to represent this line in the AST. The line is considered invalid for navigation purposes.
