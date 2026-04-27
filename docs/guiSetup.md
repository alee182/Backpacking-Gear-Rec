# Setup Guide — Backpacking Gear Recommender

## Prerequisites

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
