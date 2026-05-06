using DefaultNamespace;
using System.Text.Json;

namespace GearRecApp;

public partial class MainPage : ContentPage
{
    private readonly List<string> _priorities = new() { "Rating", "Weight", "Price" };
    private IServiceLayer? _service;

    public MainPage()
    {
        InitializeComponent();
        RefreshPriorityLabels();
    }

    private void RefreshPriorityLabels()
    {
        var labels = new[] { Priority1Label, Priority2Label, Priority3Label };
        for (int i = 0; i < _priorities.Count; i++)
            labels[i].Text = $"{i + 1}. {_priorities[i]}";
    }

    private void OnMoveUp(object sender, EventArgs e)
    {
        int idx = GetRowIndex((Button)sender, isUp: true);
        if (idx > 0)
        {
            (_priorities[idx], _priorities[idx - 1]) = (_priorities[idx - 1], _priorities[idx]);
            RefreshPriorityLabels();
        }
    }

    private void OnMoveDown(object sender, EventArgs e)
    {
        int idx = GetRowIndex((Button)sender, isUp: false);
        if (idx < _priorities.Count - 1)
        {
            (_priorities[idx], _priorities[idx + 1]) = (_priorities[idx + 1], _priorities[idx]);
            RefreshPriorityLabels();
        }
    }

    private int GetRowIndex(Button btn, bool isUp)
    {
        if (btn == Row1_Up || btn == Row1_Down) return 0;
        if (btn == Row2_Up || btn == Row2_Down) return 1;
        if (btn == Row3_Up || btn == Row3_Down) return 2;
        return -1;
    }

    private async void OnRecommendClicked(object sender, EventArgs e)
    {
        string? gearType = GearTypePicker.SelectedItem?.ToString();
        string additionalInfo = AdditionalInfoEditor.Text?.Trim() ?? string.Empty;

        StatusLabel.IsVisible = false;
        CardsContainer.Children.Clear();

        if (string.IsNullOrEmpty(gearType))
        {
            StatusLabel.Text = "Please select a gear type.";
            StatusLabel.IsVisible = true;
            return;
        }

        string priority = _priorities[0].ToLower();
        string gearKey = gearType == "Sleeping Bag" ? "sleepingbag" : gearType.ToLower();

        RecommendBtn.IsEnabled = false;
        RecommendBtn.Text = "Loading...";

        try
        {
            _service = new ServiceLayer(gearKey, additionalInfo, priority);
            List<Gear> filtered = _service.GetFilteredGear();

            var llm = new LLMService();
            // TODO: REMOVE - token debug (start)
            var (json, promptTokens, outputTokens) = await llm.GetRecommendationsAsync(filtered, additionalInfo);
            TokenLabel.Text = $"[DEBUG] Prompt tokens: {promptTokens} | Output tokens: {outputTokens} | Total: {promptTokens + outputTokens}";
            TokenLabel.IsVisible = true;
            // TODO: REMOVE - token debug (end)

            JsonDocument doc;
            try
            {
                doc = JsonDocument.Parse(json);
            }
            catch (JsonException)
            {
                StatusLabel.Text = $"LLM returned malformed JSON. Raw response:\n{json}";
                StatusLabel.IsVisible = true;
                return;
            }
            using (doc)
            {
                var gearByName = filtered.ToDictionary(g => g.Name, g => g);

                foreach (var entry in doc.RootElement.EnumerateObject())
                {
                    string gearName = entry.Name;
                    string sentence = entry.Value.GetString() ?? string.Empty;

                    if (!gearByName.TryGetValue(gearName, out Gear? gear))
                        gear = filtered.FirstOrDefault(g => g.Name.Contains(gearName, StringComparison.OrdinalIgnoreCase));

                    if (gear == null) continue;

                    CardsContainer.Children.Add(BuildCard(gear, sentence));
                }
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Error: {ex.Message}";
            StatusLabel.IsVisible = true;
        }
        finally
        {
            RecommendBtn.IsEnabled = true;
            RecommendBtn.Text = "Get Recommendation";
            ResetBtn.IsVisible = CardsContainer.Children.Count > 0;
        }
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        _service?.ResetToDefault();
        _service = null;

        // Reset priorities to default order
        _priorities.Clear();
        _priorities.AddRange(new[] { "Rating", "Weight", "Price" });
        RefreshPriorityLabels();

        // Clear inputs
        GearTypePicker.SelectedItem = null;
        AdditionalInfoEditor.Text = string.Empty;

        // Clear results
        CardsContainer.Children.Clear();
        StatusLabel.Text = string.Empty;
        StatusLabel.IsVisible = false;
        TokenLabel.Text = string.Empty;
        TokenLabel.IsVisible = false;

        // Hide reset button
        ResetBtn.IsVisible = false;
    }

    private static View BuildCard(Gear gear, string llmSentence)
    {
        var card = new Border
        {
            Stroke = Color.FromArgb("#CCCCCC"),
            StrokeThickness = 1,
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 10 },
            Padding = new Thickness(16),
            BackgroundColor = Color.FromArgb("#F9F9F9")
        };

        var stack = new VerticalStackLayout { Spacing = 6 };

        // LLM sentence at top
        stack.Children.Add(SelectableText(llmSentence, italic: true, bold: true, color: "#444444"));

        stack.Children.Add(new BoxView { HeightRequest = 1, BackgroundColor = Color.FromArgb("#DDDDDD") });

        // Gear name
        stack.Children.Add(SelectableText(gear.Name, fontSize: 18, bold: true));

        // Stats row with bold captions
        stack.Children.Add(MixedLabel(13, "#555555",
            ("Price: ", gear.Price + "   "),
            ("Weight: ", gear.Weight + "   "),
            ("Rating: ", gear.Rating)));

        // Type-specific field
        var typeField = gear switch
        {
            DefaultNamespace.Backpack b    => ("Volume: ",  b.Volume),
            DefaultNamespace.Tent t        => ("Comfort: ", t.Comfort),
            DefaultNamespace.SleepingBag s => ("Warmth: ",  s.Warmth),
            _                              => (string.Empty, string.Empty)
        };
        if (!string.IsNullOrEmpty(typeField.Item1))
            stack.Children.Add(MixedLabel(13, "#555555", typeField));

        // Synopsis
        if (!string.IsNullOrWhiteSpace(gear.Synopsis))
            stack.Children.Add(SelectableText(gear.Synopsis, fontSize: 13));

        // Pros
        if (!string.IsNullOrWhiteSpace(gear.Pros))
            stack.Children.Add(MixedLabel(12, "#2E7D32", ("Pros: ", gear.Pros)));

        // Cons
        if (!string.IsNullOrWhiteSpace(gear.Cons))
            stack.Children.Add(MixedLabel(12, "#B71C1C", ("Cons: ", gear.Cons)));

        card.Content = stack;
        return card;
    }

    private static Label MixedLabel(double fontSize, string color, params (string Caption, string Value)[] pairs)
    {
        var label = new Label
        {
            FontSize = fontSize,
            BackgroundColor = Color.FromArgb("#F9F9F9")
        };
        var fs = new FormattedString();
        foreach (var (caption, value) in pairs)
        {
            fs.Spans.Add(new Span { Text = caption, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb(color) });
            fs.Spans.Add(new Span { Text = value,   TextColor = Color.FromArgb(color) });
        }
        label.FormattedText = fs;
        return label;
    }

    private static Editor SelectableText(string text, double fontSize = 14, bool bold = false, bool italic = false, string? color = null)
    {
        return new Editor
        {
            Text = text,
            FontSize = fontSize,
            FontAttributes = (bold ? FontAttributes.Bold : FontAttributes.None) | (italic ? FontAttributes.Italic : FontAttributes.None),
            TextColor = color != null ? Color.FromArgb(color) : null,
            IsReadOnly = true,
            AutoSize = EditorAutoSizeOption.TextChanges,
            BackgroundColor = Color.FromArgb("#F9F9F9"),
            Margin = new Thickness(0)
        };
    }
}

