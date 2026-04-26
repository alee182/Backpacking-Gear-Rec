using Google.GenAI;
using Google.GenAI.Types;
using System.Text;
using System.Text.Json;
using DefaultNamespace;

namespace GearRecApp;

public class LLMService
{
    private readonly string _apiKey;
    private const string Model = "gemini-2.5-flash";

    public LLMService()
    {
        var secretsPath = Path.Combine(AppContext.BaseDirectory, "secrets.json");
        using var doc = JsonDocument.Parse(System.IO.File.ReadAllText(secretsPath));
        _apiKey = doc.RootElement.GetProperty("GeminiApiKey").GetString()
            ?? throw new InvalidOperationException("GeminiApiKey is missing from secrets.json");
    }

    public async Task<(string Json, int PromptTokens, int OutputTokens)> GetRecommendationsAsync(List<Gear> gearList, string userExtraInfo)
    {
        string prompt = BuildPrompt(gearList, userExtraInfo);
        var client = new Client(apiKey: _apiKey);
        var config = new GenerateContentConfig
        {
            // MaxOutputTokens = 2000,
            ResponseMimeType = "application/json"
        };
        var response = await client.Models.GenerateContentAsync(model: Model, contents: prompt, config: config);
        string json = response.Candidates![0].Content!.Parts![0].Text ?? string.Empty;
        int promptTokens = response.UsageMetadata?.PromptTokenCount ?? 0;
        int outputTokens = response.UsageMetadata?.CandidatesTokenCount ?? 0;
        return (json, promptTokens, outputTokens);
    }

    private static string BuildPrompt(List<Gear> gearList, string userExtraInfo)
    {
        var sb = new StringBuilder();

        sb.AppendLine("You are a backpacking gear recommendation assistant.");
        sb.AppendLine("From the gear options below, choose the 5 best fits for the user.");
        sb.AppendLine();
        sb.AppendLine($"User preferences: {userExtraInfo}");
        sb.AppendLine();
        sb.AppendLine("Gear options:");

        for (int i = 0; i < gearList.Count; i++)
        {
            var g = gearList[i];
            sb.AppendLine($"{i + 1}. Name: {g.Name} | Price: {g.Price} | Weight: {g.Weight} | Rating: {g.Rating} | Synopsis: {g.Synopsis}");
        }

        sb.AppendLine();
        sb.AppendLine("Respond with a JSON object where keys are the exact gear names in order from best to 5th best fit.");
        sb.AppendLine("Each value must be a single sentence explaining why that gear is a good fit.");
        if (!string.IsNullOrWhiteSpace(userExtraInfo))
            sb.AppendLine($"If the user provided extra preferences ('{userExtraInfo}'), try to reference them directly in each sentence when relevant.");
        sb.AppendLine("Include exactly 5 entries, ordered best fit first.");

        return sb.ToString();
    }
}
