using Google.GenAI;
using System.Text.Json;

namespace GearRecApp;

public partial class MainPage : ContentPage
{
    private readonly string _apiKey;

    public MainPage()
    {
        InitializeComponent();
        _apiKey = LoadApiKey();
    }

    private static string LoadApiKey()
    {
        var secretsPath = Path.Combine(AppContext.BaseDirectory, "secrets.json");
        if (!File.Exists(secretsPath))
            throw new FileNotFoundException($"secrets.json not found at: {secretsPath}");

        using var doc = JsonDocument.Parse(File.ReadAllText(secretsPath));
        return doc.RootElement.GetProperty("GeminiApiKey").GetString()
            ?? throw new InvalidOperationException("GeminiApiKey is missing from secrets.json");
    }

    private async void OnRecommendClicked(object sender, EventArgs e)
    {
        var prompt = PromptEditor.Text?.Trim();
        if (string.IsNullOrEmpty(prompt))
        {
            ResponseLabel.Text = "Please describe what gear you are looking for.";
            return;
        }

        RecommendBtn.IsEnabled = false;
        ResponseLabel.Text = "Getting recommendation...";

        try
        {
            var client = new Client(apiKey: _apiKey);
            var response = await client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
                contents: $"You are a backpacking gear expert. Recommend specific gear for the following request: {prompt}"
            );

            ResponseLabel.Text = response.Candidates?[0].Content?.Parts?[0].Text
                ?? "No response received.";
        }
        catch (Exception ex)
        {
            ResponseLabel.Text = $"Error: {ex.Message}";
        }
        finally
        {
            RecommendBtn.IsEnabled = true;
        }
    }
}

