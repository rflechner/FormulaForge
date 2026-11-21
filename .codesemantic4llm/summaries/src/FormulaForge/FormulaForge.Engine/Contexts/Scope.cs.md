---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/Contexts/Scope.cs
sourceHash: 1EC42117B9F17E9DF53B88AA65EB8D9E332A07C2FFEE9AA85D5645A371B16ECE
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Contexts.Scope
    kind: class
    line: 6
    column: 21
  - qualifiedName: FormulaForge.Engine.Contexts.Scope.CreateChild
    kind: method
    line: 14
    column: 18
  - qualifiedName: FormulaForge.Engine.Contexts.Scope.TryGetScalar
    kind: method
    line: 20
    column: 17
  - qualifiedName: FormulaForge.Engine.Contexts.Scope.TryGetTimeSeries
    kind: method
    line: 34
    column: 17
  - qualifiedName: FormulaForge.Engine.Contexts.Scope.TrySetScalar
    kind: method
    line: 62
    column: 26
---

<!-- codesemantic4llm:summary:0 -->
**Purpose** : L'implémentation `Scope` gère la gestion des variables et leur hiérarchie dans un contexte de code. Elle permet d'obtenir des variables, de les récupérer en tant que séries temporelles et de les modifier.
**Inputs** : Le nom d'une variable ou une séries temporelle.
**Outputs** : Une valeur de variable ou null si la variable n'est pas trouvée.
**Side Effects** : Modifie les variables du contexte et peut générer des exceptions si une variable est tentée d'être surcharge.
**Important** : `Parent` pointe vers le contexte parent, et `CreateChild` crée un nouveau contexte avec des variables héritées.
**Tried Get** : `TryGetScalar` et `TryGetTimeSeries` vérifient la présence d'une variable dans les variables ou les variables intégrées. Si trouvée, elles la récupèrent et la retournent.
**Set Scalar** : `TrySetScalar` vérifie si la variable est déjà définie dans les variables intégrées. Si oui, elle retourne une erreur. Sinon, elle l'ajoute dans les variables et la retourne avec succès.
**Tried Get Time Series** : `TryGetTimeSeries` vérifie aussi les séries temporelles, en vérifiant s'il s'agit d'une séries temporelle.
**Tried Set Time Series** : Pas implémenté ici, mais probablement

<!-- codesemantic4llm:summary:1 -->
**Objectif** : Créer un nouveau contexte enfant avec le parent actuel.
**Entrée** : Aucune, car la méthode retourne un objet directement.
**Sortie** : Un nouveau `Scope` avec le parent et les variables prédéfinies.
**Effets secondaires** : Aucun, la méthode ne modifie pas l'objet externe.
**Comportement** : Si le parent est null, utilise les variables prédéfinies.
**Nature** : Méthode statique, simple et sans paramètres.
**Contexte** : Utilisée dans la gestion des scénarios et des contextes de calcul.
**Point d'entrée** : Appelé depuis un contexte parent.
**Utilisation** : Pour créer des scénarios ou des contextes de calcul dans une hiérarchie.
**Prérequis** : L'instance parent doit être valide.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Cette méthode tente d'obtenir la valeur d'une variable scalaire depuis des sources connues.
**Inputs** : Le nom de la variable et un référeur de valeur de sortie.
**Outputs** : La valeur si trouvée, null sinon.
**Side Effects** : Modifie le référeur de sortie si la variable est trouvée.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Retreuve une valeur de temps série en utilisant un nom de variable.
**Inputs** : Nom de la variable et un refèreur pour la valeur retournée.
**Outputs** : La valeur de temps série si trouvée, null sinon.
**Side Effects** : Affecte le refèreur avec la valeur retournée.
**Behavior** : Vérifie d'abord les variables intrinsèques, puis les variables externes.
**Untrusted Data** : Les variables et leur documentation peuvent être incomplètes ou incorrectes.
**No Behavior Invention** : Ne crée pas d'actions inconnues, ne modifie pas l'environnement.
**Type Safety** : Valide que la variable est de type RuntimeTimeSeriesValue.
**Null Handling** : Retourne false si la valeur n'est pas trouvée.
**No Unintended Side Effects** : Aucun effet secondaire indésirable détecté.

<!-- codesemantic4llm:summary:4 -->
This method tries to set a scalar variable in the context. It checks if the variable is already built-in or if it can be added. If not, it returns an error. The method returns a success result if the variable is set.
