using DefaultNamespace;
using Google.GenAI;
using Google.GenAI.Types;
using System.Text;
using System.Text.Json;

// Load gear and apply price sort strategy
var serviceLayer = new ServiceLayer("backpack", "", "price");
List<Gear> filtered = serviceLayer.GetFilteredGear();

Console.WriteLine($"Filtered to {filtered.Count} backpacks by lowest price:\n");
foreach (var g in filtered)
    Console.WriteLine($"  {g.Name} — {g.Price}");

// Build prompt
var sb = new StringBuilder();
sb.AppendLine("You are a backpacking gear recommendation assistant.");
sb.AppendLine("From the gear options below, choose the 5 best fits for the user.");
sb.AppendLine();
sb.AppendLine("User preferences: (none specified)");
sb.AppendLine();
sb.AppendLine("Gear options:");
for (int i = 0; i < filtered.Count; i++)
{
    var g = filtered[i];
    sb.AppendLine($"{i + 1}. Name: {g.Name} | Price: {g.Price} | Weight: {g.Weight} | Rating: {g.Rating} | Synopsis: {g.Synopsis}");
}
sb.AppendLine();
sb.AppendLine("Respond with a JSON object where keys are the exact gear names in order from best to 5th best fit.");
sb.AppendLine("Each value must be a single sentence explaining why that gear is a good fit.");
sb.AppendLine("Include exactly 5 entries, ordered best fit first.");

// Load API key
var secretsPath = Path.Combine(AppContext.BaseDirectory, "secrets.json");
using var secretsDoc = JsonDocument.Parse(File.ReadAllText(secretsPath));
var apiKey = secretsDoc.RootElement.GetProperty("GeminiApiKey").GetString()!;

// Call LLM
Console.WriteLine("\nSending to LLM...\n");
var client = new Client(apiKey: apiKey);
var config = new GenerateContentConfig
{
    MaxOutputTokens = 500,
    ResponseMimeType = "application/json"
};
var response = await client.Models.GenerateContentAsync(model: "gemini-2.5-flash", contents: sb.ToString(), config: config);
string result = response.Candidates[0].Content.Parts[0].Text ?? "";

Console.WriteLine("LLM Response:");
Console.WriteLine(result);

