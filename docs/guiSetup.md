# Setup Guide — Backpacking Gear Recommender

## Part 1: Gemini API Key

### Prerequisites

- A Google account
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed

### 1. Get a Gemini API Key

1. Go to [https://aistudio.google.com/apikey](https://aistudio.google.com/apikey)
2. Click **Create API key**
3. Create or select a Google Cloud project
4. Copy the generated key

> **Note:** If you see `limit: 0` errors, make sure the **Generative Language API** is enabled for your project at:
> `https://console.cloud.google.com/apis/library/generativelanguage.googleapis.com`
> The free tier is not available in all regions — if quota shows 0, you may need to enable billing on the project.

### 2. Add Your API Key to Both Projects

Create the following two files (both are gitignored and will never be committed):

**`src/LLM-Testing/secrets.json`**
```json
{
  "GeminiApiKey": "YOUR_API_KEY_HERE"
}
```

**`src/GearRecApp/secrets.json`**
```json
{
  "GeminiApiKey": "YOUR_API_KEY_HERE"
}
```

---

## Part 2: GUI Setup — MAUI App

### Prerequisites

- Windows 10 version 1903 or higher (build 18362+)

### 1. Install the MAUI Workload

Run once — installs the MAUI SDK tools:

```powershell
dotnet workload install maui
```

---

### 2. Install the Windows App Runtime

Required for MAUI to run on Windows:

```powershell
winget install Microsoft.WindowsAppRuntime.1.5
```

---

### 3. Restore Dependencies

```powershell
cd src/GearRecApp
dotnet restore
```

---

### 4. Run the App

```powershell
cd src/GearRecApp
dotnet run -f net8.0-windows10.0.19041.0 --no-launch-profile
```

A window will appear with a text input and a **Get Recommendation** button. Type a gear request (e.g. "lightweight sleeping bag for cold weather under $150") and click the button to get a Gemini-powered recommendation.

## Troubleshooting

| Error | Fix |
|---|---|
| `Class not registered (0x80040154)` | Install Windows App Runtime (step 2) |
| `secrets.json not found` | Create the file as shown in step 3 |
| `Google.GenAI quota limit: 0` | See `geminiSetup.md` for API key setup |
| `The launch profile could not be applied` | Add `--no-launch-profile` to the run command |

```

> This file is in `.gitignore` and will never be committed to the repo.

---

## 3. Restore and Run

```powershell
cd src/LLM-Testing
dotnet restore
dotnet run
```

If successful, a short poem about C# will be printed to the console.

---

## Project Structure

| File | Purpose |
|---|---|
| `src/LLM-Testing/test.cs` | Main entry point — calls the Gemini API |
| `src/LLM-Testing/LLM-Testing.csproj` | Project file, references `Google.GenAI` NuGet package |
| `src/LLM-Testing/secrets.json` | Local-only file holding your API key (gitignored) |
