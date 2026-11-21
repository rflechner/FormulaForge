---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine.Tests/DomainSpecificLanguage/AstTestExtensions.cs
sourceHash: 48EB864E587065116A5E5E257012F34FA93B8A305D11F0AB54A6A3B9C3C76943
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.AstTestExtensions
    kind: class
    line: 7
    column: 21
  - qualifiedName: FormulaForge.Engine.Tests.DomainSpecificLanguage.AstTestExtensions.IgnorePosition
    kind: method
    line: 9
    column: 21
---

<!-- codesemantic4llm:summary:0 -->
**Purpose:**
Cette classe fournit une extension statique pour ignorer la position de nœuds dans un AST, facilitant la navigation et l'analyse sans se préoccuper de leur emplacement.
**Inputs/Outputs:**
Elle accepte un nœud AST (`T` de type `AstNode`) et retourne un nœud modifié avec la position ignorée. Les nœuds simples comme `LiteralExpressionNode` ou `VariableName` sont convertis en objets contenant la valeur avec la position désactivée.
**Side Effects:**
Elle modifie l'objet AST et peut le rendre null si le nœud est null. Elle ne crée pas de nouveaux nœuds, mais modifie les existants.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Cette méthode permet d'ignorer la position de l'ast à partir de l'AST, en modifiant les propriétés `PositionRange` et, pour les expressions, `VariableName`.
**Important Inputs/Outputs** : Elle prend une expression AST (`T`) et retourne une nouvelle version de celle-ci avec la position ignorée.
**Side Effects** : Elle modifie l'AST en ajoutant des propriétés et en réduisant la position, ce qui peut affecter la localisation ou l'analyse de l'AST.
