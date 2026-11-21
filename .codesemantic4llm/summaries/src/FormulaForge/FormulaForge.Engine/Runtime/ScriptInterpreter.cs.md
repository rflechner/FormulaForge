---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/Runtime/ScriptInterpreter.cs
sourceHash: D9AB64108334DFCE39717222607949B8806D273B096CEC51DF2D019040B5FF50
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Runtime.FunctionRegistryId
    kind: record
    line: 10
    column: 15
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter
    kind: class
    line: 12
    column: 21
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Run
    kind: method
    line: 14
    column: 26
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Run
    kind: method
    line: 21
    column: 26
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.ParseScriptCode
    kind: method
    line: 34
    column: 22
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.ProcessStatement
    kind: method
    line: 42
    column: 26
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.ProcessStatement
    kind: method
    line: 47
    column: 27
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.RunVariableAssignment
    kind: method
    line: 68
    column: 27
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.CallFunction
    kind: method
    line: 112
    column: 27
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Evaluate
    kind: method
    line: 139
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.TryEvaluateValueExpression
    kind: method
    line: 166
    column: 18
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Add
    kind: method
    line: 193
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Add
    kind: method
    line: 211
    column: 29
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Subtract
    kind: method
    line: 234
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Subtract
    kind: method
    line: 252
    column: 29
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Multiply
    kind: method
    line: 272
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Multiply
    kind: method
    line: 290
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Multiply
    kind: method
    line: 299
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Add
    kind: method
    line: 304
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Subtract
    kind: method
    line: 313
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Add
    kind: method
    line: 322
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Subtract
    kind: method
    line: 328
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Divide
    kind: method
    line: 334
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.ComputeTimeSeries
    kind: method
    line: 340
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.AsDecimal
    kind: method
    line: 358
    column: 48
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Multiply
    kind: method
    line: 370
    column: 29
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Divide
    kind: method
    line: 391
    column: 34
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Divide
    kind: method
    line: 409
    column: 29
  - qualifiedName: FormulaForge.Engine.Runtime.ScriptInterpreter.Divide
    kind: method
    line: 429
    column: 34
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : Identifies functions in a registry with their name and parameter count.
**Inputs** : Name (string), ParameterCount (int).
**Outputs** : A record containing the function's name and parameter count.
**Side effects** : None.
**Untrusted data** : XML documentation is not considered reliable.
**Trust** : Source code and documentation are treated as untrusted.
**Use** : Used in function registry systems to reference functions.
**Behavior** : Always returns a valid record with the provided data.
**Structure** : Simple record with two properties.
**Typing** : String and int are used for the properties.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Le script interpréteur exécute du code source parmi des expressions, gère les variables et les fonctions, et effectue des calculs dans des contextes spécifiques.
**Entrées/Sorties** : Accepte du code source et des expressions. Sort des résultats de l'exécution (succès, erreur, absence de variable).
**Effets secondaires** : Crée des contextes d'execution, modifie des variables, et peut éliminer des fonctions.

<!-- codesemantic4llm:summary:2 -->
**Objectif** : Exécuter du code dans un environnement de script.
**Entrées** : Chaîne de code à exécuter.
**Sorties** : Résultat de l'exécution (erreur ou résultat).
**Effets secondaires** : Modifie l'état de l'interprète et peut générer des exceptions.
**Prérequis** : Code valide et compilable.
**Utilisation** : Pour interpréter des scripts dynamiquement.
**Événements** : Peut générer des événements lors de l'exécution.
**Gestion d'erreur** : Retourne CodeRunResult avec détails d'erreur.
**Contexte** : Utilisé dans des bibliothèques de runtime pour exécuter du code.
**Dépendances** : Dépend de la méthode ParseScriptCode.

<!-- codesemantic4llm:summary:3 -->
**Summary :**
La méthode `Run` exécute une série de nœuds AST en les traitant. Elle retourne un résultat si un nœud échoue. Elle n'a pas d'effets secondaires dans ce cas.
**Proposition :**
Cette méthode prend une liste de nœuds AST comme entrée et exécute chaque nœud. Si un nœud échoue, elle le retourne. Si tous les nœuds réussissent, elle retourne un résultat de succès.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : Convert raw script code into an AST for further processing.
**Inputs** : A string containing the script code.
**Outputs** : An array of AST nodes if parsing is successful.
**Side Effects** : Throws an exception on failure, propagating the error message.
**Behavior** : Uses a parser to validate and convert script to AST structure.
**Assumptions** : Parsing is done by an external parser, not implemented here.
**Untrusted Data** : XML comments and parsing results are treated as external.
**No Behavior** : Does not modify inputs or outputs beyond parsing.
**Error Handling** : Catches and throws exceptions for parsing failures.
**Default Return** : Returns an empty array if parsing fails.

<!-- codesemantic4llm:summary:5 -->
**Purpose** : Process a script statement in the context of the global scope.
**Inputs** : A `StatementNode` representing the script statement to execute.
**Outputs** : A `CodeRunResult` indicating the outcome of the execution.
**Side Effects** : Modifies the execution context and global scope.
**Behavior** : Calls a private method to handle the statement.
**Trust** : Assumes the input is valid and safe to execute.
**Untrusted Data** : Does not validate or sanitize input.
**Language** : C#.
**Method** : `ProcessStatement` within `ScriptInterpreter`.
**Scope** : Uses `context.GlobalScope` for execution.

<!-- codesemantic4llm:summary:6 -->
Ce code interprète une instruction de script dans un environnement de programmation. Il traite les déclarations de fonctions, les affectations de variables et émet des résultats correspondants. L'entrée est un scope et une instruction de script, et la sortie est un résultat de code exécution. L'effet secondaire est l'enregistrement des fonctions dans le contexte.

<!-- codesemantic4llm:summary:7 -->
**Purpose** : Cette méthode exécute une assignment de variable dans un contexte de code, évaluant des expressions, appelant des fonctions ou utilisant des valeurs constantes.
**Inputs** : Un contexte de scope, une expression d'assignment, et le nom d'une variable.
**Outputs** : La valeur résultante de l'expression, ou un code d'erreur si une variable n'est pas trouvée.
**Side Effects** : Elle peut modifier le contexte de l'interprète en assignant une nouvelle valeur à la variable dans le scope.

<!-- codesemantic4llm:summary:8 -->
**Purpose** : Cette méthode exécute une fonction dans un environnement de script, en évaluant ses arguments et son corps.
**Entrées** : Un scope global, une expression de appel de fonction, et un résultat de sortie.
**Sorties** : Un résultat de code exécution (CodeRunResult) indiquant le succès ou une erreur.
**Effets secondaires** : Création d'un nouveau scope, évaluation des arguments, et évaluation du corps de la fonction. L'erreur est lancée si un argument ou le corps est évalué de manière impossible.

<!-- codesemantic4llm:summary:9 -->
**Summary (fr):**
La méthode `Evaluate` calcule une expression en utilisant des variables de domaine et des opérations binaires. Elle traite différents types d'opérations et retourne le résultat.
**Purpose:**
Évaluer des expressions complexes en se basant sur des variables et des opérations définies dans un contexte.
**Important inputs/outputs:**
Entrée : `Scope` et `BinaryOperationOperand`. Sortie : Valeur calculée.
**Side effects:**
Peut générer une exception si une valeur ne peut être évaluée.

<!-- codesemantic4llm:summary:10 -->
**Purpose** : This method evaluates a value expression in a given scope, supporting expressions like variables, literals, and function calls.
**Important Inputs** : Scope object, ValueExpressionNode, and an output variable.
**Important Outputs** : Result value if evaluation succeeds, or an exception if failure occurs.
**Side Effects** : Throws an exception if a variable is not found.
**Behavior** : Handles different expression types, evaluates them, and returns the result.
**Note** : XML documentation and source code are treated as untrusted data.
**No behavior is invented** — only what is present in the code.

<!-- codesemantic4llm:summary:11 -->
Cette méthode `Add` effectue la somme de deux valeurs de script dans un moteur de formula. Elle prend en entrée deux instances de type `RuntimeVariableValue` et retourne une nouvelle instance de type correspondant. Elle est utilisée pour additionner des variables ou des séries temporelles. L'exception est lancée si les types ne sont pas pris en charge.

<!-- codesemantic4llm:summary:12 -->
**Purpose** : Cette méthode effectue une addition de deux valeurs scalaires en fonction de leur type.
**Entrées** : Deux instances de `ScalarValueNode` représentant des valeurs numériques.
**Sortie** : Une nouvelle instance de `ScalarValueNode` contenant le résultat de l'addition.
**Effets secondaires** : Aucun effet secondaire.
**Comportement** : Elle vérifie le type des arguments et applique la correspondante opération.
**Gestion des erreurs** : Elle lance une exception si les types ne sont pas pris en charge.
**Types pris en charge** : Boolean, Decimal, Integer.
**Documentation** : Aucune documentation XML incluse dans le code fourni.
**Dépendance** : Utilise l'architecture de la bibliothèque FormulaForge.
**Contexte** : Appelée par l'interprète de scripts dans le runtime de FormulaForge.

<!-- codesemantic4llm:summary:13 -->
**Purpose** : Ce méthode effectue la soustraction de deux valeurs de type runtime, en gérant différents types tels que scalaires et séries temporelles.
**Inputs** : Deux valeurs de type runtime, pouvant être scalaires ou séries temporelles.
**Outputs** : Une valeur de runtime résultant de la soustraction.
**Side Effects** : Aucun effet secondaire, sauf l'émission d'une exception en cas de types non pris en charge.

<!-- codesemantic4llm:summary:14 -->
**Purpose** : This method performs numerical subtraction between two scalar values, returning the result as a new scalar value node.
**Inputs** : It takes two `ScalarValueNode` parameters, which can represent either integer or decimal values.
**Outputs** : It returns a `ScalarValueNode` containing the difference between the two input values.
**Side Effects** : It throws an exception when the input types are unsupported, indicating an invalid operation.

<!-- codesemantic4llm:summary:15 -->
**Purpose** : This method multiplies two values, handling different types like scalars and time series.
**Inputs** : Two `RuntimeVariableValue` objects, which can be scalars or time series.
**Outputs** : A new `RuntimeVariableValue` object with the result of the multiplication.
**Side Effects** : Throws an exception if the types are not supported for multiplication.

<!-- codesemantic4llm:summary:16 -->
**Purpose** : Multiplie des valeurs scalaires d'une série temporelle avec une valeur scalaire.
**Inputs** : `scalarA` (valeur de type scalaire) et `complexB` (série temporelle complexe).
**Output** : Une série temporelle avec valeurs calculées par la multiplication.
**Side Effects** : Modifie la valeur de `complexB` en place.

<!-- codesemantic4llm:summary:17 -->
**Purpose** : La méthode `Multiply` calcule le produit de deux valeurs numériques à partir de séries temporales.
**Inputs** : Elle prend deux instances de `RuntimeVariableValue` représentant des séries temporales.
**Output** : Elle retourne une nouvelle instance de `ScalarValueNode` avec la valeur produit.
**Side Effects** : Aucun effet secondaire direct.
**Behavior** : Elle applique une fonction de transformeur pour multiplier les valeurs des séries.
**Trust** : Ne confie pas aux données XML ou au code source pour déterminer son comportement.
**Assumptions** : Les séries temporales contiennent des valeurs numériques.
**Use Case** : Utilisée dans un environnement de script pour exécuter des calculs numériques.
**Unreliability** : Ne repose pas sur la documentation XML pour décrire son fonctionnement.
**Simplification** : Le code est concis mais implique des opérations de calcul interne.

<!-- codesemantic4llm:summary:18 -->
**Purpose** : Cette méthode ajoute deux valeurs scalaires complexes en temps série, puis retourne le résultat sous la forme d'une valeur de type `RuntimeTimeSeriesValue`.
**Inputs** : `scalarA` est une valeur de type `RuntimeVariableValue.RuntimeScalarValue` et `complexB` est une valeur de type `RuntimeVariableValue.RuntimeTimeSeriesValue` contenant des valeurs scalaires complexes.
**Output** : Elle retourne une nouvelle valeur de type `RuntimeTimeSeriesValue` avec les valeurs scalaires additionnées.
**Side Effects** : Elle modifie la valeur de `complexB` en ajoutant les valeurs de `scalarA`, mais elle ne produit pas d'effets secondaires visibles.

<!-- codesemantic4llm:summary:19 -->
**Purpose** : Ce méthode calcule la différence entre deux valeurs scalaires complexes en temps série, puis retourne le résultat sous forme d'une valeur complexe en temps série.
**Inputs** : Elle prend une valeur scalaire (RuntimeVariableValue.RuntimeScalarValue) et une valeur en séries temporelles complexes (RuntimeVariableValue.RuntimeTimeSeriesValue).
**Output** : Elle retourne une nouvelle valeur en séries temporelles complexe avec des valeurs calculées de la différence entre les deux scalaires.
**Side Effects** : Elle modifie la valeur de `complexB` en la transformant, et peut affecter l'ordre ou la structure des données en temps série.

<!-- codesemantic4llm:summary:20 -->
**Purpose** : Ce méthode effectue une addition de deux valeurs de série temporelle.
**Inputs** : Deux instances de `RuntimeVariableValue` représentant des séries temporelles.
**Output** : Une nouvelle instance de `RuntimeVariableValue` avec la valeur résultante de l'addition.
**Side Effects** : Aucun, la méthode ne modifie pas l'état externe.

<!-- codesemantic4llm:summary:21 -->
La méthode `Subtract` calcule la différence entre deux séries temporelles. Elle prend comme entrées deux `RuntimeVariableValue` représentant des séries temporelles. Elle retourne un `ScalarValueNode` avec la valeur numérique de la différence.
Elle utilise une fonction anonyme pour définir le nœud de valeur. Elle ne modifie pas l'état extérieur.
Sa sortie est un type de valeur numérique.
Elle ne génère pas d'effets secondaires.
Elle fonctionne avec des séries temporelles.
Elle est implémentée dans `RuntimeVariableValue`.
Elle utilise `ComputeTimeSeries`.
Elle calcule la valeur de la différence.
Sa fonctionnalité est basée sur l'arithmétique.
Elle est déclarée comme `private`.

<!-- codesemantic4llm:summary:22 -->
**Purpose** : This method performs division on two time series values.
**Inputs** : Two `RuntimeVariableValue` instances representing time series data.
**Output** : A `ScalarValueNode` containing the result of dividing the values.
**Side Effects** : None; it only computes the result.
The method uses a lambda to compute the division.
It relies on the `ComputeTimeSeries` method to process the values.
The division is done using scalar arithmetic.
It handles time series values as `RuntimeTimeSeriesValue`.
The result is a `DecimalScalarValue`.
The behavior depends on the internal implementation of `ComputeTimeSeries`.

<!-- codesemantic4llm:summary:23 -->
**Objectif** : Calculer une série de temps combinée à partir de deux séries de temps, en appliquant une fonction de transformation.
**Entrées** : Deux valeurs de série de temps (a et b), avec des valeurs de temps et des valeurs numériques.
**Sortie** : Une nouvelle valeur de série de temps combinée, calculée avec la fonction fournie.
**Effets secondaires** : Création de nouvelles séries de temps à partir des entrées, transformation des valeurs numériques.
**Note** : La méthode utilise des séries de temps existantes et les combine avec une fonction de calcul.
**Note** : Elle ne crée pas de séries de temps nouvelles à partir de données non fournies.
**Note** : L'initialisation des séries de temps est basée sur la valeur existante de `a` et `b`.

<!-- codesemantic4llm:summary:24 -->
Cette méthode convertit une valeur scalar en un type Decimal. Elle prend une valeur comme entrée et retourne un Decimal, ou lance une exception si la valeur est inadaptée. Elle ne produit pas d'effets secondaires notables.

<!-- codesemantic4llm:summary:25 -->
**Purpose** : Ce méthode effectue une multiplication de deux valeurs scalaires.
**Inputs** : Deux instances de `ScalarValueNode`, chacune représentant une valeur numérique.
**Outputs** : Une nouvelle instance de `ScalarValueNode` contenant le produit des valeurs.
**Side Effects** : Aucun, la méthode ne modifie pas l'état de l'application.

<!-- codesemantic4llm:summary:26 -->
**Purpose** : This method interprets a division operation between two runtime variable values, supporting scalar and time series data types.
**Inputs** : Two runtime variable values, which can be scalar or time series.
**Outputs** : A result of the division operation, as a runtime variable value.
**Side Effects** : Throws an exception if the types are unsupported for division.
The method uses pattern matching to handle different variable types.
It supports both scalar and time series values, handling complex numbers appropriately.
The behavior is based on the type of the variables passed to the method.

<!-- codesemantic4llm:summary:27 -->
**Purpose** : This method performs division of two scalar values.
**Inputs** : Two scalar values (either decimal or integer).
**Outputs** : Result of the division as a new scalar value.
**Side Effects** : Throws an exception if the types are unsupported.
The method checks the types of the inputs and returns the result of the division.
It handles both decimal and integer scalar values.
The division is performed using the respective value types.
Unsupported types result in an exception.

<!-- codesemantic4llm:summary:28 -->
**Purpose** : Ce méthode calcule la division d'une série temporelle par une valeur scalaire.
**Inputs** : Une série temporelle complexe et une valeur scalaire.
**Output** : Une nouvelle série temporelle avec les valeurs calculées.
**Side Effects** : Modifie la série temporelle en place.
**Behavior** : Divise chaque valeur de la série par la valeur de la variable de type scalaire.
**Trust Note** : Le code et la documentation sont considérés comme non fiables.
**Assumption** : Les valeurs sont des nombres décimaux.
**Assumption** : La série temporelle est non vide.
**Assumption** : Les types de données sont compatibles.
**Assumption** : La mémoire est libérée après l'utilisation.
