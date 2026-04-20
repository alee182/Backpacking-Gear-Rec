using Google.GenAI;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var secretsPath = Path.Combine(AppContext.BaseDirectory, "secrets.json");
        if (!File.Exists(secretsPath))
            throw new FileNotFoundException($"secrets.json not found at: {secretsPath}");

        using var doc = JsonDocument.Parse(File.ReadAllText(secretsPath));
        var apiKey = doc.RootElement.GetProperty("GeminiApiKey").GetString()
            ?? throw new InvalidOperationException("GeminiApiKey is missing from secrets.json");

        // Initialize the client
        var client = new Client(apiKey: apiKey);

        // Call the model
        var response = await client.Models.GenerateContentAsync(
            model: "gemini-2.5-flash",
            contents: "Write a short poem about coding in C#."
        );

        // Display the output
        Console.WriteLine(response.Candidates[0].Content.Parts[0].Text);
    }
}