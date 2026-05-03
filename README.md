# Backpacking Gear Recommender

## Overview

<!-- Brief project description goes here -->

---

## OOP Feature Requirements

| Feature | File | Line(s) | Rationale |
|---|---|---|---|
| **Inheritance (1)** | `filename.cs` | L00–L00 | <!-- Why a base class was used here --> |
| **Inheritance (2)** | `filename.cs` | L00–L00 | <!-- Why a derived class was used here --> |
| **Interface (1)** | `filename.cs` | L00–L00 | <!-- Rationale --> |
| **Interface (2)** | `filename.cs` | L00–L00 | <!-- Rationale --> |
| **Interface (3)** | `filename.cs` | L00–L00 | <!-- Rationale --> |
| **Polymorphism (1)** | `filename.cs` | L00–L00 | <!-- e.g., method override / dynamic dispatch --> |
| **Polymorphism (2)** | `filename.cs` | L00–L00 | <!-- e.g., method override / dynamic dispatch --> |
| **Access Modifiers** | `filename.cs` | L00–L00 | <!-- Reasoning for chosen visibility (public/private/protected/internal) --> |
| **Struct** | `filename.cs` | L00–L00 | <!-- Why a struct was appropriate here --> |
| **Enum** | `filename.cs` | L00–L00 | <!-- Why an enum was appropriate here --> |
| **Data Structure** | `filename.cs` | L00–L00 | <!-- Which data structure (List, Dictionary, etc.) and why --> |
| **I/O** | `filename.cs` | L00–L00 | <!-- How input/output is demonstrated using language I/O libraries --> |

---

## Design Patterns

### Pattern 1 — Singleton

- **Category:** Creational
- **File:** `GearRepository.cs`
- **Lines:** L5–L21
- **Rationale:** The program needs a single shared gear list across all components when ran. A Singleton on `GearRepository` ensures that every service and strategy reads and writes the same `_singletonGearList` instance, preventing duplicate data. The private constructor  and static `GetInstance()` factory method make sure that no second instance can be created.

---

### Pattern 2 — Factory Method

- **Category:** Creational
- **File:** `GearFactory.cs`
- **Lines:** L5–L14
- **Rationale:** Different gear types (backpacks, tents, sleeping bags) are parsed from JSON and each require their own construction logic.  the abstract class `GearFactory` defines the abstract `CreateGear` method so each concrete factory (`BackpackFactory`, `TentFactory`, `SleepingBagFactory`) can handle details for creating different items. this decouples the process from the rest of the program, making it easy to add new items later
---

### Pattern 3 — Strategy

- **Category:** Behavioral
- **File:** `ISortStrategy.cs`, `PriceSortStrategy.cs`, `RatingSortStrategy.cs`, `WeightSortStrategy.cs`
- **Rationale:** Users can sort gear results by price, rating, or weight, and the sorting algorithm needs to be swappable at runtime. Using the sort strategy was appropriate as each sorting algorithm followed similar steps but had different implementations for each. The `ISortStrategy` interface defines the `SortItems` contract, and each  strategy encapsulates one algorithm. The benefit of this design pattern is it avoids a large if/else block or switch case in the service layer and makes it easy to add a new sort strategy in the future.
