# Backpacking Gear Recommender

## Overview

Backpacking Gear Recommender is a .NET application that helps users discover and compare backpacking gear — backpacks, tents, and sleeping bags — sourced from a  JSON database. Users can filter gear by type, sort results by price, weight, or rating, and get personalized recommendations powered by an LLM. The project is structured around core OOP principles (inheritance, interfaces, polymorphism) and implements Singleton, Factory Method, and Strategy design patterns.


---

## Setup Guide — Backpacking Gear Recommender

### Prerequisites

- A Google account
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed
- Windows 10 version 1903 or higher (build 18362+)

---

## 1. Get a Gemini API Key

1. Go to [https://aistudio.google.com/apikey](https://aistudio.google.com/apikey)
2. Click **Create API key**, then create or select a Google Cloud project
3. Copy the generated key

> **Note:** If you see `limit: 0` errors, make sure the **Generative Language API** is enabled at:
> `https://console.cloud.google.com/apis/library/generativelanguage.googleapis.com`
> The free tier is not available in all regions — if quota shows 0, you may need to enable billing.

---

## 2. Add Your API Key

Create this file (gitignored — never committed):

**`src/GearRecApp/secrets.json`**
```json
{
  "GeminiApiKey": "YOUR_API_KEY_HERE"
}
```

---

## 3. Install the MAUI Workload

Run once:

```powershell
dotnet workload install maui
```

---

## 4. Install the Windows App Runtime

```powershell
winget install Microsoft.WindowsAppRuntime.1.5
```

---

## 5. Restore and Run

```powershell
cd src/GearRecApp
dotnet restore
dotnet run -f net8.0-windows10.0.19041.0 --no-launch-profile
```

---

## Troubleshooting

| Error | Fix |
|---|---|
| `Class not registered (0x80040154)` | Install Windows App Runtime (step 4) |
| `secrets.json not found` | Create `src/GearRecApp/secrets.json` as shown in step 2 |
| `Google.GenAI quota limit: 0` | Enable the Generative Language API and check billing (see step 1 note) |
| `The launch profile could not be applied` | Add `--no-launch-profile` to the run command |

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

---

## Design Decisions

The app is split into two main components — **Core** (business logic) and **GearRecApp** (MAUI UI). The MAUI layer passes user inputs to `ServiceLayer`, which is what bridges the UI to the Backend. It takes user inputs from the frontend and uses tools like `GearManager` to select the right factory for object creation, parse the JSON, and fill the singleton `GearRepository`, then runs the chosen `ISortStrategy` to sort results before returning them to the UI. The UI then forwards the list to `LLMService` for tailored recommendations from a Gemini Model.

`Gear` is an abstract  class that holds all shared properties of different gear types (name, price, weight, rating, etc.) so that `Backpack`, `Tent`, and `SleepingBag` only need to define what is unique to them. this helps reduce duplication in code. Similarly, the abstract`GearFactory` class contains an abstract  `CreateGear()` contract across all three concrete factories, while letting each one handle the specific JSON fields its gear type needs.  the `ISortStrategy` allows different sorting strategies hold there own sort method and the `ServiceLayer` holds a reference to the interface, so swapping in a new sort algorithm at runtime requires no changes to any other class. The main tradeoff of using `ISortStrategy` is that for only three sort options, it introduces three extra classes for what could have been a simple switch expression. The benefit is it aligns with the open closed principle as you can add new sort strategies without changing the `ServiceLayer`.However, that comes at the cost of more files and complexity than whats needed. For abstract classes, using `Gear` and `GearFactory` as abstract classes rather than interfaces means that subclasses can only have one parent class instead of having the ability to have multiple interfaces. If a future gear type needed to inherit behavior from a different hierarchy, it would not be possible. Using interfaces instead would have been more flexible, but abstract classes were chosen here because they also let shared implementation (like the base constructor in `Gear`) live in one place rather than being duplicated across every subclass.
