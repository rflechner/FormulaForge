---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Engine/DomainSpecificLanguage/ValueExpressionNodeParser.cs
sourceHash: 294F27A55B448DAA31CBA2962046F938C7C4FD96A19849E0E099685D11EC7D92
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Engine.DomainSpecificLanguage.ValueExpressionNodeParser
    kind: class
    line: 11
    column: 14
---

<!-- codesemantic4llm:summary:0 -->
**Purpose**
This class defines parsers for parsing scalar values, boolean values, literals, and expressions in a domain-specific language. It supports parsing numbers, booleans, and variable references, allowing for the conversion of parsed expressions into appropriate data structures.
**Inputs/Outputs**
It parses strings that represent integer, decimal, or boolean values, as well as variable names and literal expressions. The output is scalar value nodes that can be used in further expression parsing.
**Side Effects**
The parsing process creates new instances of `ScalarValueNode`, `VariableName`, and `LiteralExpressionNode` based on the input strings. These instances are used in higher-level expression parsing and do not alter the input data.
