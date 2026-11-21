---
schemaVersion: 1
source: src/FormulaForge/FormulaForge.Domain/Entities/Project.cs
sourceHash: 89273B9B09C53785A8BEB398385E187383A265A273E22F9660A91B64970560AE
analyzer: dotnet-csharp
analyzerVersion: 17d4dc90ffc047e0b4f56190981b330e
modelHash: 6302C12B8DAF8980CAEE0802082DE9E2FD5675A69FE9FB96D5526D0A13C3E0D6
promptHash: CD1674EAE4005CFCE5F6314EEDEF319E173256D71C20F7F6D541D13EFD4599FE
languageHash: 67AD8F41A7BB0A18C45F3EFD8F6A32BDCD832BCF8F3F2EB44006C03D53076E18
symbols:
  - qualifiedName: FormulaForge.Domain.Entities.Project
    kind: class
    line: 6
    column: 14
  - qualifiedName: FormulaForge.Domain.Entities.DecimalScalarValueEntity
    kind: class
    line: 29
    column: 14
  - qualifiedName: FormulaForge.Domain.Entities.IntegerScalarValueEntity
    kind: class
    line: 37
    column: 14
  - qualifiedName: FormulaForge.Domain.Entities.BooleanScalarValueEntity
    kind: class
    line: 45
    column: 14
  - qualifiedName: FormulaForge.Domain.Entities.DecimalTimeSeriesEntity
    kind: class
    line: 53
    column: 14
  - qualifiedName: FormulaForge.Domain.Entities.DecimalTimeSeriesEntry
    kind: class
    line: 61
    column: 14
  - qualifiedName: FormulaForge.Domain.Entities.IntegerTimeSeriesEntity
    kind: class
    line: 70
    column: 14
  - qualifiedName: FormulaForge.Domain.Entities.IntegerTimeSeriesEntry
    kind: class
    line: 78
    column: 14
  - qualifiedName: FormulaForge.Domain.Entities.BooleanTimeSeriesEntity
    kind: class
    line: 87
    column: 14
  - qualifiedName: FormulaForge.Domain.Entities.BooleanTimeSeriesEntry
    kind: class
    line: 95
    column: 14
---

<!-- codesemantic4llm:summary:0 -->
**Summary :**
La classe `Project` représente un projet avec des propriétés comme son ID, son utilisateur, son nom, son code et plusieurs listes de valeurs scalaires et séries temporelles.
**Purpose :**
Elle permet de modéliser des projets, en associant des éléments scalaires et temporels à chaque projet.
**Inputs/Outputs :**
Elle accepte un GUID pour l'ID du projet et des valeurs scalaires ou séries temporelles pour les différents types.
**Side Effects :**
Elle crée des listes par défaut si elles ne sont pas explicitement initialisées.

<!-- codesemantic4llm:summary:1 -->
**Purpose** : Cette classe représente une entité avec des propriétés pour identifier, nommer, valider et lier un projet.
**Important Inputs/Outputs** : Elle reçoit un ID générée automatiquement, un nom requis, une valeur en decimal et un ID de projet.
**Side Effects** : Création d'un ID Version7, mais ne modifie pas d'autres données.
**Behavior** : Aucune logique d'opération spécifique, juste la création d'une instance.
**Inputs** : ID, Key, Value, ProjectId.
**Outputs** : Une instance de DecimalScalarValueEntity.
**Side Effects** : Génération de l'ID, mais ne conserve pas d'état externe.
**Behavior** : Aucune modification, seulement l'initialisation.
**Inputs** : Guid pour Id, string pour Key, decimal pour Value, Guid pour ProjectId.
**Outputs** : Une instance avec ces propriétés.

<!-- codesemantic4llm:summary:2 -->
**Purpose** : Ce class est utilisé pour représenter des valeurs scalaires entières associées à un projet, avec un identifiant unique et une clé requise.
**Inputs** : L'identifiant de projet (Guid) et la clé (string) sont des propriétés initialisées par le constructeur.
**Outputs** : Les propriétés Id, Key et Value sont exposées pour l'accès et la manipulation.
**Side effects** : La création de l'identifiant Id à partir de Guid.CreateVersion7() génère un GUID spécifique.

<!-- codesemantic4llm:summary:3 -->
**Purpose** : Cette classe représente une entité avec un ID, une clé requise, une valeur booléenne et un ID de projet.
**Inputs** : L'ID est généré automatiquement, la clé est fournie par l'utilisateur.
**Outputs** : Elle expose l'ID, la clé et la valeur booléenne.
**Side Effects** : Création d'un ID lors de l'initialisation.
**Important** : Utilise le type `Guid` et la valeur `true` par défaut pour la valeur booléenne.
**Behavior** : Aucune logique implémentée, il ne modifie pas les données.
**Trust** : Le code et les commentaires XML ne sont pas vérifiés pour leur fiabilité.
**Usage** : Stocke des valeurs booléennes associées à des entités.
**Constraints** : La clé est obligatoire.
**Output** : Idéale pour des scénarios où des valeurs booléennes sont liées à des entités.

<!-- codesemantic4llm:summary:4 -->
**Purpose** : This class represents a decimal time series entity, typically used to store time-based data with fractional precision.
**Inputs/Outputs** : It accepts a Guid Id, a required string Key, a list of DecimalTimeSeriesEntry objects, and a Guid ProjectId.
**Side Effects** : It initializes the Id using Guid.CreateVersion7() and sets the Entries list to an empty collection.
**Behavior** : The class is designed for data modeling, likely in a domain where precise time measurements are needed.
**Important Properties** : The Id and Key are required, ensuring the entity is uniquely identified and has a meaningful name.
**Output** : The entity can be used to store and retrieve time series data, possibly with associated project information.
**Side Effects** : The Guid is automatically generated, and the Entries list is initialized, which may affect data storage or retrieval.
**Behavior** : The class supports navigation to related entries through the Entries property.
**Input Validation** : The Key is required, ensuring the entity has a valid identifier.
**Output** : It provides a foundation for managing time-based data with fractional precision in a domain application.

<!-- codesemantic4llm:summary:5 -->
**Déclaration :** `DecimalTimeSeriesEntry` est une classe représentant une entrée dans une série de temps en décimales.
**Entrées importantes :** `Id` est un GUID auto-généré, `Value` est un chiffre décimal, et `Start`, `End` sont des moments en temps UTC.
**Sorties :** Elle expose les propriétés définies, permettant l'interrogation et la manipulation des données.
**Effets secondaires :** L'initialisation de `Id` utilise `Guid.CreateVersion7()` pour générer une version de GUID.

<!-- codesemantic4llm:summary:6 -->
**Purpose** :
This class represents an integer time series entity with an ID, a key, entries, and a project ID.
**Inputs** :
- `Key` : A required string used to identify the time series.
**Outputs** :
- `Id` : A generated GUID for the entity.
- `Entries` : A list of time series entries.
**Side Effects** :
- `Id` is automatically set to a versioned GUID.
- `Entries` is initialized as an empty list.

<!-- codesemantic4llm:summary:7 -->
**Purpose** : Represents a time series entry with a unique ID, a value, and start/end timestamps.
**Inputs** : None — no parameters in the constructor.
**Outputs** : Id, Value, Start, End, TimeSeriesId — data properties.
**Side Effects** : Creates a new Guid for Id.
**Behavior** : No custom logic; relies on default values and standard properties.
**Untrusted Data** : XML documentation is not considered reliable.
**Use** : Used for storing time series data in a system.
**Key Properties** : Id, Value, Start, End, TimeSeriesId.
**Guid Usage** : Version7 for unique entries.
**TimeSpan** : Start and End are in DateTimeOffset format.

<!-- codesemantic4llm:summary:8 -->
**Purpose** : This class represents a BooleanTimeSeriesEntity, which holds a list of entries linked to a project.
**Inputs** : Id, Key, ProjectId are required, with Id using Guid.CreateVersion7().
**Outputs** : Entries is a list of BooleanTimeSeriesEntry objects.
**Side Effects** : Id is auto-generated, and Entries is initialized as an empty list.
**Behavior** : No method definitions are provided, so its behavior is inferred from structure.
**Note** : XML documentation is not included in the declaration.
**Untrusted Data** : The declaration does not include method implementation or detailed behavior.
**Use** : Typically used in domain modeling for time-based boolean data.
**Constraints** : Key is required and string.
**Structure** : Class with properties and an empty default list.

<!-- codesemantic4llm:summary:9 -->
**Purpose** : Represents a time series entry with a boolean value, start and end times, and a time series identifier.
**Inputs** : None. The class relies on the constructor to initialize values.
**Outputs** : None. The class does not return any value.
**Side Effects** : Creates a new Guid on construction. No other side effects are visible.
